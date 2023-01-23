using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostEventBase : MonoBehaviour
{
    protected float _angle = 80f;

    protected void ShakePlayerCamera(Collider player)
    {
        CamShake shakeSrc = player.GetComponentInChildren<CamShake>();
        if (shakeSrc != null)
        {
            shakeSrc.StartShaking();
        }
    }

    protected bool CheckPlayerAngle(Transform eventPos, Transform targetPos)
    {
        Quaternion look = Quaternion.LookRotation(targetPos.position, eventPos.position);
        float betwenAngle = Quaternion.Angle(targetPos.rotation, eventPos.rotation);

        Debug.Log(betwenAngle);

        if (betwenAngle > this._angle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
