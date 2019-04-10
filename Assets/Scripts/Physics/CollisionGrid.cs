﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CollisionGrid : MonoBehaviour {
     MyVector3 WindVelocity;
     float ChangeTimer,AirResitance;
     myTransformation Transformation;
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
        Transformation.BoundObject = new AABB(
        Transformation.Translation - new MyVector3(1, 1, 1),
        Transformation.Translation + new MyVector3(1, 1, 1));
        ChangeTimer += Time.deltaTime;
		if (ChangeTimer >=10)
        {
            WindVelocity = RandomVector(-1.0f, 1.0f, -1.0f, 1.0f, -1.0f, 1.0f);
            AirResitance = Random.Range(2, 20);
            ChangeTimer = 0;
        }
    }
}
