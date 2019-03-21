using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myRigidBody : MonoBehaviour {
    //This is specifically For bullets Do not use for anything else but the bullets
    public MyVector3 Force;
    public  MyVector3 Gravity, WindVeloicty;
    public MyVector3 acceleartion, velocity ;
    public float Mass =1,AirReistance =2;
    public myTransformation Transformation;
    // CurrentAir;
    // Use this for initialization
    void Start ()
    {



    }
	

	// Update is called once per frame
	void Update () {
        //This will be rolled into Global Physics
        Gravity.y = -9.3f;
        Transformation = GetComponent<myTransformation>();
      //  for(int i = 0; i < GameObject.Find("Atmosphere").GetComponent<Atmosphere>().airpocket.Count; i++)
      //  {
            
      //     // if (AABB.Intersects(Transformation.CollisionBox, GameObject.Find("Atmosphere").GetComponent<Atmosphere>().airpocket[i].Transformation.CollisionBox))
      //       {
      ////          CurrentAir = GameObject.Find("Atmosphere").GetComponent<Atmosphere>().airpocket[i];
      //      }

      //  }

//WindVeloicty = CurrentAir.WindVelocity;
       // AirReistance = CurrentAir.AirResitance;
        acceleartion = Force / Mass;
        velocity += ((acceleartion + Gravity + WindVeloicty) / AirReistance) * Time.deltaTime;
        Transformation.Translation +=  velocity * Time.deltaTime;
        Transformation.Rotation= VectorMaths.DirectionToEuler(VectorMaths.VectorNormalized(velocity));
        Force = new MyVector3(0, 0, 0);

    }
}
