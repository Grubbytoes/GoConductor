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
            // Fade the track back in
            var fadeIn = CreateTween();
            AudioPlayer.VolumeDb = FinalTrackVolume - 30;
            fadeIn.TweenProperty(AudioPlayer, "volume_db", FinalTrackVolume, Attack);
        }
        
        // We're ready to go
        base.Play();
        AudioPlayer.Play(PlayHead);
    }

    public override void Pause()
    {
        base.Pause();
        PlayHead = AudioPlayer.GetPlaybackPosition();
        AudioPlayer.Stop();
    }

    public override void Stop()
    {
        base.Stop();
        AudioPlayer.Stop();
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