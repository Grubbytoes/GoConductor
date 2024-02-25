using System;
using System.Collections.Generic;
using Godot;

namespace GoConductorPlugin.addons.go_conductor__;

public abstract partial class MultiMusicPlayer : GcMusicNode
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
            DebugPrint("adding " + child.Name);
            TrackDictionary.Add(child.Name, child as GcMusicNode);
        }
    }

    public abstract bool Cue(String trackName);

    
    /// <summary>
    /// Finds the track of the given name
    /// </summary>
    /// <param name="trackName">The name of the track</param>
    /// <returns>The track, or null if it cannot be found</returns>
    public GcMusicNode GetTrack(String trackName)
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

    /// <summary>
    /// Returns an enumerator of all child tracks, 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<GcMusicNode> GetAllTracks()
    {
        return TrackDictionary.Values;
    }
}