﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Sniper : MonoBehaviour
{
    public bool Explosion;
    public MyPhysics physics;
    public Slider ForceSlider;
    public Slider MassSlider;
    public Canvas UI;
    public float FireSpeed, Mass;
    public CamLock bfocus;
    List<Bullet> Bullets = new List<Bullet>();
    public GameObject BulletModel;
    public myTransformation Transformation;
    public MyVector3 GunOffset, GunPosition;
    List<GameObject> BulletHandler = new List<GameObject>();
    public MyVector3 ForwardDirection, RightDirection;
    public float PITCH, YAW;
    public float Bolttimer;
    void Start()
    {
        FireSpeed = 100;
        Mass = 20;
        ForwardDirection = new MyVector3(0, 0);
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection);
        MassSlider.value = 1.01F;
    }
    void Fire()
    {
        Transformation = GetComponent<myTransformation>();
        GameObject go = Instantiate(BulletModel, new Vector3(0, 0, 0), transform.rotation);
        go.name = "Bullet";
        go.AddComponent<myTransformation>();
        go.AddComponent<BoxUpdater>();
        go.AddComponent<MyPhysics>();
        go.AddComponent<Bullet>();
        BulletInit Temp;
        Temp.FireSpeed = FireSpeed;
        Temp.mass = Mass;
        GunPosition = Transformation.Translation + GunOffset;
        Temp.GunPosition = GunPosition;

        Temp.GunRotation = Transformation.Rotation;
        go.GetComponent<Bullet>().Init(Temp);
        Bullets.Add(go.GetComponent<Bullet>());
        BulletHandler.Add(go);
 
    }
    void Update()
    {

        if (FindObjectOfType<SpawnMovingTarget>().Playercontrol == false)
        {
            FireSpeed = ForceSlider.value;
            Mass = MassSlider.value;
            Transformation = GetComponent<myTransformation>();
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0);
            physics = GetComponent<MyPhysics>();
            physics.player = true;
            physics.torque = new MyVector3(0, 0, 0);
            physics.AngularAcceleration = new MyVector3(0, 0, 0);
            physics.AngularVelocity = new MyVector3(0, 0, 0);
            ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW));
            RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection);
            RightDirection = VectorMaths.VectorNormalized(RightDirection);

            if (Transformation.Translation.y > 100)
            {
                physics.Force.y -= 100;
            }
            if (Transformation.Translation.y > 1000)
            {
                physics.Force.y -= 500;
            }

            GunOffset = RightDirection * 2;
            GunOffset.y += 0;
            GunPosition = Transformation.Translation + GunOffset;


            PITCH += -Input.GetAxis("Mouse Y");
            YAW += Input.GetAxis("Mouse X");
            Mathf.Clamp(YAW, -90, 90);
            PITCH = Mathf.Clamp(PITCH, -90, 90);
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
            if (Input.GetKey(KeyCode.Space))
            {
                physics.Force += new MyVector3(0, 1, 0) * 100;
            }
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0);

            if (Bullets.Count > 0)
                for (int i = 0; i < Bullets.Count; i++)
                {
                    if (Bullets[i].GetComponent<Bullet>().Alive == false)
                    {
                        Destroy(Bullets[i]);
                        Bullets.Remove(Bullets[i]);
                        Destroy(BulletHandler[i]);
                        BulletHandler.Remove(BulletHandler[i]);
                    }
                    if (Bullets[i].GetComponent<Bullet>().timeoutDestructor >= 50)
                    {
                        Destroy(Bullets[i]);
                        Bullets.Remove(Bullets[i]);
                        Destroy(BulletHandler[i]);
                        BulletHandler.Remove(BulletHandler[i]);
                    }
                }
            Bolttimer -= Time.deltaTime;
            if (UI.enabled == false)
            {
                if (Bolttimer < 0)
                {
                    if (Input.GetMouseButton(0))
                    {

                        Fire();
                        Bolttimer = 1;
                    }
                }

            }
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0);
        }
    }
}