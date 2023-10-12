using UnityEngine;

/// <summary>
/// Struct added to pass vectors around but with a little more data for ease/cleanliness.
/// We hold the force and gravity level this force is working with
/// </summary>
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