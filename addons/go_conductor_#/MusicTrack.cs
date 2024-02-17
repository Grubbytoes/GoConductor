using System;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MusicTrack : GcMusicNode
{
    private AudioStreamPlayer AudioPlayer { get; set; }
    private float FinalTrackVolume { get; set; }
    [Export] public float Attack = 0.2f;
    [Export] public bool Loop = true;

    public override float PlaybackPosition
    {
        get {
            if (Playing)
            {
                return AudioPlayer.GetPlaybackPosition();
            }
            else
            {
                return PlayHead;
            }
        }
        set { AudioPlayer.Seek(value); }
    }


    public override void Play()
    {
        // If we are already playing, return
        if (Playing) {return;}
        
        // Not playing from the start (ie after pausing)?
        if (PlaybackPosition > 0.0)
        {
            // Lower the tracks volume and play
            AudioPlayer.VolumeDb = FinalTrackVolume - 30;
            AudioPlayer.Play(PlaybackPosition);
            
            // Get the tween going
            var fadeIn = CreateTween();
            fadeIn.TweenProperty(AudioPlayer, "volume_db", FinalTrackVolume, Attack);
        }
        else
        {
            // Just play from the start
            DebugPrint("playing from the start");
            AudioPlayer.Play();
        }
        
        // We're ready to go
        base.Play();
    }

    public override void Pause()
    {
        base.Pause();
        PlayHead = AudioPlayer.GetPlaybackPosition();
        StopAudioPlayer();
    }

    public override void Stop()
    {
        base.Stop();
        StopAudioPlayer();
    }
    
    // Stops Audio player after a short volume tween, nothing more, nothing less
    // doesn't touch any variables, nothing
    private void StopAudioPlayer()
    {
        var tween = CreateTween();
        tween.TweenProperty(AudioPlayer, "volume_db", FinalTrackVolume - 30, Attack);
        tween.TweenCallback(Callable.From(AudioPlayer.Stop));
    }

    // TODO
    private void TrackEnd()
    {
        // If loop, play the track again, sam
        if (Loop)
        {
            AudioPlayer.Play();
        }
        // Otherwise, we stop
        else
        {
            Stop();
        }
    }

    public override void _Ready()
    {
        AudioPlayer = (AudioStreamPlayer)GetChild(0);
        FinalTrackVolume = AudioPlayer.VolumeDb;
    }
}