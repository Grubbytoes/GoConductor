using System.Collections.Generic;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__.transition;


/// <summary>
/// The base class for anything that involves stopping one track and starting another. All music switches should have
/// one as a child.
/// </summary>
public abstract partial class MusicTransition : Node
{
    [Signal]
    public delegate void DoneEventHandler();

    public MusicTransition(GcMusicNode parent, float duration)
    {
        Parent = parent;
        Duration = duration;
        Incoming = new HashSet<GcMusicNode>();
        Outgoing = new HashSet<GcMusicNode>();
    }

    /// <summary>
    /// The set of all track come in in the transition
    /// </summary>
    protected HashSet<GcMusicNode> Incoming { private set; get; }

    /// <summary>
    /// The set of all tracks being stopped by this transition
    /// </summary>
    protected HashSet<GcMusicNode> Outgoing { private set; get; }

    /// <summary>
    /// The node which called the transition
    /// </summary>
    public GcMusicNode Parent { get; private set; }

    /// <summary>
    /// How long the transition should last for
    /// </summary>
    public float Duration { get; set; }

    protected Tween TransitionTween { get; set; }

    public virtual void Start()
    {
        // Create a tween as a child of the parent
        TransitionTween = Parent.CreateTween();
    }

    /// <summary>
    /// Kills the transition tween
    /// </summary>
    public void Kill()
    {
        TransitionTween?.Kill();
    }

    protected void TransitionDone()
    {
        // Stop all outgoing tracks
        foreach (var t in Outgoing)
        {
            t.Stop();
        }
        
        // Send the signal, baby!
        EmitSignal(MusicTransition.SignalName.Done);
    }

    /// <summary>
    /// Adds a new track to the set of incoming tracks, so long as it is not already in either set
    /// </summary>
    /// <param name="musicNode">The track to add</param>
    /// <returns>True if track added</returns>
    public virtual bool AddIncomingTrack(GcMusicNode musicNode)
    {
        if (!Incoming.Contains(musicNode) && !Outgoing.Contains(musicNode))
        {
            Incoming.Add(musicNode);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds a new track to the set of outgoing tracks, so long as it is not already in either set
    /// </summary>
    /// <param name="musicNode">The track to add</param>
    /// <returns>True if track added</returns>
    public virtual bool AddOutgoingTrack(GcMusicNode musicNode)
    {
        if (!Outgoing.Contains(musicNode) && !Incoming.Contains(musicNode))
        {
            Outgoing.Add(musicNode);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds all child tracks of a multi-music player node, those that are not already in either set.
    /// Note that even if this method returns false some track may have been added, it's just a sign that something may
    /// not have worked the way you intended - more of a warning that an outright failure
    /// </summary>
    /// <param name="musicTrack">The parent of all new tracks to add</param>
    /// <returns>True if all new tracks added okay</returns>
    public virtual bool AddIncomingTrack(MultiMusicPlayer musicTrack)
    {
        bool addedOk = true;

        foreach (GcMusicNode t in musicTrack.GetAllTracks())
        {
            addedOk = (addedOk && AddIncomingTrack((GcMusicNode)(MultiMusicPlayer)t));
        }

        return addedOk;
    }

    /// <summary>
    /// Adds all child tracks of a multi-music player node, those that are not already in either set.
    /// See warning for NewIncomingTrack
    /// </summary>
    /// <param name="musicTrack">The parent of all new tracks to add</param>
    /// <returns>True if all new tracks added okay</returns>
    public virtual bool AddOutgoingTrack(MultiMusicPlayer musicTrack)
    {
        bool addedOk = true;

        foreach (GcMusicNode t in musicTrack.GetAllTracks())
        {
            addedOk = (addedOk && AddOutgoingTrack((GcMusicNode)(MultiMusicPlayer)t));
        }

        return addedOk;
    }
}