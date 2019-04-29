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
    float timer;
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


        //Apply a force in the direction of the gun and times it by the firing speed
        Physics.Force += VectorMaths.EulerAnglesToDirection(new MyVector3(Initiate.GunRotation.x, -Initiate.GunRotation.y, 0)) * Initiate.FireSpeed;
        Physics.Bouncy = true;
        //Force Bullet to be alive
        Alive = true;
    }

    // Use this for initialization
    void Start()
    {

        timeoutDestructor = 0;
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Sniper sniper = FindObjectOfType<Sniper>();
     
        Transformation = GetComponent<myTransformation>();
        Physics = GetComponent<MyPhysics>();
        //Update Components
        if (sniper.Explosion == true)
            if (Physics.Collided == true)
            { 
                GameObject go = new GameObject("Explosion");
                go.AddComponent<Explosion>();
                go.AddComponent<myTransformation>();

                ExplosionInit Exp;
                Exp.ExplosionRadius = 100;
                Exp.ExplosionStrength = 1000;
                go.GetComponent<Explosion>().Init(Exp);

                TransformationInit Temp;
                Temp.Translation = this.Transformation.Translation;
                Temp.Scale = this.Transformation.Scale;
                Temp.Rotation = this.Transformation.Rotation;
                go.GetComponent<myTransformation>().Initialise(Temp);
            }
        //Add Delta Time TO timeout Destructor
        timeoutDestructor += Time.deltaTime;
    }
}
