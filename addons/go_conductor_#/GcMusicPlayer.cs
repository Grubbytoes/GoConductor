using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class GcMusicPlayer : Node
{
    private bool _playing;
    private float _gain;
    private float _playHead;

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

    public void PlayPause()
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