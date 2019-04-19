using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct PhysicsInit
{
    public float Mass;
    public bool Dynamic, Bouncy;
}
public class MyPhysics : MonoBehaviour
{

    public void Initialise(PhysicsInit Values)
    {
        this.Mass = Values.Mass;
        this.Dynamic = Values.Dynamic;
        this.Bouncy = Values.Bouncy;
    }

    public GridHandle PhysicObjectHandler;
    public myTransformation Transformation;

    public MyVector3 Normal = new MyVector3(0, 0, 0);
    public MyVector3 Gravity = new MyVector3(0, 0, 0);
    public MyVector3 Force = new MyVector3(0, 0, 0);
    public MyVector3 Acceleration = new MyVector3(0, 0, 0);
    public MyVector3 Velocity = new MyVector3(0, 0, 0);
    public MyVector3 torque = new MyVector3(0, 0, 0);
    public MyVector3 AngularAcceleration = new MyVector3(0, 0, 0);
    public MyVector3 AngularVelocity = new MyVector3(0, 0, 0);
    public float Mass = 1.01f;
    public float AirResitance = 1.01f;
    public float Push = 0.0f;
    public int ObjectId = 0;
    public float Inertia = 1.0f;
    public bool Dynamic = false;
    public bool Bouncy = false; //Object that has motion and is effected by physics, if false object is static at all times, Bouncy will effect what it does when colliding, Static does not move the object at all
    public bool Collided = false;
// Use this for initialization


void Start()
    {
        Normal = new MyVector3(0, 0, 0);
        Gravity = new MyVector3(0, 0, 0);
        Acceleration = new MyVector3(0, 0, 0);
        Velocity = new MyVector3(0, 0, 0);
        torque = new MyVector3(0, 0, 0);
        AngularAcceleration = new MyVector3(0, 0, 0);
        AngularVelocity = new MyVector3(0, 0, 0);
        Mass = 1.01f;
        AirResitance = 1.01f;
        Push = 0.0f;
        ObjectId = 0;
        Inertia = 1.0f;
    }
    void FixedUpdate()
    {
      
         Collided = false;
        Transformation = GetComponent<myTransformation>();
        if (Transformation.Translation.y < -50)
        {
            Transformation.Translation.y = 50;
        }
            if (PhysicObjectHandler != null)
        Gravity = PhysicObjectHandler.Gravity;

        if (Dynamic == true|| Bouncy == true)
        {
            Force += Gravity;
            Acceleration = Force / Mass ;
            Velocity += Acceleration / AirResitance * Time.deltaTime;

            Force = new MyVector3(0, 0, 0);
            //WindVelocity = CurrentGrid.WindVelocity;
            //AirResitance = CurrentGrid.AirResitance;

            AngularAcceleration = torque / Inertia;
            AngularVelocity += AngularAcceleration * Time.fixedDeltaTime;
        }

        if (PhysicObjectHandler != null)
            for (int i = 0; i < PhysicObjectHandler.PhysicHandle.Count; i++)
            {
                if (ObjectId != PhysicObjectHandler.PhysicHandle[i].ObjectId)
                {
                 
                    if (Transformation.BoundObject.Intersects(PhysicObjectHandler.PhysicHandle[i].Transformation.BoundObject))
                    {
                        Collided = true;
                        Transformation.BoundObject.CollisionResolution(PhysicObjectHandler.PhysicHandle[i].Transformation.BoundObject, out Normal, out Push);
                    if (Bouncy == true)
                    {
                        Transformation.Translation += Normal * (Push + 0.0000001639f);
                        Velocity =new MyVector3(-Velocity.x,-Velocity.y,-Velocity.z) *0.7f;
                    }
                    if (Dynamic == true)
                    {
                        Transformation.Translation += Normal * (Push + 0.0000001639f);
                        Velocity = new MyVector3(0, 0, 0);
                    }
                }
            }
        }
        Transformation.Translation += Velocity * Time.deltaTime;
        Velocity /= AirResitance;

        //Quat q = new Quat();

        //float avMag = (AngularVelocity * Time.fixedDeltaTime).Length();
        //q.w = Mathf.Cos(avMag / 2);
        //q.x = Mathf.Sin(avMag / 2) * (AngularVelocity.x * Time.fixedDeltaTime) / avMag;
        //q.y = Mathf.Sin(avMag / 2) * (AngularVelocity.y * Time.fixedDeltaTime) / avMag;
        //q.z = Mathf.Sin(avMag / 2) * (AngularVelocity.z * Time.fixedDeltaTime) / avMag;
        //Quat TargetOrienation = q * Transformation.GetRotation();
        //Transformation.SetRotation(TargetOrienation);
    }
    // Update is called once per frame
    void Update()
    {
        /*
         * 
         * 
         * Inbetween fixed steps find out time between steps which would be 0.02seconds if it is 50 fps then over the course of 0.02s lerp between A to B 
         * */


    }
}