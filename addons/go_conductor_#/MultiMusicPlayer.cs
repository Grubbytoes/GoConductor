using System;
using System.Collections.Generic;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MultiMusicPlayer : GcMusicNode
{
    private Dictionary<String, GcMusicNode> TrackDictionary { get; set; }

    public override void _EnterTree()
    {
        base._EnterTree();
        TrackDictionary = new Dictionary<string, GcMusicNode>();
    }

    public override void _Ready()
    {
        foreach (var child in GetChildren())
        {
            GD.Print(String.Format("Now looking at child '{0}'", child.Name));
            
            GD.Print("  - valid!");
            TrackDictionary.Add(child.Name, child as GcMusicNode);
        }
    }

    protected GcMusicNode GetTrack(String trackName)
    {
        // I haven't been using c# long enough to know exatly what this does
        // but my IDE said it was a good idea
        return TrackDictionary.TryGetValue(trackName, out var track) ? track : null;
        /*
        Here's what it looked like before:
        
        if (TrackDictionary.TryGetValue(trackName, out var track))
        {
            return track;
        }
        else
        {
            return null;
        }
        */
    }
}