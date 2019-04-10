using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SphereUpdate : MonoBehaviour {

    public float Radius;
    public MyVector3 CenterPoint;
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
        if (Transformation.ModelMaxExtent.y > CurRadius)
        {
            CurRadius = Transformation.ModelMaxExtent.y;
        }
        if (Transformation.ModelMaxExtent.z > CurRadius)
        {
            CurRadius = Transformation.ModelMaxExtent.z;
        }


        if (Transformation.ModelMinExtent.x < MinCurRadius)
        {
            MinCurRadius = Transformation.ModelMinExtent.x;
        }
        if (Transformation.ModelMinExtent.y < MinCurRadius)
        {
            MinCurRadius = Transformation.ModelMinExtent.y;
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
        if (Transformation.Scale.y > BiggestScale)
        {
            BiggestScale = Transformation.Scale.y;
        }
        if (Transformation.Scale.z > BiggestScale)
        {
            BiggestScale = Transformation.Scale.z;
        }



        Radius = CurRadius - MinCurRadius;
        Radius *= BiggestScale;
        Transformation.BoundObject = new BoundingCircle(Transformation.Translation, Radius);
        BoundingCircle Circle1 = Transformation.BoundObject as BoundingCircle;
        CenterPoint = Circle1.CenterPoint;
        Radius = Circle1.Radius;
    }
}
