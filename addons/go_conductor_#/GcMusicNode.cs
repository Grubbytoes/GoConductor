using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public abstract partial class GcMusicNode : Node, IMusicController
{
    private float _playbackPosition;

    /// <summary>
    ///     Where in time the arrangement is at currently
    /// </summary>
    public virtual float PlaybackPosition
    {
        get
        {
            GD.PushWarning("UNIMPLEMENTED PLAYBACK POSITION GET");
            return _playbackPosition;
        }
        set => _playbackPosition = value;
    }

    /// <summary>
    ///     The point in time at which the arrangement was last played from or paused at.
    ///     Ie, where the arrangement should be playing from next time play is hit
    /// </summary>
    public bool Playing { get; set; }

    public float Gain { get; set; }

    public float PlayHead { get; set; }

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

    /// <summary>
    ///     Pauses if playing, plays if paused
    /// </summary>
    public void TogglePause()
    {
        if (Playing)
            Pause();
        else
            Play();
    }


    // TODO This isn't working w/ MusicTrack
    public virtual void PlayFrom(float position)
    {
        Pause();
        seek(position);
        Play();
    }

    /// <summary>
    ///     Sets the play head to the given position, but does NOT change the arrangement is it is currently playing.
    ///     IE, will be played from this position after the arrangement is next played from a state of pause/stop
    /// </summary>
    /// <param name="position">The position to play from</param>
    public void seek(float position)
    {
        PlayHead = position;
    }

    /// <summary>
    ///     Alias for TogglePause
    /// </summary>
    public void TogglePlay()
    {
        TogglePause();
    }

    protected void DebugPrint(string message)
    {
        var output = Name + ": - " + message;
        GD.Print(output);
    }
}