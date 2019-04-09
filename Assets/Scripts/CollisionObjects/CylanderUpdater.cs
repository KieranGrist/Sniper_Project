using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylanderUpdater : MonoBehaviour {
    public MyVector3 Min;
    public MyVector3 Max;
    public MyVector3 Center;
    public MyVector3 Half;
    public float Radius;
    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update () {
        myTransformation Transformation = GetComponent<myTransformation>();
        float CurRadius = float.MinValue;
        if (Transformation.ModelMaxExtent.x > CurRadius)
        {
            CurRadius = Transformation.ModelMaxExtent.x;
        }
        if (Transformation.ModelMaxExtent.z > CurRadius)
        {
            CurRadius = Transformation.ModelMaxExtent.z;
        }


        float BiggestScale = float.MinValue;
        if (Transformation.Scale.x > BiggestScale)
        {
            BiggestScale = Transformation.Scale.x;
        }
        if (Transformation.Scale.z > BiggestScale)
        {
            BiggestScale = Transformation.Scale.z;
        }


        Radius = BiggestScale * CurRadius;


        Transformation.BoundObject = new BoundingCapsule(Transformation.Translation + (Transformation.Scale * Transformation.ModelMinExtent),
               Transformation.Translation + (Transformation.Scale * Transformation.ModelMaxExtent),
               Radius);
        BoundingCapsule boundingCapsule = Transformation.BoundObject as BoundingCapsule;
        Min = boundingCapsule.A;
        Max = boundingCapsule.B;
        Half = (boundingCapsule.B - boundingCapsule.A) * 0.5f;
        Center = Half + boundingCapsule.A;
        boundingCapsule.Center = Center;
        boundingCapsule.Half = Half;
    }
}
