using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MusicTrack : GcMusicNode
{
    private AudioStreamPlayer AudioPlayer { get; set; }

    public override float PlaybackPosition => AudioPlayer.GetPlaybackPosition();

    public override void Play()
    {
        if (Playing) {return;}
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

    public override void _Ready()
    {
        AudioPlayer = (AudioStreamPlayer)GetChild(0);
        
    }
}