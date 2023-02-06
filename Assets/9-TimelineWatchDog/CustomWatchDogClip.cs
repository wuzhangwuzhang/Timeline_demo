using UnityEngine;
using UnityEngine.Playables;
 
public class CustomWatchDogClip : WatchableClip
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<CustomBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.AddWatch(PlayableDirector, StartTime, EndTime,ClipName);
        return playable;
    }
}