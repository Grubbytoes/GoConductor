using Godot;

namespace GoConductorPlugin.addons.go_conductor__.transition;

public abstract partial class MusicTransition : Node
{
    public GcMusicNode Parent { get; private set; }
    /// <summary>
    /// How long the transition should last for
    /// </summary>
    public float Duration { get; set; }
    
    public MusicTransition(GcMusicNode parent, float duration)
    {
        Parent = parent;
        Duration = duration;
    }

    public abstract void Start();

    public abstract void Kill();

}