using System.Collections.Generic;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MusicConductor : MultiMusicPlayer
{
    private List<GcMusicNode> TracksCurrentlyPlaying { get; set; }

    // Basically the first element in TracksCurrentlyPlaying is the lead
    public GcMusicNode LeadTrack
    {
        get {
            if (TracksCurrentlyPlaying.Count < 1)
            {
                return null;
            }
            return TracksCurrentlyPlaying[0];
        }
        private set => TracksCurrentlyPlaying.Insert(0, value);
    }

    public override void _Ready()
    {
        base._Ready();
        LeadTrack = GetChild(0) as MusicTrack;
    }
    
    /// <summary>
    /// Adds the track to the arrangement, if it is found
    /// </summary>
    /// <param name="trackName">Name of track to play</param>
    /// <returns>True if track successfully found and added to arrangement</returns>
    public bool CueIn(string trackName)
    {
        GcMusicNode trackIn = GetTrack(trackName);

        if (trackIn == null)
        {
            return false;
        }
        
        TracksCurrentlyPlaying.Add(trackIn);
        
        // Do we need to play the track?
        if (Playing)
        {
            GD.Print("UNIMPLIMENTED LMAO");
            // TODO
        }

        return true;
    }

    public override void Play()
    {
        base.Play();
        foreach (GcMusicNode t in TracksCurrentlyPlaying)
        {
            t.Play();
        }
    }

    public override void Pause()
    {
        base.Pause();
        foreach (GcMusicNode t in TracksCurrentlyPlaying)
        {
            t.Pause();
        }
    }

    public override void Stop()
    {
        base.Stop();
        foreach (GcMusicNode t in TracksCurrentlyPlaying)
        {
            t.Pause();
        }
    }
}