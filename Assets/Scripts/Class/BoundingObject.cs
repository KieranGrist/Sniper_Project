using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BoundingObject
{
    public BoundingObject()
    {
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public abstract bool Intersects(BoundingObject RHS);

    public override string ToString()
    {
        return base.ToString();
    }
}