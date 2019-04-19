using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Sniper : MonoBehaviour
{
    MyPhysics physics;
    public Slider ForceSlider;
    public Slider MassSlider;
    public Canvas UI;
    public float FireSpeed, Mass;
    public CamLock bfocus;
    List<Bullet> Bullets = new List<Bullet>();
    public GameObject BulletModel;
    myTransformation Transformation;

    List<GameObject> BulletHandler = new List<GameObject>();
    MyVector3 ForwardDirection, RightDirection;
    float PITCH, YAW;
    float Bolttimer;
    void Start()
    {
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
        go.AddComponent<BoxUpdater>();
        BulletInit Temp;
        Temp.FireSpeed = FireSpeed;
        Temp.mass = Mass;
        Temp.GunPosition = Transformation.Translation * RightDirection * 1;
        Temp.GunPosition.y += 1;
        Temp.GunRotation = Transformation.Rotation;
        go.GetComponent<Bullet>().Init(Temp);
        Bullets.Add(go.GetComponent<Bullet>());
        BulletHandler.Add(go);
    }
    void Update()
    {

        FireSpeed = ForceSlider.value;
        Mass = MassSlider.value;
        Transformation = GetComponent<myTransformation>();
        physics = GetComponent<MyPhysics>();
        ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW));
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection);
        RightDirection = VectorMaths.VectorNormalized(RightDirection);
        PITCH += -Input.GetAxis("Mouse Y");
        YAW += Input.GetAxis("Mouse X");
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

        Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0);


        for (int i = 0; i < Bullets.Count; i++)
        {
            if (Bullets[i].GetComponent<Bullet>().Alive == false)
            {
                Destroy(Bullets[i]);
                Bullets.Remove(Bullets[i]);
                Destroy(BulletHandler[i]);
                BulletHandler.Remove(BulletHandler[i]);
            }
            if (Bullets[i].GetComponent<Bullet>().timeoutDestructor >= 30)
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
                    Bolttimer = 0.5f;
                }
            }
       
        }
    }
}