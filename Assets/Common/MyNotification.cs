
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MyNotification : INotification
 {
     public PropertyName id { get; }
     public String clipName;
     public NotificationFlags flags => NotificationFlags.TriggerOnce;
     public MyNotification()
     {
         
     }
     
     public MyNotification(String _clipName)
     {
         clipName = _clipName;
     }
 }