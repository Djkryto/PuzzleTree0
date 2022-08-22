using System;
using UnityEngine;

[Serializable]
public struct Position
{
    public float X;
    public float Y;
    public float Z;

    public Position(Vector3 itemPosition)
    {
        X = itemPosition.x;
        Y = itemPosition.y;
        Z = itemPosition.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(X, Y, Z);
    }
}