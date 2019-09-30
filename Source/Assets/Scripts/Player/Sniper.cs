using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
[System.Serializable]
public class Sniper : MonoBehaviour
{
    public bool Explosion; //Explosion bool
    public MyPhysics physics; //physics component of object
    public Slider ForceSlider; //slider for force
    public Slider MassSlider; //Slider for mass
    public Canvas UI; //UI reference
    public float FireSpeed; //Speed bullet fires at
    public float Mass; //Mass of object
    public CamLock bfocus; //Camera script reference
    List<Bullet> Bullets = new List<Bullet>(); //List of bullets
    public GameObject BulletModel; //Model of bullet
    public myTransformation Transformation; //Transformation Component
    public MyVector3 GunOffset; //Gun Offset
    public MyVector3 GunPosition; //Gun Position
    List<GameObject> BulletHandler = new List<GameObject>(); //Game object list of bullets  
    public MyVector3 ForwardDirection; //Direction player is facing
    public MyVector3 RightDirection; //Direction to the right of the player 
    public float PITCH; //rotation up down
    public float YAW; //Rotation left to right
    public float Bolttimer; //Timer for bolt 
    void Start()
    {
        FireSpeed = 100; //Set firing speed to 100
        Mass = 20; //Set mass to 20
        ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(0, 0)); //Convert pitch and yaw into a direction
        RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection); //Get cross product of up and forward direction
        RightDirection = VectorMaths.VectorNormalized(RightDirection); //Normalise Right direction so if the player looks directly up they can still move left and right
        MassSlider.value = 1.01F; //Set mass to be 1.01 
    }
    void Fire()
    {
        Transformation = GetComponent<myTransformation>(); //Get transformation componenet
        GameObject go = Instantiate(BulletModel, new Vector3(0, 0, 0), transform.rotation); //Create game object
        go.name = "Bullet"; //Change name of game object to be bullet
        go.AddComponent<myTransformation>(); //Add transformation componet
        go.AddComponent<BoxUpdater>(); //Add box collider component
        go.AddComponent<MyPhysics>(); //Add physics component
        go.AddComponent<Bullet>(); //Add Bullet component
        BulletInit Temp; //Initialise structure
        Temp.FireSpeed = FireSpeed; //Set firing speed
        Temp.mass = Mass; //Set mass
        GunPosition = Transformation.Translation + GunOffset; //Set gun postion to be offset of the players location
        Temp.GunPosition = GunPosition; //Set postion of bullet
        Temp.GunRotation = Transformation.Rotation; //Set rotation of bullet
        go.GetComponent<Bullet>().Init(Temp); //Intialise bullet with init structure 
        Bullets.Add(go.GetComponent<Bullet>()); //add bullet component to bullet list 
        BulletHandler.Add(go); //Add game object to bullet game object list
 
    }
    void Update()
    {

        if (FindObjectOfType<SpawnMovingTarget>().Playercontrol == false)
        {
            FireSpeed = ForceSlider.value; //fireing speed is equal to slider value
            Mass = MassSlider.value; //mass is equal to slider value
            Transformation = GetComponent<myTransformation>(); //Get Transformatio component  
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0); //Set rotation to be inverse of pitch and yaw
            physics = GetComponent<MyPhysics>(); //Get physic component
            physics.player = true; //Set player to true
            ForwardDirection = VectorMaths.EulerAnglesToDirection(new MyVector2(PITCH, YAW)); //Convert pitch and yaw into a direction
            RightDirection = VectorMaths.VectorCrossProduct(MyVector3.up, ForwardDirection); //Get cross product of up and forward direction
            RightDirection = VectorMaths.VectorNormalized(RightDirection); //Normalise Right direction so if the player looks directly up they can still move left and right

            if (Transformation.Translation.y > 100)
            {
                physics.Force.y -= 100; //Decrease force by100
            }
            if (Transformation.Translation.y > 1000)
            {
                physics.Force.y -= 500; //Decrease Force y by 500
            }

            GunOffset = RightDirection * 2; //Set gun offset to the right of the player
            GunPosition = Transformation.Translation + GunOffset; //Offet player postition by gun offset and set that value to gun positon  


            PITCH += -Input.GetAxis("Mouse Y"); //Increase pitch by mouse y axis
            YAW += Input.GetAxis("Mouse X"); //Increase yaw by mouse x axis
            PITCH = Mathf.Clamp(PITCH, -90, 90); //Clamp pitch 
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                physics.Force += ForwardDirection * 1000; //Increase force by Forward direction * 1000  
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {

                physics.Force += RightDirection * 1000; //Increase force by Right direction * 1000 
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {

                physics.Force -= ForwardDirection * 1000;  //decrease force by forward direction * 1000 
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {

                physics.Force -= RightDirection * 1000; //decrease force by right direction * 1000
            }
            if (Input.GetKey(KeyCode.Space))
            {
                physics.Force += new MyVector3(0, 1, 0) * 100; //Increase force by Up * 100 
            }
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0); //Set rotation to be inverse of pitch and yaw

            if (Bullets.Count > 0)
                for (int i = 0;    i < Bullets.Count;    i++)
                {
                    if (Bullets[i].GetComponent<Bullet>().Alive == false)
                    {
                        Destroy(Bullets[i]); //Destroy bullet script 
                        Bullets.Remove(Bullets[i]); //Remove bullet script from list
                        Destroy(BulletHandler[i]); //Destroy game object
                        BulletHandler.Remove(BulletHandler[i]); //Remove bullet game object from list 
                    }
                    if (Bullets[i].GetComponent<Bullet>().timeoutDestructor >= 50)
                    {
                        Destroy(Bullets[i]); //Destroy bullet script 
                        Bullets.Remove(Bullets[i]); //Remove bullet script from list
                        Destroy(BulletHandler[i]); //Destroy game object
                        BulletHandler.Remove(BulletHandler[i]); //Remove bullet game object from list 
                    }
                }
            Bolttimer -= Time.deltaTime; //Decrease bolttimer by delta time 
                if (UI.enabled == false)
                {
                if (Bolttimer < 0)
                {
                    if (Input.GetMouseButton(0))
                    {

                        Fire(); //Call fire function
                        Bolttimer = 1; //Set bolt timer to be 1
                    }
                }

            }
            Transformation.Rotation = new MyVector3(-PITCH, -YAW, 0); //Set rotation to be inverse of pitch and yaw
        }
    }
}