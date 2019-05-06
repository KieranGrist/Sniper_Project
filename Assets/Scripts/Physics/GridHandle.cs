using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GridHandle : MonoBehaviour
{
    public Slider Windslider;
   
    public List<MyPhysics> PhysicHandle = new List<MyPhysics>();
    public MyVector3 Gravity;
    public MyVector3 WindVelocity;
    public float ChangeTimer, AirResitance, WindSpeed;
    // Use this for initialization
    void Start()
    {
        WindSpeed = Random.Range(1, 1000);
        WindVelocity = RandomVector(-1.0f, 1.0f, 0, 0, -1.0f, 1.0f);
        WindVelocity.y = 0;
        AirResitance = Random.Range(2, 20);
        ChangeTimer = 0;

    }
    MyVector3 RandomVector(float minx, float maxx, float miny, float maxy, float minz, float maxz)
    {
        MyVector3 Ret = new MyVector3();
        Ret.x = Random.Range(minx, maxx);
        Ret.y = Random.Range(miny, maxy);
        Ret.z = Random.Range(minz, maxz);
        return Ret;
    }
    // Update is called once per frame
    void Update()
    {
        PhysicHandle.Clear();
        PhysicHandle.AddRange(FindObjectsOfType<MyPhysics>());
        for (int x = 0; x < PhysicHandle.Count; x++)
        {
            PhysicHandle[x].PhysicObjectHandler = GetComponent<GridHandle>();
            PhysicHandle[x].ObjectId = x;
       //     PhysicHandle[x].AirResitance = AirResitance;
        }


        Bullet[] Allbullets = FindObjectsOfType<Bullet>();
        WindSpeed = FindObjectOfType<WindSpeedtext>().Windslider.value;
        for (int i =0; i < Allbullets.Length; i++)
        {
            Allbullets[i].Physics.Force += (WindSpeed * WindVelocity);// * Allbullets[i].Physics.Mass;

        }

        ChangeTimer += Time.deltaTime;


        if (ChangeTimer >= 100)
        {
            FindObjectOfType<WindSpeedtext>().Windslider.value = WindSpeed = Random.Range(FindObjectOfType<WindSpeedtext>().Windslider.minValue, FindObjectOfType<WindSpeedtext>().Windslider.maxValue);
        
            WindVelocity = RandomVector(-1.0f, 1.0f,0, 0, -1.0f, 1.0f);
            WindVelocity.y = 0;
            AirResitance = Random.Range(2, 20);
            ChangeTimer = 0;

        }
    }
}