using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LookAtClip : PlayableAsset, ITimelineClipAsset
{
    // public LookAtBehaviour template = new LookAtBehaviour();
    public ExposedReference<Transform> lookAtPoint;
    public bool freezeX;
    public bool freezeY;
    public bool freezeZ;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<LookAtBehaviour>.Create(graph);
        LookAtBehaviour clone = playable.GetBehaviour();
        clone.lookAtPoint = lookAtPoint.Resolve(graph.GetResolver());
        clone.freezeX = this.freezeX;
        clone.freezeY = this.freezeY;
        clone.freezeZ = this.freezeZ;
        return playable;
    }
}
