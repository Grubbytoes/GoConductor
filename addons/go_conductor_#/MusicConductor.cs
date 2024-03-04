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

    public override float PlaybackPosition
    {
        get => LeadTrack.PlaybackPosition;
        set
        {
            foreach (GcMusicNode t in TracksCurrentlyPlaying)
            {
                t.PlaybackPosition = value;
            }
        }
    }
    
    public override void _EnterTree()
    {
        base._EnterTree();
        TracksCurrentlyPlaying = new List<GcMusicNode>();
    }

    public override void _Ready()
    {
        base._Ready();
        LeadTrack = GetChild(0) as MusicTrack;
    }

    /// <summary>
    /// Cues the track if it is not playing, cues out if it is
    /// </summary>
    /// <param name="trackName">Name of the track</param>
    /// <returns>True if track successfully found and acted upon</returns>
    public override bool Cue(string trackName)
    {
        DebugPrint("Cueing " + trackName);
        
        // Try cueing the track
        bool success = CueIn(trackName);
        
        // If that didn't work, maybe its already playing
        if (!success)
        {
            success = CueOut(trackName);
        }
        
        // If either was successful, success should be true
        return success;
    }

    public override IEnumerable<GcMusicNode> GetVisibleTracks()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Adds the track to the arrangement, if it is found
    /// </summary>
    /// <param name="trackName">Name of track to play</param>
    /// <returns>True if track successfully found and added to arrangement</returns>
    public bool CueIn(string trackName)
    {
        GcMusicNode trackIn = GetTrack(trackName);
        
        // Track not found or track already playing
        if (trackIn == null || TracksCurrentlyPlaying.Contains(trackIn) )
        {
            return false;
        }
        
        // Append track to currently playing, so we can see it later
        TracksCurrentlyPlaying.Add(trackIn);
        
        // Do we need to play the track?
        if (Playing)
        {
            trackIn.PlayFrom(PlaybackPosition);
        }
        else
        {
            // I changed this from playhead, if it breaks in the future
            trackIn.PlaybackPosition = PlaybackPosition;
        }

        return true;
    }

    /// <summary>
    /// Removes the track, by name, from the arrangement
    /// </summary>
    /// <param name="trackName">Track name</param>
    /// <returns>True if found and removed from arrangement successfully</returns>
    public bool CueOut(string trackName)
    {
        GcMusicNode trackOut = GetTrack(trackName);
        int trackOutIdx = TracksCurrentlyPlaying.IndexOf(trackOut);

        if (trackOutIdx < 0)
        {
            return false;
        }
        
        trackOut.Stop();
        TracksCurrentlyPlaying.RemoveAt(trackOutIdx);
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
            t.Stop();
        }
    }

    public override void PlayFrom(float position)
    {
        foreach (var t in TracksCurrentlyPlaying)
        {
            t.PlayFrom(position);
        }
        base.Play();
    }
}