using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace CustomMarkerTrack
{
    [TrackBindingType(typeof(NotificationReceiver))]
    [TrackClipType(typeof(CustomMarkerClip))]
    public class CustomMarkerTrack : TrackAsset
    {
        public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        {
            base.GatherProperties(director, driver);
            // GameObject go = GameObject.Find("TimelineGo");
            // if (go)
            // {
            //     NotificationReceiver notificationReceiver = go.GetOrAddComponent<NotificationReceiver>();
            //     // director.SetGenericBinding(this,notificationReceiver);
            // }
            // using (var outputs = timelineAsset.outputs.GetEnumerator())
            // {
            //     while (outputs.MoveNext())
            //     {
            //         PlayableBinding curTrack = outputs.Current;
            //         var trackObject = curTrack.sourceObject;
            //         if (trackObject is Tantawowa.TimelineEvents.TimelineEventTrack )
            //         {
            //             Tantawowa.TimelineEvents.TimelineEventTrack timelineEventTrack = curTrack.sourceObject as Tantawowa.TimelineEvents.TimelineEventTrack;
            //             if (!(timelineEventTrack is null))
            //             {
            //                 var clips = timelineEventTrack.GetClips().ToList();
            //                 foreach (var clip in clips)
            //                 {
            //                     #region Clip Copy
            //
            //                     // var tmpClip = CreateDefaultClip();
            //                     // tmpClip.start = clip.start;
            //                     // tmpClip.duration = clip.duration;
            //                     // tmpClip.displayName = clip.displayName;
            //                     
            //                     // if (tmpClip.blendInCurveMode != TimelineClip.BlendCurveMode.Manual)
            //                     // {
            //                     //     tmpClip.blendInCurveMode = TimelineClip.BlendCurveMode.Manual;
            //                     //     tmpClip.mixInCurve = AnimationCurve.Linear(0, 0, 1, 1);
            //                     // }
            //                     //
            //                     // if (tmpClip.blendOutCurveMode != TimelineClip.BlendCurveMode.Manual)
            //                     // {
            //                     //     tmpClip.blendOutCurveMode = TimelineClip.BlendCurveMode.Manual;
            //                     //     tmpClip.mixOutCurve = AnimationCurve.Linear(0, 1, 1, 0);
            //                     // }
            //
            //                     #endregion
            //
            //                     #region Clip Cast Marker
            //                     var marker = CreateMarker(typeof(NotificationMarker), (clip.start + clip.duration * 0.5f));
            //                     NotificationMarker nMarker = marker as NotificationMarker;
            //                     if (!(nMarker is null)) nMarker.name = clip.displayName;
            //                     #endregion
            //                 }
            //             }
            //
            //             break;
            //             // if (director != null)
            //             // {
            //             //     EditorUtility.SetDirty(director.playableAsset);
            //             // }
            //             // AssetDatabase.SaveAssets();
            //         }
            //     }
            // }
        }

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            // var director = go.GetComponent<PlayableDirector>();
            //获取轨道上的所有Clip,并将clip名字作为参数传过去
            foreach (var clip in GetClips())
            {
                var playableAsset = clip.asset as CustomMarkerClip;
                if (playableAsset)
                {
                    if (!(playableAsset is null))
                    {
                        playableAsset.ClipName = clip.displayName;
                    }
                }
            }
            var scriptPlayable = ScriptPlayable<CustomMarkerBehaviour>.Create(graph, inputCount);
            return scriptPlayable;
        }
    }
}
