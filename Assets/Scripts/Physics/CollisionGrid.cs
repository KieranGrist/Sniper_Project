using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CollisionGrid : MonoBehaviour {
    public MyVector3 WindVelocity;
    public float ChangeTimer,AirResitance;
    public  myTransformation Transformation;
    public MyVector3 StartPosition;
      public MyVector3 StartScale;
    void Start () {
        Transformation = GetComponent<myTransformation>();
        Transformation.Translation = StartPosition;
        Transformation.Scale = StartScale;

      
    }
	MyVector3 RandomVector(float minx, float maxx, float miny, float maxy, float minz, float maxz)
    {
        MyVector3 Ret = new MyVector3();
        Ret.x = Random.Range(minx, maxx);
        Ret.y = Random.Range(miny, maxy);
        Ret.z = Random.Range(minz, maxz);
        return Ret;
    }


	// Update is called once per frame
	void Update () {
        ChangeTimer += Time.deltaTime;
          List<MyPhysics> PhysicHandle = new List<MyPhysics>();
     PhysicHandle.AddRange(FindObjectsOfType<MyPhysics>());
        for (int i = 0; i < PhysicHandle.Count; i++)
        {
            //if (Transformation.BoundObject.Intersects(PhysicHandle[i].Transformation.BoundObject))
            //{
            //    PhysicHandle[i].Force = WindVelocity *100;
            //    PhysicHandle[i].AirResitance = AirResitance;
            //}
        }

		if (ChangeTimer >=10)
        {
            WindVelocity = RandomVector(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f);
            AirResitance = Random.Range(2, 20);
            ChangeTimer = 0;
        }
    }
}
