using UnityEngine;
using UnityEngine.Playables;

//旁白click
public class SubtitleNarratorClip: PlayableAsset
{
    public string Id;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleNarratorBehaviour>.Create(graph);
        SubtitleNarratorBehaviour behaviour = playable.GetBehaviour();
        behaviour.Id = Id;
        return playable;
    }
}