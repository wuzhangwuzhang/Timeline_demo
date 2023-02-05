using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CustomStyle("CustomNotificationMarker")]
public class CustomNotificationMarker : Marker,INotification,INotificationOptionProvider
{
    public PropertyName id { get; }
    public NotificationFlags flags
    {
        get { return NotificationFlags.Retroactive; }
    }
}