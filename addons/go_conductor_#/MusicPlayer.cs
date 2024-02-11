using Godot;
using System;

public  abstract partial class MusicPlayer : Node
{
    private bool _playing;
    private float _gain;
    private float _playHead;

    private bool Playing
    {
        get => _playing;
        set => _playing = value;
    }

    private float Gain
    {
        get => _gain;
        set => _gain = value;
    }

    private float PlayHead
    {
        get => _playHead;
        set => _playHead = value;
    }

    public void Play()
    {
        Playing = true;
    }

    public void Pause()
    {
        Playing = false;
    }

    public void Stop()
    {
        Playing = false;
        PlayHead = 0;
    }

    public void Restart()
    {
        Stop(); 
        Play();
    }
}
