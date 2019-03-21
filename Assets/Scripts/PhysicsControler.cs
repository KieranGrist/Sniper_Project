using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsControler : MonoBehaviour {
    public List<MyPhysics> myPhysics = new List<MyPhysics>();
    public MyVector3 Gravity;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        myPhysics.Clear();
        myPhysics.AddRange(MyPhysics.FindObjectsOfType<MyPhysics>());
        for ( int i=0; i < myPhysics.Count; i++)
            {
            myPhysics[i].PhyCont = this;
            }
    }
}
