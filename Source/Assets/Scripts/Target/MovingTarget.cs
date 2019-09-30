using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 

public class MovingTarget : MonoBehaviour {
     myTransformation Transformation; //Transformation Component
     public MyPhysics Physics; //Physics component
    public MyVector3 ForceApplied; //Force Applied
    public MyVector3 Origin; //Origin Point
    float DeltaTimer; //Timer
    bool firsttime = false; //Bool to see if this is the first time 
    // Use this for initialization

    void Start () {
        firsttime = false; //sets first time to false
    }

    // Update is called once per frame
    void Update () {
        if (firsttime == false)
        {
            Transformation = GetComponent<myTransformation>(); //Gets the transformations
            Physics = GetComponent<MyPhysics>(); //Gets the physic Component
            Physics.Bouncy = true; //Sets the objects to be dynamic
            Physics.Mass = FindObjectOfType<Sniper>().Mass; //sets mass to 1.01f
            Physics.Force = new MyVector3(0, 0, 0); //Sets force to 0
            Physics.Force += VectorMaths.EulerAnglesToDirection(new MyVector3(Transformation.Rotation.x, -Transformation.Rotation.y, 0)) * FindObjectOfType<Sniper>().FireSpeed;
            ForceApplied = Physics.Force; //Sets Force Applied to physics force
            firsttime = true; //Sets first time true

            Origin = Transformation.Translation; //Sets Origin to the position the object started at
        }
        Transformation.Scale = new MyVector3(20, 20, 20); //Sets the scale of the object
        Transformation = GetComponent<myTransformation>(); //Gets transformation component
        Physics = GetComponent<MyPhysics>(); //Gets the physic component
        DeltaTimer += Time.deltaTime; //Increase Delta Timer by delta time
        float Distance = (Origin - Transformation.Translation).Length(); //Distance from Origin to Object as length
        if (DeltaTimer > 60)
        {
            Destroy(this.gameObject); //Destroy the game object
            Destroy(this); //Destroy this component
        }
        if (Distance > 15000)
        {
            Destroy(this.gameObject); //Destroy the game object
            Destroy(this); //Destroy this component
        }

    }
}
