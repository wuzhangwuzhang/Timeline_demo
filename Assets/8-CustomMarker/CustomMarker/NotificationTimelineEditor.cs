using UnityEngine;
using UnityEditor.Timeline;
using UnityEngine.Timeline;

[CustomTimelineEditor(typeof(CustomNotificationMarker))]
public class NotificationTimelineEditor : MarkerEditor
{
    public override MarkerDrawOptions GetMarkerOptions(IMarker marker)
    {
        if (marker is CustomNotificationMarker emitter)
        {
            var options = base.GetMarkerOptions(emitter);
            options.tooltip = $"事件参数:{emitter.name}";
            return options;
        }
        return default;
    }
}
