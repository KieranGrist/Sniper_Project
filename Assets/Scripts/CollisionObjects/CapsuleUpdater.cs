using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CapsuleUpdater : MonoBehaviour {
    public MyVector3 Min;
    public MyVector3 Max;
    public float Radius;
    // Use this for initialization
    void Start () {


    }

    // Update is called once per frame
    void Update () {
        myTransformation Transformation = GetComponent<myTransformation>();

        float CurRadius = float.MinValue;
        float MinCurRadius = float.MaxValue;


        if (Transformation.ModelMaxExtent.x > CurRadius)
        {
            CurRadius = Transformation.ModelMaxExtent.x;
        }

        if (Transformation.ModelMaxExtent.z > CurRadius)
        {
            CurRadius = Transformation.ModelMaxExtent.z;
        }


        if (Transformation.ModelMinExtent.x < MinCurRadius)
        {
            MinCurRadius = Transformation.ModelMinExtent.x;
        }

        if (Transformation.ModelMaxExtent.z < MinCurRadius)
        {
            MinCurRadius = Transformation.ModelMinExtent.z;
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



        Radius = CurRadius - MinCurRadius;
        Radius *= BiggestScale;
  

        Transformation.BoundObject = new BoundingCapsule(Transformation.Translation + (Transformation.Scale * Transformation.ModelMinExtent),
               Transformation.Translation + (Transformation.Scale * Transformation.ModelMaxExtent),
               Radius);
        BoundingCapsule Capsule2 = Transformation.BoundObject as BoundingCapsule;
        Min = Capsule2.A;
        Max = Capsule2.B;
    }
}
