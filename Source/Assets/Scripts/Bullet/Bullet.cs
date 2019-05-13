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
public class Bullet : MonoBehaviour
{
    public bool Alive = true; //Bool to check if the bullet is still alive 
    public float timeoutDestructor; //Time bullet has been alive 
    myTransformation Transformation; //Transformation Component
    public MyPhysics Physics; //Physics Component
    //Used to Initialise Bullet
    public void Init(BulletInit Initiate)
    {
        //Get Components
        Physics = GetComponent<MyPhysics>(); //Gets Physics component from object (Makes sure physics is up to date)
        Transformation = GetComponent<myTransformation>(); //Gets Transformation Component from object (Makes sure transformation is up to date)

        //Reset Timeout
        timeoutDestructor = 0;

        //Set Scale
        Transformation.Scale = new MyVector3(2,2,2);

        //Set Physical Mass
        Physics.Mass = Initiate.mass;

        //Set Rotation And Translation
        Transformation.Translation = Initiate.GunPosition;


        //Apply a force in the direction of the gun and times it by the firing speed
        Physics.Force += VectorMaths.EulerAnglesToDirection(new MyVector3(Initiate.GunRotation.x, -Initiate.GunRotation.y, 0)) * Initiate.FireSpeed;
        Physics.Bouncy = true;
        //Force Bullet to be alive
        Alive = true;
    }

    // Use this for initialization
    void Start()
    {
        timeoutDestructor = 0; //Sets timeout to be 0
    }

    // Update is called once per frame
    void Update()
    {
        Sniper sniper = FindObjectOfType<Sniper>(); //As there is only one player you can just find the sniper object

        Physics = GetComponent<MyPhysics>(); //Gets Physics component from object (Makes sure physics is up to date)
        Transformation = GetComponent<myTransformation>(); //Gets Transformation Component from object (Makes sure transformation is up to date)

        //Checks if timer is more then 0.2, this stops explosions being instant
        if (timeoutDestructor > 0.2f)
        {
            //Checks if the player is creating an explosion
            if (sniper.Explosion == true)
            {
                //If physic object has collided run these scripts
                if (Physics.Collided == true)
                {
                  
                    GameObject go = new GameObject("Explosion"); //Create a new invisble game object called explosion
                    go.AddComponent<Explosion>(); //Add Explosion 
                    go.AddComponent<myTransformation>(); //Add Transformation

                    TransformationInit Temp; //Create an initiation structre for transformation 
                    Temp.Translation = this.Transformation.Translation; //Set translalation 
                    Temp.Translation.y -= 20; //Decrease height, this makes the explosion more impactfull
                    Temp.Scale = this.Transformation.Scale; //Sets scale
                    Temp.Rotation = this.Transformation.Rotation; //sets rotation
                    go.GetComponent<myTransformation>().Initialise(Temp); //Sets the transformation to initation structure
                    timeoutDestructor = 10000; //Kill the bullet
                }
            }
        }
        //Add Delta Time TO timeout Destructor
        timeoutDestructor += Time.deltaTime;
    }
}
