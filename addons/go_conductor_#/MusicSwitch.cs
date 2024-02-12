using System;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MusicSwitch: MultiMusicPlayer
{
    public GcMusicNode CurrentlyPlaying { get; private set; }
    public bool RestartOnRecue { get; set; }

    public override void Play()
    {
        base.Play();
        CurrentlyPlaying.Play();
    }

    public override void Pause()
    {
        base.Pause();
        CurrentlyPlaying.Pause();
    }

    public override void Stop()
    {
        base.Stop();
        CurrentlyPlaying.Stop();
    }

    /// <summary>
    /// Cues the song of the given name, if it exists as a child
    /// </summary>
    /// <param name="trackName">Name of the track as it exists as a node</param>
    /// <returns>true if track changed as a result </returns>
    public bool Cue(String trackName)
    {
        // Get the track
        GcMusicNode newTrack = GetTrack(trackName);
        
        // Exit if new track is null
        if (newTrack == null)
        {
            return false;
        }
        // Exit if same track and not restart on recue
        if (newTrack.Equals(CurrentlyPlaying) && !RestartOnRecue)
        {
            return false;
        }
        
        // Les just do a hard cut for now
        CurrentlyPlaying?.Stop();
        CurrentlyPlaying = newTrack;
        CurrentlyPlaying.Restart();
        
        // If we got this far, then we succeeded, wooo!!
        return true;
    }
}