using System.Collections.Generic;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__.transition;

/// <summary>
/// Once started, fades one or more tracks in or out. Tracks faded out are stopped once the transition is over
///
/// This is ALL IT DOES, any updates to currently playing must be done by the parent
/// </summary>
public class Fade : MusicTransition
{
    private HashSet<GcMusicNode> Incoming { set; get; }
    private HashSet<GcMusicNode> Outgoing { set; get; }
    
    public Fade(GcMusicNode parent, float duration) : base(parent, duration)
    {
        Incoming = new HashSet<GcMusicNode>();
        Outgoing = new HashSet<GcMusicNode>();
    }

    /// <summary>
    /// Adds a new track to the set of incoming tracks, so long as it is not already in either set
    /// </summary>
    /// <param name="musicNode">The track to add</param>
    /// <returns>True if track added</returns>
    public bool AddIncomingTrack(GcMusicNode musicNode)
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
    public bool AddOutgoingTrack(GcMusicNode musicNode)
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
    public bool AddIncomingTrack(MultiMusicPlayer musicTrack)
    {
        bool addedOk = true;

        foreach (GcMusicNode t in musicTrack.GetAllTracks())
        {
            addedOk = (addedOk && AddIncomingTrack(t));
        }

        return addedOk;
    }

    /// <summary>
    /// Adds all child tracks of a multi-music player node, those that are not already in either set.
    /// See warning for NewIncomingTrack
    /// </summary>
    /// <param name="musicTrack">The parent of all new tracks to add</param>
    /// <returns>True if all new tracks added okay</returns>
    public bool AddOutgoingTrack(MultiMusicPlayer musicTrack)
    {
        bool addedOk = true;

        foreach (GcMusicNode t in musicTrack.GetAllTracks())
        {
            addedOk = (addedOk && AddOutgoingTrack(t));
        }

        return addedOk;
    }

    public override void Start()
    {
        TransitionTween = Parent.CreateTween();
        
        // Add all outgoing tracks to the tween
        foreach (var t in Outgoing)
        {
            TransitionTween.Parallel().TweenProperty(t, "Gain", -30, Duration);
        }
        
        // Add all incoming tracks to the tween
        foreach (var t in Incoming)
        {
            // * lower track volume
            // * start the track
            // * Add volume tweener
            t.Gain -= 30f;
            t.Play();
            TransitionTween.Parallel().TweenProperty(t, "Gain", 30, Duration).AsRelative();
        }
        
        // Add callbacks
        TransitionTween.TweenCallback(Callable.From(StopOutgoing));
        
        // Let 'er rip!!!
        TransitionTween.Play();
    }

    public override void Kill()
    {
        throw new System.NotImplementedException();
    }

    private void StopOutgoing()
    {
        foreach (var t in Outgoing)
        {
            t.Stop();
        }
    }
}