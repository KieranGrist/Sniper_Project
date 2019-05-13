using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMovingTarget : MonoBehaviour {
    public GameObject cube;
    public bool Playercontrol;
    public myTransformation Transformation;
    public MyVector3 ForwardDirection;
    public float Timer;
    public float PITCH, YAW, Distance;
    void CreateCube()
    {
        Transformation = GetComponent<myTransformation>();
        GameObject go = Instantiate(cube, new Vector3(0, 0, 0), transform.rotation);
        go.name = "Moving Target";
        go.AddComponent<myTransformation>();
        go.AddComponent<BoxUpdater>();
        TransformationInit Temp;
        Temp.Translation = Transformation.Translation;
        Temp.Rotation = Transformation.Rotation;
        Temp.Scale = new MyVector3(2, 2, 2);
        go.GetComponent<myTransformation>().Initialise(Temp);
        go.AddComponent<MyPhysics>();
        go.AddComponent<MovingTarget>();

    }
    // Use this for initialization
    void Start () {
        CreateCube();
    }
	
	// Update is called once per frame
	void Update () {
        ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW));
        if (Playercontrol == true)
        {
            if (Input.GetKey(KeyCode.R))
            {
                Playercontrol = false;
            }

            PITCH += -Input.GetAxis("Mouse Y");
            YAW += Input.GetAxis("Mouse X");
            Mathf.Clamp(YAW, -90, 90);
            PITCH = Mathf.Clamp(PITCH, -90, 90);
            FindObjectOfType<CamLock>().SFocus = GetComponent<myTransformation>();
        }
        else
        {
            FindObjectOfType<CamLock>().SFocus = FindObjectOfType<Sniper>().Transformation;
        }
        Transformation = GetComponent<myTransformation>();
        Timer += Time.deltaTime;
        if (Timer >= 2)
        {
            if (Playercontrol == true)
            {
                if (Input.GetMouseButton(0))
                {
                    CreateCube();
                    Timer = 0;
                }
            }
            else
            {
                CreateCube();
                Timer = 0;
            }
 
        }
        Distance = (FindObjectOfType<Sniper>().Transformation.Translation - Transformation.Translation).Length();
   if (Distance < 30)
        { 
            if (Input.GetKey(KeyCode.E))
            {
                Playercontrol = true;
            }
        }
        Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0);
    }
}
