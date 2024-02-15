using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MusicTransition : Node
{
    /// <summary>
    /// Whether or not the Transition has been started
    /// </summary>
    public bool Started { get; private set;}
    /// <summary>
    /// Track(s), if any, being faded in by the transition
    /// </summary>
    public GcMusicNode Incoming { get; private set; }
    /// <summary>
    /// Track(s), if any, being faded in by the transition
    /// </summary>
    public GcMusicNode Outgoing { get; private set; }
    /// <summary>
    /// The node which called this transition
    /// </summary>
    public GcMusicNode Parent { get; private set; }
    /// <summary>
    /// How long the transition should last for
    /// </summary>
    public float Duration { get; set; }
    
    public MusicTransition(GcMusicNode parent)
    {
        Parent = parent;
    }

    public virtual void Start()
    {
        Tween tween = GetTree().CreateTween();
    }
}