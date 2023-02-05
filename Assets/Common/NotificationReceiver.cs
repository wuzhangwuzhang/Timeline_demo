using System;
using UnityEngine;
using UnityEngine.Playables;

class NotificationReceiver: MonoBehaviour, INotificationReceiver
{
    public void OnNotify(Playable origin, INotification notification, object context)
    {
        string args = String.Empty;
        if (notification is NotificationMarker customMarkerNotification)
        {
            args = customMarkerNotification.name;

            if (string.IsNullOrEmpty(args))
            {
                return;
            }
#if UNITY_EDITOR
            double time = origin.IsValid() ? origin.GetTime() : 0.0;
            Debug.LogFormat($"Received notification of type {customMarkerNotification.GetType()}  at time {time:F3} args:{args}");
#endif
        }
        else if (notification is CustomNotificationMarker notificationMarker)
        {
            args = notificationMarker.name;
            if (string.IsNullOrEmpty(args))
            {
                return;
            }
#if UNITY_EDITOR
            double time = origin.IsValid() ? origin.GetTime() : 0.0;
            Debug.LogFormat($"Received notification of type marker at time {time:F3} args:{args}");
#endif
        }
    }
}