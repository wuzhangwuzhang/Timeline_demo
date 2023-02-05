using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CustomMarkerNotification : INotification
{
    public PropertyName id { get; }
    public NotificationFlags flags => NotificationFlags.TriggerOnce;

    public readonly string ClipName;
    public readonly string OwnerName;

    public CustomMarkerNotification() { }

    public CustomMarkerNotification(string clipName,string ownerName)
    {
        ClipName = clipName;
        OwnerName = ownerName;
    }
}