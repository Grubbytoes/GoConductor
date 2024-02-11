using System;
using System.Collections.Generic;

namespace GoConductorPlugin.addons.go_conductor__;

public partial class MultiMusicPlayer : GcMusicNode
{
    private Dictionary<String, GcMusicNode> TrackDictionary { get; set; }

    public new void _Ready()
    {
        foreach (var child in GetChildren())
        {
            if (child.IsClass("GcMusicNode"))
            {
                TrackDictionary.Add(child.Name, (GcMusicNode)child);
            }
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