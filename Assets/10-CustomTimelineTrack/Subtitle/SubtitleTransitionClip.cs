using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTransitionClip: PlayableAsset
{
    public string Id;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SubtitleTransitionBehaviour>.Create(graph);
        SubtitleTransitionBehaviour behaviour = playable.GetBehaviour();
        behaviour.Id = Id;
        return playable;
    }
}