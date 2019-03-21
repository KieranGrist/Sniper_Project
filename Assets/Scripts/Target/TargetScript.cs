using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
    
    public MyVector3 Target = new MyVector3();
    public float TargetScale =10;
    public int TIMESCOMPLETE = 0;
    myTransformation Transformation;
    public BoundingObject TarCol;
   public float Speed = 1;
    public bool Alive = true;
    public Sniper sniper;
    public TargetSpawn TaretSpawner;
    public myTransformation Floor;
    void Start()
    {
        Transformation = GetComponent<myTransformation>();
        Transformation.BoundObject = new AABB(new MyVector3(-1, -1, -1), new MyVector3(1, 1, 1));
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
        for (int i = 0; i < sniper.Bullets.Count; i++)
        {
            if (Transformation.BoundObject.Intersects(sniper.Bullets[i].Transformation.BoundObject))
            {
                Alive = false;
                GameObject.Find("Sniper").GetComponent<Sniper>().Bullets[i].GetComponent<Bullet>().Alive = false;
            }
        }



    
        TarCol = new AABB(new MyVector3(Target.x - TargetScale, Target.y - TargetScale, Target.z - TargetScale), new MyVector3(Target.x + TargetScale, Target.y + TargetScale, Target.z + TargetScale));

        Transformation = GetComponent<myTransformation>();
        if (TarCol.Intersects(Floor.BoundObject))
        {
            NewPostion();
        }
        if (TarCol.Intersects(sniper.Transformation.BoundObject))
        {
            NewPostion();
        }
        if (Transformation.BoundObject.Intersects(TarCol)) 
        {
            Speed = 0;
        }
        else
        {
            if (MyVector3.Length(Target- Transformation.Translation) > 50)
            {
                Speed = 5;
            }
            if (MyVector3.Length(Target- Transformation.Translation) > 500)
            {
                Speed = 10;
            }
            if (MyVector3.Length(Target- Transformation.Translation) > 5000)
            {
                Speed = 50;
            }
            if (MyVector3.Length(Target- Transformation.Translation) > 50000)
            {
                Speed = 100;
            }
            MyVector3 Direction = (Target- Transformation.Translation);
            Direction = VectorMaths.VectorNormalized(Direction);
            MyVector3 Velocity = Direction * Speed;
            Transformation.Translation += Velocity;
        }





  
    }
}
        