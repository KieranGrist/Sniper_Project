using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereUpdate : MonoBehaviour {

    public float Radius;

    public MyVector3 Center, Half;

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


        Transformation.BoundObject = new BoundingCircle(Transformation.Translation, Radius);
        BoundingCircle Circle2 = Transformation.BoundObject as BoundingCircle;

        MyVector3 MinExtent, MaxExtent;
        MinExtent = Circle2.CenterPoint - Circle2.Radius;
        MaxExtent = Circle2.CenterPoint + Circle2.Radius;
        Circle2.Half = (MaxExtent - MinExtent) * 0.5f;
        Half = Circle2.Half;
        Center = Circle2.CenterPoint;
    }
}
