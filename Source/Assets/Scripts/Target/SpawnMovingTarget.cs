using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

public class SpawnMovingTarget : MonoBehaviour
{
    public GameObject cube; //Game object for targets
    public bool Playercontrol; //bool to see if the player has control or not
    public myTransformation Transformation; //Transformation Component of object
    public MyVector3 ForwardDirection; //Direction object is facing
    public MyVector3 RightDirection; //Direction to the right of object
    public float timebetweenobjects =1.5f;
    public float Timer; //Timer for spawning
    public float PITCH; //Pitch Of Objecty
    public float YAW; //Yaw Rotation Of Object
    public float Distance; //Distance from player to spawner
    void CreateCube()
    {
        Transformation = GetComponent<myTransformation>(); //Gets Transfion Component of object
        GameObject go = Instantiate(cube, new Vector3(0, 0, 0), transform.rotation); //Creates Game Object
        go.name = "Moving Target"; //Sets name of object
        go.AddComponent<myTransformation>(); //Add Transformation component
        go.AddComponent<BoxUpdater>(); //Add box updater component
        TransformationInit Temp; //Initalisation Structure
        Temp.Translation = Transformation.Translation; //Rotation of Translation 
        Temp.Rotation = Transformation.Rotation; //Rotation of Object
        Temp.Scale = new MyVector3(2, 2, 2); //Set scale to be 2,2,2
        go.GetComponent<myTransformation>().Initialise(Temp); //Initialise Transformation with initalise struct
        go.AddComponent<MyPhysics>(); //Add Physics Component 

        go.AddComponent<MovingTarget>(); //Add moving target component

    }
    // Use this for initialization
    void Start()
    {
        CreateCube(); //Create a target
    }

    // Update is called once per frame
    void Update()
    {
        ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW)); //Get Forward direction of  player
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection); //Get cross product of up and forward direction
        RightDirection = VectorMaths.VectorNormalized(RightDirection); //Normalise Right direction so if the player looks directly up they can still move left and right

        if (FindObjectOfType<Sniper>().UI.enabled == false)
        {
            if (Playercontrol == true)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    Playercontrol = false; //sets player control to false
                }

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    Transformation.Translation += ForwardDirection * 2; //Increase force by Forward direction * 1000  
                }
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {

                    Transformation.Translation += RightDirection * 2; //Increase force by Right direction * 1000 
                }
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {

                    Transformation.Translation -= ForwardDirection * 2;  //decrease force by forward direction * 1000 
                }
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {

                    Transformation.Translation -= RightDirection * 2; //decrease force by right direction * 1000
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    Transformation.Translation += new MyVector3(0, 1, 0) * 2; //Increase force by Up * 100 
                }
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    Transformation.Translation -= new MyVector3(0, 1, 0) * 2; //Increase force by Up * 100 
                }
                PITCH += -Input.GetAxis("Mouse Y"); //Increases pitch by mouse rotation y 
                YAW += Input.GetAxis("Mouse X"); //Increase Yaw by Mouse x

                PITCH = Mathf.Clamp(PITCH, -90, 90); //Clamp the players look down and up rotatiopn 
                FindObjectOfType<CamLock>().SFocus = GetComponent<myTransformation>(); //Sets the cameera focus to spawner transformation
            }
            else
            {
                FindObjectOfType<CamLock>().SFocus = FindObjectOfType<Sniper>().Transformation; //Set the camera focus to sniper transformation
            }
            Transformation = GetComponent<myTransformation>(); //Get transformation of object
            Timer += Time.deltaTime; //Increases timer by delta tim
            if (Timer >= timebetweenobjects)
            {
                if (Playercontrol == true)
                {
                    if (Input.GetMouseButton(0))
                    {
                        CreateCube(); //Creates a target 
                        Timer = 0; //Sets timer to 0
                    }
                }
                else
                {
                    CreateCube(); //Creates a target 
                    Timer = 0; //Sets timer to 0
                }

            }
            Distance = (FindObjectOfType<Sniper>().Transformation.Translation - Transformation.Translation).Length(); //Gets the distance between the sniper and the object
            if (Distance < 1000)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    Playercontrol = true; //Allows the player to control the target spawner
                }
            }
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0); //Sets Rotation to be inverse of pitch and yaw, for some reason unity has their mouse input set weirdly
        }
    }
}