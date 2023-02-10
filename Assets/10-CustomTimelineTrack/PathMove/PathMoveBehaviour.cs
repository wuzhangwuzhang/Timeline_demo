/*
 * Clip的行为控制逻辑
 */
using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Playables;

public class PathMoveBehaviour : PlayableBehaviour
{
    /// <summary>
    /// 路点
    /// </summary>
    public List<Vector3> WayPoints;
    /// <summary>
    /// 轨道的绑定对象
    /// </summary>
    public Transform TargetTrans;
    private TweenerCore<Vector3, Path, PathOptions> tweenCore;

    private readonly int run = Animator.StringToHash("Run");
    private bool isFirstFrame = true;
    private bool isComplete = false;

    private int wayPointIndex = 0;
    [NonSerialized]
    public string displayName;

    private Vector3 playerOriginAngle = Vector3.zero;
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (isFirstFrame)
        {
            bool isCanRotate = true;
            bool isPlayer = 0 == string.Compare(TargetTrans.name, "Player");
            if (!string.IsNullOrEmpty(displayName))
            {
                var args = displayName.Split('|');
                if (args.Length > 1 && args[1] == "0")
                {
                    isCanRotate = false;
                }
            }
            if (0 == WayPoints.Count)
            {
                Debug.LogWarning("路点不存在!!!");
                return;
            }
            float timeDuration = (float)playable.GetDuration();
            Debug.Log($"PathMoveBehaviour timeDuration:{timeDuration} {TargetTrans?.name}");
        
            List<Vector3> wayPoints = new List<Vector3>();
            wayPoints.Clear();
            wayPoints.AddRange(WayPoints);

            var child = TargetTrans.GetChild(0);
            if (child)
            {
                var angle = child.eulerAngles;
                if (angle.x != 0 || angle.z != 0)
                {
                    playerOriginAngle = child.eulerAngles;
                    //Debug.Log($"x:{playerOriginAngle.x} y:{playerOriginAngle.y} z:{playerOriginAngle.z}");
                }
            }
            tweenCore = TargetTrans.DOPath(wayPoints.ToArray(),timeDuration).SetSpeedBased(false);//.SetEase(Ease.Linear)
            tweenCore.OnStart(() =>
            {
                isComplete = false;
                Debug.Log("OnStart");
                if (isCanRotate)
                {
                    if (isPlayer)
                    {
                        TargetTrans.GetChild(0).LookAt(wayPoints[0],Vector3.up);
                    }
                    else
                    {
                        TargetTrans.LookAt(wayPoints[0],Vector3.up);
                    }
                }
                var sphereCollider = TargetTrans.GetComponent<SphereCollider>();
                if (sphereCollider)
                {
                    sphereCollider.enabled = false;
                }
                
                // var capsuleCollider = TargetTrans.GetComponent<CapsuleCollider>();
                // if (capsuleCollider)
                // {
                //     capsuleCollider.enabled = false;
                // }
                //Debug.Log("OnStart,关闭碰撞");
            });
            
            // tweenCore.onPause = () =>
            // {  
            //     Debug.Log("onPause");
            // };
            
            tweenCore.onComplete = () =>
            {
                isComplete = true;
                //Debug.Log("onComplete,还原碰撞");
                // TargetTrans.localRotation= Quaternion.identity;
                // var sphereCollider = TargetTrans.GetComponent<SphereCollider>();
                // if (sphereCollider)
                // {
                //     sphereCollider.enabled = true;
                // }
                // var capsuleCollider = TargetTrans.GetComponent<CapsuleCollider>();
                // if (capsuleCollider)
                // {
                //     capsuleCollider.enabled = true;
                // }
            };
            
            tweenCore.onWaypointChange = delegate(int index)
            {
                if (isCanRotate)
                {
                    wayPointIndex = index + 1;
                    if (wayPointIndex >= WayPoints.Count)
                    {
                        wayPointIndex = WayPoints.Count - 1;
                    }
                    if (isPlayer)
                    {
                        TargetTrans.GetChild(0).LookAt(WayPoints[wayPointIndex],Vector3.up);
                    }
                    else
                    {
                        TargetTrans.LookAt(WayPoints[wayPointIndex],Vector3.up);
                    }
                }
               
                // int nextPointIds = index + 1;
                // Debug.Log($"PathMoveBehaviour WayPoint Index:{index} Next:{nextPointIds}");
                // var nextIndex = nextPointIds;
                // if (nextIndex > WayPoints.Count)
                // {
                //     nextIndex = WayPoints.Count;
                // }
                // var nextPoint = WayPoints[nextIndex];
                // Vector3 vT = nextPoint - TargetTrans.localPosition;
                // TargetTrans.localRotation =  Quaternion.LookRotation(vT);
                //TargetTrans.LookAt(WayPoints[nextPointIds]);
                //Debug.Log($"<color=yellow>PathMoveBehaviour OnStart:{TargetTrans.name} 朝向第{nextPointIds}个点: {nextPoint}</color>");
            };

            tweenCore.SetUpdate(UpdateType.Normal);
            tweenCore.onUpdate = () =>
            {
                // if (!isComplete)
                // {
                //     TargetTrans.DOLookAt(WayPoints[wayPointIndex],0.5f,AxisConstraint.Y,null);
                // }
                // TargetTrans.LookAt(WayPoints[wayPointIndex]);
                if (isPlayer)
                {
                    Transform playerModelTrans = TargetTrans.GetChild(0);
                    if (playerModelTrans)
                    {
                        var localRotation = TargetTrans.localRotation;
                        playerModelTrans.rotation = Quaternion.Euler(new Vector3(playerOriginAngle.x, playerModelTrans.eulerAngles.y, playerOriginAngle.z)) * localRotation * Quaternion.Inverse(localRotation);
                    }
                }
            };
            tweenCore.SetId<TweenerCore<Vector3, Path, PathOptions>>("pathMove");
            isFirstFrame = false;
        }

        if (TargetTrans != null)
        {
            DOTween.Play("pathMove");    
        }
        else
        {
            Debug.LogError("The control target is null!");
        }
    }
    
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        // DOTween.Pause("pathMove");
    }

    public override void OnGraphStop(Playable playable)
    {
        if (tweenCore != null)
        {
            tweenCore.Kill();
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        tweenCore = null;
        wayPointIndex = 0;
    }
}
