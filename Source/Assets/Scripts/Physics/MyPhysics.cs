using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
[System.Serializable]
public struct PhysicsInit
{
    public float Mass; //How much the object weighs no matter gravitational force incluing the weight
    public bool Dynamic; //If the object is effected by physics
        public bool Bouncy; //If the object bounces when colliding
}
public class MyPhysics : MonoBehaviour
{
    public MyVector3 GetMomentumAtPoint(MyVector3 point)
    {
        MyVector3 momentum = new MyVector3(); //Create momentum

        if (AngularVelocity.Length() > 0)
        {
            MyVector3 pointVelocity = Velocity + VectorMaths.VectorCrossProduct(AngularVelocity, Transformation.Translation - point); //Set point velocity to be velocity + the cross product of the angular velocity and translation - point
            momentum = Mass * pointVelocity; //Set momentum to be mass * point velocity
        }
        else
        {
            momentum = Mass * Velocity; //set momentum to be mass * velcity
        }
        return momentum; //Return momentum
    }
    public void ImpartMomentum(MyPhysics otherbody, MyVector3 ContactPoint)
    {
        MyVector3 otherMomentum = otherbody.GetMomentumAtPoint(ContactPoint); //Get momentum of contact point for the other object
        MyVector3 thisMomentum = GetMomentumAtPoint(ContactPoint); //Get momentum of contact point for this object
        MyVector3 SummedMomentum = thisMomentum + otherMomentum; //Add momentums together

        ApplyMomentum(SummedMomentum); //Apply momentum to object
        otherbody.ApplyMomentum(SummedMomentum); //Apply momentum to other object
    }
    public void ApplyMomentum(MyVector3 momentum)
    {
        Velocity += momentum / Mass; //Increaes velocity by momentum divided by mass
    }

    public void Initialise(PhysicsInit Values)
    {
        this.Mass = Values.Mass; //Set Mass to be Init Mass
        this.Dynamic = Values.Dynamic;//Set Dynamic to be Init Dynamic
        this.Bouncy = Values.Bouncy;//Set Bouncy to be Init Bouncy
    }

    public GridHandle PhysicObjectHandler; //Grid handle for physic objects
    public myTransformation Transformation; //Transformation component of object
    public MyVector3 Normal = new MyVector3(0, 0, 0); //Collision Normal
    public MyVector3 Gravity = new MyVector3(0, 0, 0); //Gravity 
    public MyVector3 Force = new MyVector3(0, 0, 0); //Force Of Object
    public MyVector3 Acceleration = new MyVector3(0, 0, 0); //Acceleration of bject
    public MyVector3 Velocity = new MyVector3(0, 0, 0); //Velocity Of object
    public MyVector3 torque = new MyVector3(0, 0, 0); //Torque of object
    public MyVector3 AngularAcceleration = new MyVector3(0, 0, 0); //Rotational Acceleration
    public MyVector3 AngularVelocity = new MyVector3(0, 0, 0); //Rotational Velocity
    public MyVector3 WeightForce; //Weight of object in gravity
    public float Mass = 1.01f; //How much the object weighs no matter gravitational force incluing the weight
    public float Push = 0.0f; //How far to push the object in collision
    public float BounceAmmount; //How bouncy the ball is
    public int ObjectId = 0; //ID of object for collision detection
    public float Inertia = 1.0f; //Intertia for rotation
    public bool Dynamic = false; //Object that has motion as effected by phyiscs at all times
    public bool Bouncy = false; //Bouncy will effect what it does when colliding
    public bool Collided = false; //bool to check if the object has collided (Can be used by other scripts)
    public bool player; //bool to check if the object is a player to skip certain physic steps
// Use this for initialization


void Start()
    {
        Normal = new MyVector3(0, 0, 0); //Set Normal to be a new vector of 0,0,0
        Gravity = new MyVector3(0, 0, 0); //Set Gravity to be a new vector of 0,0,0
        Acceleration = new MyVector3(0, 0, 0); //Set Acceleration to be a new vector of 0,0,0
        Velocity = new MyVector3(0, 0, 0); //Set Velocity to be a new vector of 0,0,0
        torque = new MyVector3(0, 0, 0); //Set torque to be a new vector of 0,0,0
        AngularAcceleration = new MyVector3(0, 0, 0); //Set AngularAcceleration to be a new vector of 0,0,0

        Push = 0.0f; //Makes push 0
        ObjectId = 0; //Make object id 0 
        Inertia = 1.0f; //Set Intertia to 1
    }
    void FixedUpdate()
    {
      
         Collided = false; //Set collided to false
        Transformation = GetComponent<myTransformation>(); //Get transformation component of object
        if (Transformation.Translation.y < -50)
        {
            Transformation.Translation.y = 50; //If object has fallen through level reset that object
        }
            if (PhysicObjectHandler != null)
        Gravity = PhysicObjectHandler.Gravity; //Set gravity to be handler gravity (Singleton)

        if (Dynamic == true|| Bouncy == true)
        {
            WeightForce = Force + (Gravity * Mass); //set weight force to be force + gravtiy * mass
            Force += Gravity * Mass; //Increase force by gravity * mass
            Acceleration = Force / Mass; //Set Acceleration to be force / mass
            
  
            Velocity += Acceleration * Time.fixedDeltaTime; //Increase velocity by acceleration * fixed delta time
            Force = new MyVector3(0, 0, 0); //sets force to 0 otherwise a force will constantly be applied ot the object
            AngularAcceleration = torque / Inertia; //set rotational acceleration to be torque / intertia
            AngularVelocity += AngularAcceleration * Time.fixedDeltaTime; //Increase angular velocity by angular acceleration * fixed delta time
            torque = new MyVector3(0, 0, 0); //Set torque to 0 otherwise object will always rotate
        }

        if (PhysicObjectHandler != null)
            for (int i = 0; i < PhysicObjectHandler.PhysicHandle.Count; i++)
            {
                if (ObjectId != PhysicObjectHandler.PhysicHandle[i].ObjectId)
                {

                    if (Transformation.BoundObject.Intersects(PhysicObjectHandler.PhysicHandle[i].Transformation.BoundObject))
                    {
                        Collided = true; //Set collided to true
                        Transformation.BoundObject.CollisionResolution(PhysicObjectHandler.PhysicHandle[i].Transformation.BoundObject, out Normal, out Push); //Get collision resoloutiuon data from the collision
                        if (Bouncy == true)
                        {
                            Transformation.Translation += Normal * (Push + 0.0000001639f); //Push the object by normal * (Push + 0.0000001639f) this stops to object getting stuck on terrain

                            Velocity = new MyVector3(-Velocity.x, -Velocity.y, -Velocity.z) * 0.9f; //Inverse velocity and times it by 0.9


                        }
                        if (Dynamic == true)
                        {
                            Transformation.Translation += Normal * (Push + 0.0000001639f); //Push the object by normal * (Push + 0.0000001639f) this stops to object getting stuck on terrain
                            Velocity = new MyVector3(0, 0, 0); //Set velocity to be 0

                        }
                    }
                }
            }
        Transformation.Translation += Velocity * Time.deltaTime; //Increase translation by velocity * delta time
        
    


        if (player == false)
        {
            Quat q = new Quat(); //Create Q
            float avMag = (AngularVelocity * Time.fixedDeltaTime).Length(); //set av mg to be the length of  angular velocity * fixed time 
            q.w = Mathf.Cos(avMag / 2); //set w to be Cos of av mag /2
            q.x = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).x / avMag; //set x to be sin of av mag / 2  * angular velocity x * fixed time / avg mag
            q.y = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).y / avMag; //set y to be sin of av mag / 2  * angular velocity y * fixed time / avg mag
            q.z = Mathf.Sin(avMag / 2) * (AngularVelocity * Time.fixedDeltaTime).z / avMag; //set z to be sin of av mag / 2  * angular velocity z * fixed time / avg mag
            Quat TargetOrienation = q * Transformation.GetRotation(); //Multiply new rotation by current rotation of object

            Transformation.SetRotation(TargetOrienation); //Set rotation of object to new rotation
            AngularVelocity *= (Time.deltaTime*2) ; //Multiply Angular Velocity by Delta time * 2
        }
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}