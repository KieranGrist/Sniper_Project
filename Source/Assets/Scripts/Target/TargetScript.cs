using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
[System.Serializable]
public class TargetScript : MonoBehaviour
{

    MyVector3 Target = new MyVector3(); //Target Position
    float timer;  //Timer for object
    myTransformation Transformation;  //Transformation Component
    float Speed = 1;  //Speed of object
    public bool Alive = true; //Alive Variable
    public TargetSpawn TaretSpawner; //Target controler
    void Start()
    {
        Transformation = GetComponent<myTransformation>(); 
        Transformation.Translation.y = 100; //Makes sure Height is high to stop floor collision
        Target.x = 0;  //Sets target x to 0
        Target.y = 0;  //Sets target x to 0
        Target.z = 0; //Sets target z to 0
        NewPostion(); //Calls new position function
        Transformation.Scale = new MyVector3(100, 150, 10);  //Sets scale of object to be 100, 150, 10 
    }
    void NewPostion()
    {
        float MAX; //Create max
        float MIN; //Create min
        MAX = TaretSpawner.TargetDistance; //Max = Target distance
        MIN = -TaretSpawner.TargetDistance; //Min = -Target distance
        Target.x = Random.Range(MIN, MAX); //x = random value between min and max
        Target.y = Random.Range(10, MAX); //y = random value between 10 and max
        Target.z = Random.Range(MIN, MAX); //Z = random value between min and max
    }
    // Update is called once per frame
    void Update()
    {

        MyPhysics physics = GetComponent<MyPhysics>(); //Get and store the physics component
        timer += Time.deltaTime; //Add delta time to timer
        if (timer >= 3)
            if (physics.Collided == true)
            {
                Alive = false; //Set Alive to falsez
            }

        Transformation = GetComponent<myTransformation>(); //Get the object transformation and store it


        if (MyVector3.Length(Target - Transformation.Translation) < 5)
        {
            Transformation.Translation = Target; //Set translation to be target
        }
        else
        {
            if (MyVector3.Length(Target - Transformation.Translation) > 5)
            {
                Transformation.Translation = Target; //Set translation to be target
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 50)
            {
                Speed = 5; //Speed set to 5
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 500)
            {
                Speed = 10; //Speed set to 10
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 5000)
            {
                Speed = 50; //Speed set to 50
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 50000)
            {
                Speed = 100; //Speed set to 100
            }
            MyVector3 Direction = (Target - Transformation.Translation); //Direction = Target - Transllation
            Direction = VectorMaths.VectorNormalized(Direction); //Direction is equal to direction normalised
            MyVector3 Velocity = Direction * Speed; //Velocity = direction * speed
            Transformation.Translation += Velocity; //Add Velocity to current translation
        }
    }
}