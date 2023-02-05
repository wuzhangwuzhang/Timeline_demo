using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NotificationMarker : Marker, INotification
{
    public PropertyName id { get; }
    [SerializeField] public String Args;
}