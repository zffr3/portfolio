using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializablePlayerData 
{
    public SerializableVector4 PlayerPosition { get; private set; }
    public SerializableVector4 PlayerRotation { get; private set; }

    public string[] HandWeapons { get; private set; }

    public float HealthCount { get; private set; }
    public int XpCount { get; private set; }

    public Ranks PlayerRank { get; private set; }

    public float WalkSpeed { get; private set; }
    public float SprintForce { get; private set; }
    public float JumpForce { get; private set; }

    public float DamageForce { get; private set; }
    public float BulletPercent { get; private set; }
    public int BulletId { get; private set; }

    public SerializablePlayerData(SerializableVector4 poistionVector, SerializableVector4 rotationVector, string[] hands, float health, int xp, Ranks rank, float wSpeed, float sForce, float jForce, float dForce, float bPercent, int id)
    {
        this.PlayerPosition = poistionVector;
        this.PlayerRotation = rotationVector;

        this.HandWeapons = hands;

        this.HealthCount = health;
        this.XpCount = xp;

        this.PlayerRank = rank;

        this.WalkSpeed = wSpeed;
        this.SprintForce = sForce;
        this.JumpForce = jForce;

        this.DamageForce = dForce;
        this.BulletPercent = bPercent;
        this.BulletId = id;
    }
}

[System.Serializable]
public struct SerializableVector4
{
    public float X;
    public float Y;
    public float Z;
    public float W;

    public Vector3 GetVector3()
    {
        return new Vector3(this.X, this.Y, this.Z);
    }

    public Quaternion GetQuaternion()
    {
        return new Quaternion(this.X, this.Y, this.Z, this.W);
    }

    public SerializableVector4(Vector3 tVector)
    {
        this.X = tVector.x;
        this.Y = tVector.y; 
        this.Z = tVector.z;
        this.W = 0;
    }

    public SerializableVector4(Quaternion tQuaternion)
    {
        this.X = tQuaternion.x;
        this.Y = tQuaternion.y;
        this.Z = tQuaternion.z;
        this.W = tQuaternion.w;
    }
}
