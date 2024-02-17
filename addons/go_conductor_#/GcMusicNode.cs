using System;
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
        set => _playbackPosition = value;
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
    
    // TODO This isn't working w/ MusicTrack
    public void PlayFrom(float position)
    {
        Stop();
        PlayHead = position;
        Play();
    }
    
    /// <summary>
    /// Pauses if playing, plays if paused
    /// </summary>
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

    /// <summary>
    /// Alias for TogglePause
    /// </summary>
    public void TogglePlay()
    {
        TogglePause();
    }

    protected void DebugPrint(String message)
    {
        String output = Name + ": - " + message;
        GD.Print(output);
    }
}