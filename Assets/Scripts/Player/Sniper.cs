using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Sniper : MonoBehaviour
{
    MyPhysics physics;
     Slider ForceSlider, MassSlider;
     Canvas UI;
     float FireSpeed, Mass;
     CamLock bfocus;
     List<Bullet> Bullets = new List<Bullet>();
     GameObject BulletModel;
     myTransformation Transformation;
    int Bullet = 1;

     List<GameObject> BulletHandler = new List<GameObject>();
     MyVector3 ForwardDirection, RightDirection;
    float PITCH, YAW, ReloadTime, BoltTime;

    bool Jumping = false, ReloadTimer = false;
    float JumpTimer = 0;
    void Start()
    {
        BoltTime = 1;
        FireSpeed = 100;
        Mass = 20;
        ForwardDirection = new MyVector3(0, 0);
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection);
    }
    void Fire()
    {
        GameObject go = Instantiate(BulletModel, new Vector3(0, 0, 0), transform.rotation);
        go.name = "Bullet";
        go.AddComponent<myTransformation>();
        go.AddComponent<Bullet>();
        go.AddComponent<MyPhysics>();
        BulletInit Temp;
        Temp.FireSpeed = FireSpeed;
        Temp.mass = Mass;
        Temp.GunPosition = Transformation.Translation;
        Temp.GunRotation = Transformation.Rotation;
        go.GetComponent<Bullet>().Init(Temp);
        Bullets.Add(go.GetComponent<Bullet>());
        BulletHandler.Add(go);
        bfocus.BFocus = go.GetComponent<Bullet>();
        bfocus.BulletCam = true;
    }
    void Reload()
    {
        if (ReloadTime <= 1)
        {
            ReloadTime += Time.deltaTime;
        }
        else
        {
            ReloadTime = 0;
            Bullet = 500;
            ReloadTimer = false;
        }
    }
    void Update()
    {
        Transformation = GetComponent<myTransformation>();
        physics = GetComponent<MyPhysics>();
        ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW));
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection);
        RightDirection = VectorMaths.VectorNormalized(RightDirection);  
        PITCH += -Input.GetAxis("Mouse Y");
        YAW += Input.GetAxis("Mouse X");

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            physics.Force += ForwardDirection * 1000;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {

            physics.Force += RightDirection * 1000;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {

            physics.Force -= ForwardDirection * 1000;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {

            physics.Force -= RightDirection * 1000;
        }

        Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0);
    }


}


/*
void Update()
{

   Transformation = GetComponent<myTransformation>();

    Transformation.BoundObject = new BoundingCapsule(
      new MyVector3(Transformation.Translation.x, Transformation.Translation.y - Feat, Transformation.Translation.z),
      new MyVector3(Transformation.Translation.x, Transformation.Translation.y + Head, Transformation.Translation.z),
     2);

    PITCH += -Input.GetAxis("Mouse Y");
    YAW += Input.GetAxis("Mouse X");

  //  FireSpeed = ForceSlider.value;
   //     Mass = MassSlider.value;
        for (int i = 0; i < Bullets.Count; i++)
        {
            if (Bullets[i].GetComponent<Bullet>().Alive == false)
            {
                Destroy(Bullets[i]);
                Bullets.Remove(Bullets[i]);
                Destroy(BulletHandler[i]);
                BulletHandler.Remove(BulletHandler[i]);
                bfocus.BulletCam = false;
            }
            if (Bullets[i].GetComponent<Bullet>().timeoutDestructor >= 30)
            {
                Destroy(Bullets[i]);
                Bullets.Remove(Bullets[i]);
                Destroy(BulletHandler[i]);
                BulletHandler.Remove(BulletHandler[i]);
                bfocus.BulletCam = false;
            }
        }

    if (UI.enabled == false)
    {
        if (ReloadTimer == false)
        {


            if (BoltTime >= 0)
            {

                BoltTime -= Time.deltaTime;

            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    if (BoltTime <= 0)
                    {
                        Fire();
                        Bullet -= 1;
                        BoltTime = 1;
                    }

                }
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            ReloadTimer = true;
        }
        if (ReloadTimer == true)
        {
            Reload();

        }
    }
        ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW));
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection);
  if (Input.GetKey(KeyCode.Space))
    {
        if (Jumping == false)
        {
            Jumping = true;
        }
    }

//if (Transformation.CollisionCapsule.Intersects(Floor.CollisionBox))
//    {
//        Debug.Log("it worked");
//    }
//  else
//    {
//        Velocity.y -= 0.05f;
//    }
  if (Jumping == true)
    {
        Velocity.y += 0.01f;
    }
  if (JumpTimer >= 0.1)
    {
        Jumping = false;
    }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {

        Velocity += ForwardDirection * 10;
        }
       if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
       {

        Velocity += RightDirection * 10;
      }
    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {

        Velocity -= ForwardDirection * 10;
    }
    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
    {

        Velocity -= RightDirection * 10;
    }

        Transformation.Rotation = new MyVector3(-PITCH,-YAW, 0);

        Transformation.Translation.x += Velocity.x * Time.deltaTime;
        Transformation.Translation.y += Velocity.y;
        Transformation.Translation.z += Velocity.z * Time.deltaTime;
        Velocity *= Time.deltaTime;

    }
    */
