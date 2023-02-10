using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class LookAtBehaviour : PlayableBehaviour
{
    public Transform lookAtPoint;

    public bool freezeX = false;
    public bool freezeY = true;
    public bool freezeZ = false;
} 
