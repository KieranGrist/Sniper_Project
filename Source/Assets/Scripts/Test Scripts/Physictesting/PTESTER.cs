using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTESTER : MonoBehaviour
{
    public int PhysicObjectscount = 90;
    public GameObject model;
    public List<MyRigidBody> PhysicObjects = new List<MyRigidBody>();
    public List<JayRigidBody> JayPhysicObjects = new List<JayRigidBody>();
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PhysicObjectscount; i++)
        {
            GameObject Go = Instantiate(model, new Vector3(0, 0, 0), transform.rotation);
            Go.name = "MyPhysicObject" + i;
            Go.AddComponent<myTransformation>();
            Go.AddComponent<MyRigidBody>();
            PhysicObjects.Add(Go.GetComponent<MyRigidBody>());
            myTransformation temp;
            temp = Go.GetComponent<myTransformation>();
            temp.Translation = new MyVector3(i * 10, 150, 0);
            PhysicObjects[i].ID = i;
            PhysicObjects[i].Mass = (i * 10) + 1;
            PhysicObjects[i].OriginalPostion = new MyVector3(i * 10, 150, 0);
            Go.GetComponent<Renderer>().material.color = Color.green;

            GameObject gameObject = Instantiate(model, new Vector3(0, 0, 0), transform.rotation);
            gameObject.AddComponent<myTransformation>();
            gameObject.AddComponent<JayRigidBody>();
            gameObject.name = "JayPhysicObject" + i;
            JayPhysicObjects.Add(gameObject.GetComponent<JayRigidBody>());
            temp = gameObject.GetComponent<myTransformation>();
            temp.Translation = new MyVector3(i * 10, 150, 0);
            temp.Translation.z += 10;
            JayPhysicObjects[i].OriginalPostion = new MyVector3(i * 10, 150, 0);
            JayPhysicObjects[i].OriginalPostion.z += 10;
            JayPhysicObjects[i].ID = i;
            JayPhysicObjects[i].Mass = PhysicObjects[i].Mass;
            gameObject.GetComponent<Renderer>().material.color = Color.red;

        }

    }
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < PhysicObjectscount; i++)
        {
            if (JayPhysicObjects[i].Postion.y < -100.0F)
            {
                JayPhysicObjects[i].Restart();
                JayPhysicObjects[i].Postion = JayPhysicObjects[i].OriginalPostion;
           
            }
            if (PhysicObjects[i].Postion.y < -100.0F)
            {
                PhysicObjects[i].Restart();
                PhysicObjects[i].Postion = PhysicObjects[i].OriginalPostion;
   
            }
        }
    }
}