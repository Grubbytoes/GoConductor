namespace GoConductorPlugin.addons.go_conductor__.transition;

/// <summary>
/// Easiest thing in the world, just straight stops one track and starts the other
/// </summary>
class HardCut : MusicTransition
{
    public HardCut(GcMusicNode parent, float duration) : base(parent, duration)
    {
    }
}