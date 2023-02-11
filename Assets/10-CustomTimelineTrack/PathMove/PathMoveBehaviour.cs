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

public class PathMoveBehaviour : WatchableCustomPlayableBehaviour
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

    private int wayPointIndex = 0;
    private Vector3 playerOriginAngle = Vector3.zero;
    

    /// <summary>
    /// Clip开始
    /// </summary>
    public override void OnWatchStart()
    {
        DoAction();
    }


    /// <summary>
    /// Clip结束
    /// </summary>
    public override void OnWatchComplete()
    {
        
    }

    private void DoAction()
    {
        bool isCanRotate = true;
        bool isPlayer = 0 == string.Compare(TargetTrans.name, "Player");
        if (!string.IsNullOrEmpty(DisplayName))
        {
            var args = DisplayName.Split('|');
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

        float timeDuration = (float) m_playable.GetDuration();
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

        tweenCore = TargetTrans.DOPath(wayPoints.ToArray(), timeDuration).SetSpeedBased(false).SetLookAt(0); //.SetEase(Ease.Linear)
        tweenCore.OnStart(() =>
        {
            if (isCanRotate)
            {
                if (isPlayer)
                {
                    TargetTrans.GetChild(0).LookAt(wayPoints[0], Vector3.up);
                }
                else
                {
                    TargetTrans.LookAt(wayPoints[0], Vector3.up);
                }
            }

            var sphereCollider = TargetTrans.GetComponent<SphereCollider>();
            if (sphereCollider)
            {
                sphereCollider.enabled = false;
            }
        });

        tweenCore.onComplete = () =>
        {
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
                    TargetTrans.GetChild(0).LookAt(WayPoints[wayPointIndex], Vector3.up);
                }
                else
                {
                    TargetTrans.LookAt(WayPoints[wayPointIndex], Vector3.up);
                }
            }
        };

        tweenCore.SetUpdate(UpdateType.Normal);
        tweenCore.onUpdate = () =>
        {
            if (isPlayer)
            {
                Transform playerModelTrans = TargetTrans.GetChild(0);
                if (playerModelTrans)
                {
                    var localRotation = TargetTrans.localRotation;
                    playerModelTrans.rotation =
                        Quaternion.Euler(new Vector3(playerOriginAngle.x, playerModelTrans.eulerAngles.y,
                            playerOriginAngle.z)) * localRotation * Quaternion.Inverse(localRotation);
                }
            }
        };
        tweenCore.SetId<TweenerCore<Vector3, Path, PathOptions>>("pathMove"); ;
        if (TargetTrans != null)
        {
            DOTween.Play("pathMove");
        }
        else
        {
            Debug.LogError("The control target is null!");
        }
    }
    
    public override void OnPlayableDestroy(Playable playable)
    {
        wayPointIndex = 0;
        if (tweenCore != null)
        {
            tweenCore.Kill();
            tweenCore = null;
        }
    }
}