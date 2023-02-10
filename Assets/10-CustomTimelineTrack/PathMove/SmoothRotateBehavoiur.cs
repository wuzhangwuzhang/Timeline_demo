/*
 * Clip的行为控制逻辑
 */
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

public class SmoothRotateBehavoiur : PlayableBehaviour
{
    public Vector3 RotateVector3;
    private bool _isFirstFrame = true;
    public Transform TargetTrans;
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (_isFirstFrame)
        {
            bool isPlayer = 0 == string.Compare(TargetTrans.name, "Player");
            float timeDuration = (float)playable.GetDuration();
#if !CLOSE_LOG
            Debug.Log($"SmoothRotateBehavoiur timeDuration:{timeDuration} TargetTrans.Name:{TargetTrans?.name}  RotateVector3：{RotateVector3}"); //!CLOSE_LOG
#endif
            Transform playerModel  = TargetTrans;
            if (isPlayer)
            {
                playerModel = TargetTrans.GetChild(0); 
            }
             
            if (playerModel)
            {
                var eulerAngles = playerModel.eulerAngles;
                var tweenCore = playerModel.DORotate( new Vector3(eulerAngles.x,RotateVector3.y,eulerAngles.z), timeDuration, RotateMode.Fast);
//                 tweenCore.OnStart(() =>
//                 {
// #if !CLOSE_LOG
//                     Debug.Log($"{TargetTrans?.name} 开始转向目标"); //!CLOSE_LOG
// #endif
//                 });
//             
//                 tweenCore.onUpdate = () =>
//                 {
//                     Debug.Log($"update:{TargetTrans.eulerAngles}");
//                 };
//             
//                 tweenCore.onComplete = () =>
//                 {
// #if !CLOSE_LOG
//                     Debug.Log($"{TargetTrans?.name} 朝向目标结束"); //!CLOSE_LOG
// #endif
//                 };
            
                _isFirstFrame = false;
                tweenCore.SetId("SmoothRotate");
            }
        }

        if (TargetTrans != null)
        {
            DOTween.Play("SmoothRotate");    
        }
        else
        {
#if !CLOSE_ERR
        Debug.LogError("The control target is null!"); //!CLOSE_LOG
#endif
        }
    }
    
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        // DOTween.Pause("SmoothRotate");
    }

    public override void OnPlayableDestroy(Playable playable)
    {
      
    }
}
