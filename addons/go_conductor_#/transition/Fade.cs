using System.Collections.Generic;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__.transition;

/// <summary>
/// Once started, fades one or more tracks in or out. Tracks faded out are stopped once the transition is over
///
/// This is ALL IT DOES, any updates to currently playing must be done by the parent
/// </summary>

/*
 * TODO
 * Making sure that gain is returned to its original value, might actually be a good idea to do that further up
 * TODO
 * Protect against unexpected behaviour if one transition is called in the middle of another
 */
public partial class Fade : MusicTransition
{
    public Fade(GcMusicNode parent, float duration) : base(parent, duration)
    {
    }

    public override void Start()
    {
        // Create a tween as a child of the parent
        TransitionTween = Parent.CreateTween();
        
        // Add all outgoing tracks to the tween
        foreach (var t in Outgoing)
        {
            TransitionTween.Parallel().TweenProperty(t, "Gain", -30, Duration).AsRelative();
        }
        
        // Add all incoming tracks to the tween
        foreach (var t in Incoming)
        {
            // * lower track volume
            // * start the track
            // * Add volume tweener
            t.Gain -= 30f;
            t.Play();
            TransitionTween.Parallel().TweenProperty(t, "Gain", 30, Duration).AsRelative();
        }
        
        // Add callbacks
        TransitionTween.TweenCallback(Callable.From(TransitionDone));
        
        // Let 'er rip!!!
        TransitionTween.Play();
    }

    public override void Kill()
    {
        throw new System.NotImplementedException();
    }
}