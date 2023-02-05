using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Timeline;

namespace CustomMarkerTrack
{
    [CustomTimelineEditor(typeof(CustomMarkerClip))]
    public class CustomMarkClipTools : ClipEditor
    {
        public override void OnCreate(TimelineClip clip, TrackAsset track, TimelineClip clonedFrom)
        {
            base.OnCreate(clip, track, clonedFrom);
            clip.duration = 0.2f;
            clip.displayName = "MarkClip";
        }

        public override ClipDrawOptions GetClipOptions(TimelineClip clip)
        {
            var option = base.GetClipOptions(clip);
            option.tooltip = "这是一个自定义的Clip!";
            option.highlightColor = Color.red;
            return option;
        }
    }
}