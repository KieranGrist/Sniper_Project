using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TargetScript : MonoBehaviour {
    
     MyVector3 Target = new MyVector3();
    float timer;
    myTransformation Transformation;
    BoundingObject TarCol;
    float Speed = 1;
    public bool Alive = true;
   public TargetSpawn TaretSpawner;
    void Start() { 
        Transformation = GetComponent<myTransformation>();
        Transformation.Translation.y = 100;
        TarCol = new AABB(new MyVector3(-3, -3, -3), new MyVector3(3, 3, 3));
        Target.x = 0;
        Target.y = 0;
        Target.z = 0;
        NewPostion();
        Transformation.Scale = new MyVector3(100, 150, 10);
    }
    void NewPostion()
    {
            float MAX = 10;
             float MIN = 10;
          MAX = TaretSpawner.TargetDistance;
        MIN = -TaretSpawner.TargetDistance;
        Target.x = Random.Range(MIN, MAX);
        Target.y = Random.Range(10, MAX);
        Target.z = Random.Range(MIN, MAX);
    }
    // Update is called once per frame
    void Update()
    {

        MyPhysics physics = GetComponent<MyPhysics>();
        timer += Time.deltaTime;
        if (timer >=3)
        if (physics.Collided == true)
        {
            Alive = false;
        }

        Transformation = GetComponent<myTransformation>();


        if (Transformation.BoundObject.Intersects(TarCol))
        {
            Speed = 0;
        }
        else
        {
            if (MyVector3.Length(Target - Transformation.Translation) > 5)
            {
                Transformation.Translation = Target;
            }
             if (MyVector3.Length(Target - Transformation.Translation) > 50)
            {
                Speed = 5;
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 500)
            {
                Speed = 10;
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 5000)
            {
                Speed = 50;
            }
            if (MyVector3.Length(Target - Transformation.Translation) > 50000)
            {
                Speed = 100;
            }
            MyVector3 Direction = (Target - Transformation.Translation);
            Direction = VectorMaths.VectorNormalized(Direction);
            MyVector3 Velocity = Direction * Speed;
            Transformation.Translation += Velocity;
        }






    }
}
        