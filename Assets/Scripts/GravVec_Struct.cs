using UnityEngine;

public struct GravVec
{
    public Vector2 Force;
    public int Level;

    public GravVec(Vector2 f, int l)
    {
        Force = f;
        Level = l;
    }

}