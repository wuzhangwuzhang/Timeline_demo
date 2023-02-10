using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.855f, 0.8623f, 0.870f)]
[TrackClipType(typeof(LookAtClip))]
[TrackBindingType(typeof(Transform))]
public class LookAtTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<LookAtMixerBehaviour>.Create(graph, inputCount);
    }

    
}
