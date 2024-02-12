using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public abstract partial class GcMusicNode : Node, IMusicController
{
    private bool _playing;
    private float _gain;
    private float _playHead;
    private float _playbackPosition;

    public virtual float PlaybackPosition
    {
        get
        {
            GD.PushWarning("UNIMPLEMENTED PLAYBACK POSITION GET");
            return _playbackPosition;
        }
        private set => _playbackPosition = value;
    }

    public bool Playing
    {
        get => _playing;
        set => _playing = value;
    }

    public float Gain
    {
        get => _gain;
        set => _gain = value;
    }

    public float PlayHead
    {
        get => _playHead;
        set => _playHead = value;
    }

    public virtual void Play()
    {
        Playing = true;
    }

    public virtual void Pause()
    {
        Playing = false;
    }

    public virtual void Stop()
    {
        Playing = false;
        PlayHead = 0;
    }

    public void Restart()
    {
        Stop(); 
        Play();
    }
    
    public void PlayFrom(float position)
    {
        Stop();
        PlayHead = position;
        Play();
    }

    public void TogglePause()
    {
        if (Playing)
        {
            Pause();
        }
        else
        {
            Play();
        }
    }
}