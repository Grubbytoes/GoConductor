using System;
using GoConductorPlugin.addons.go_conductor__.transition;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MusicSwitch: MultiMusicPlayer
{
    private MusicTransition Transition;
    public GcMusicNode CurrentlyPlaying { get; private set; }
    public bool RestartOnRecue { get; set; }
    

    public override float PlaybackPosition
    {
        get => CurrentlyPlaying.PlaybackPosition;
        set => CurrentlyPlaying.PlaybackPosition = value;
    }
    
    public override void Play()
    {
        base.Play();
        DebugPrint("playing " + CurrentlyPlaying.Name);
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

    public override void PlayFrom(float position)
    {
        CurrentlyPlaying.PlayFrom(position);
    }

    /// <summary>
    /// Cues the song of the given name, if it exists as a child
    /// </summary>
    /// <param name="trackName">Name of the track as it exists as a node</param>
    /// <returns>true if track changed as a result </returns>
    public override bool Cue(String trackName)
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
        
        // If we're cueing from a state of pause, then our life is easy
        // But we are unlikely to be so lucky
        if (Playing)
        {
            // Abort the transition if there is one in progress
            Transition?.Kill();

            Transition = new Fade(this, 1f);
            Transition.AddIncomingTrack(newTrack);
            Transition.AddOutgoingTrack(CurrentlyPlaying);
            
            // May god have mercy on our souls
            Transition.Start();
        }
        
        // Set the marker to the new track
        CurrentlyPlaying = newTrack;
        
        // If we got this far, then we succeeded, wooo!!
        return true;
    }


}