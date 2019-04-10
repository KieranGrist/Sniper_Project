using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//Init Structure For Bullet
public struct BulletInit
{
    public MyVector3 GunPosition; //Position Of Gun
    public MyVector3 GunRotation; //Rotation Of Gun
    public float FireSpeed; //Speed To fire bullet at
       public float mass; //Weight Of Bullet
}

//Class For bullet objects
public class Bullet : MonoBehaviour {
   

    public bool Alive = true; //Bool to check if the bullet is still alive 
     float timeoutDestructor; //Time bullet has been alive 
    public  myTransformation Transformation; //Transformation Component
    public MyPhysics Physics; //Physics Component

    //Used to Initialise Bullet
    public void Init(BulletInit Initiate)
    {
//Get Components
        Physics = GetComponent<MyPhysics>();
        Transformation = GetComponent<myTransformation>();

        //Reset Timeout
        timeoutDestructor = 0;

        //Set Scale
        Transformation.Scale = new MyVector3(5.5f, 5.5f, 5.5f);

        //Set Physical Mass
        Physics.Mass = Initiate.mass;

        //Set Rotation And Translation
        Transformation.Translation = Initiate.GunPosition;
        Transformation.Rotation = Initiate.GunRotation;

        //Apply a force in the direction of the gun and times it by the firing speed
        Physics.Force = VectorMaths.EulerAnglesToDirection(new MyVector3(-Transformation.Rotation.x, Transformation.Rotation.y, 0)) * Initiate.FireSpeed;

        //Force Bullet to be alive
        Alive = true;
    }

    public Bullet()
    {
        //Get Components
        Physics = GetComponent<MyPhysics>();
        Transformation = GetComponent<myTransformation>();

        //Initialise timeout to 0
        timeoutDestructor = 0;

        //Set alive to false
        Alive = false;
    }

    // Use this for initialization
    void Start()
    {
        Physics = GetComponent<MyPhysics>();
        Transformation = GetComponent<myTransformation>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update Components
        Transformation = GetComponent<myTransformation>();
        Physics = GetComponent<MyPhysics>();

        //Add Delta Time TO timeout Destructor
          timeoutDestructor += Time.deltaTime;

        //If The bullet has fallen through the level kill the bullet
        if (Transformation.Translation.y <= 0)
        {
            Alive = false;
        }
    }
}
