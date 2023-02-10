using UnityEngine;
using UnityEngine.Playables;


public class SmoothRotateClip : PlayableAsset
{
    [Header("角色朝向")] 
    public Vector3 RotateVector3;
    public Transform bindingTarget;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<SmoothRotateBehavoiur>.Create(graph);
        SmoothRotateBehavoiur behaviour = playable.GetBehaviour();
        behaviour.TargetTrans = bindingTarget;
        behaviour.RotateVector3 = RotateVector3;
        return playable;
    }
}