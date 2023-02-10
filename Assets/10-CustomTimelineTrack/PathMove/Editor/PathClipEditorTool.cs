#if UNITY_EDITOR
using DG.Tweening;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

[ExecuteInEditMode]
[CustomEditor(typeof(PathMoveClip))]
public class PathClipEditorTool : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("导出路点"))
        {
            PathMoveClip pathMoveClip = (PathMoveClip) target;
            DOTweenPath tweenPath = pathMoveClip.tweenPathEditor.Resolve(TimelineEditor.inspectedDirector.playableGraph.GetResolver());
            if (tweenPath != null)
            {
                pathMoveClip.wayPoints.Clear();
                int index = 0;
                foreach (var wp in tweenPath.wps)
                {
                    ++index;
                    Debug.Log($"路点:{index}  x:{wp.x}  y:{wp.y} z:{wp.z}");
                }
                pathMoveClip.wayPoints.AddRange(tweenPath.wps);
            }
            else
            {
                Debug.LogError("PathMoveClip上没有找到绑定DOTweenPath的组件");
            }
        }
        if (GUILayout.Button("清空路点"))
        {
            PathMoveClip pathMoveClip = (PathMoveClip) target;
            pathMoveClip.wayPoints.Clear();
        }
    }

}
#endif