using System;
using UnityEngine;
using UnityEngine.Playables;

public class LookAtMixerBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Transform trackBinding = playerData as Transform;
        if (trackBinding == null)
        {
            return;
        }

        int inputCount = playable.GetInputCount();
        bool hasLookAtPos = false;
        Vector3 targetLookAtPos = Vector3.zero;
        // float rotationTotalWeight = 0f;

        // Quaternion blendedRotation = new Quaternion(0f, 0f, 0f, 0f);

        for (int i = 0; i < inputCount; i++)
        {
            ScriptPlayable<LookAtBehaviour> playableInput = (ScriptPlayable<LookAtBehaviour>)playable.GetInput(i);
            LookAtBehaviour input = playableInput.GetBehaviour();

            if (input.lookAtPoint == null)
            {
                continue;
            }
            // trackBinding.LookAt(input.lookAtPoint.position);
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight == 0)
            {
                continue;
            }
            Vector3 tempVec3 = input.lookAtPoint.position;
            if (input.freezeX)
            {
                tempVec3.x = trackBinding.transform.position.x;
            }
            if (input.freezeY)
            {
                tempVec3.y = trackBinding.transform.position.y;
            }
            if (input.freezeZ)
            {
                tempVec3.z = trackBinding.transform.position.z;
            }
            // Debug.Log(inputWeight);
            targetLookAtPos += tempVec3 * inputWeight;
            hasLookAtPos = true;

            //    var lookDir = input.lookAtPoint.position - trackBinding.position;
            //    if (input.freezeX)
            //    {
            //        lookDir.x = 0;
            //    }
            //    if (input.freezeY)
            //    {
            //        lookDir.y = 0;
            //    }
            //    if (input.freezeZ)
            //    {
            //        lookDir.z = 0;
            //    }
            //    rotationTotalWeight += inputWeight;
            //    Quaternion desiredRotation = Quaternion.LookRotation(lookDir);// trackBinding.rotation;//
            //    if (lookDir != Vector3.zero)
            //    {
            //        desiredRotation = Quaternion.LookRotation(lookDir);
            //    }

            //    desiredRotation = NormalizeQuaternion(desiredRotation);

            //    if (Quaternion.Dot(blendedRotation, desiredRotation) < 0f)
            //    {
            //        desiredRotation = ScaleQuaternion(desiredRotation, -1f);
            //    }

            //    desiredRotation = ScaleQuaternion(desiredRotation, inputWeight);

            //    blendedRotation = AddQuaternions(blendedRotation, desiredRotation);
        }
        if (hasLookAtPos)
        {
            trackBinding.LookAt(targetLookAtPos);
            //trackBinding.rotation = blendedRotation;
        }
    }

    static Quaternion AddQuaternions(Quaternion first, Quaternion second)
    {
        first.w += second.w;
        first.x += second.x;
        first.y += second.y;
        first.z += second.z;
        return first;
    }

    static Quaternion ScaleQuaternion(Quaternion rotation, float multiplier)
    {
        rotation.w *= multiplier;
        rotation.x *= multiplier;
        rotation.y *= multiplier;
        rotation.z *= multiplier;
        return rotation;
    }

    static float QuaternionMagnitude(Quaternion rotation)
    {
        return Mathf.Sqrt((Quaternion.Dot(rotation, rotation)));
    }

    static Quaternion NormalizeQuaternion(Quaternion rotation)
    {
        float magnitude = QuaternionMagnitude(rotation);

        if (magnitude > 0f)
            return ScaleQuaternion(rotation, 1f / magnitude);

        Debug.LogWarning("Cannot normalize a quaternion with zero magnitude.");
        return Quaternion.identity;
    }
}
