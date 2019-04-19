using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GridHandle : MonoBehaviour {
     List<CollisionGrid> Grids = new List<CollisionGrid>();
     List<GameObject> GOHandle = new List<GameObject>();
   public List<MyPhysics> PhysicHandle = new List<MyPhysics>();
       public MyVector3 Gravity; 
    float PocketLast;
    float scale;
     public GameObject Model;
    // Use this for initialization
    void Start ()
    {
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
        }

        float PocketsNeeded = 25000;
        if (PocketsNeeded != PocketLast)
        {
            scale = PocketsNeeded / 2;
            for (int b = 0; b < GOHandle.Count; b++)
            {
                Destroy(GOHandle[b]);
            }
            GOHandle.Clear();
            Grids.Clear();
            int i = 0;
            for (float x = -PocketsNeeded; x < PocketsNeeded; x += scale)
            {
                for (float y = -PocketsNeeded; y < PocketsNeeded; y += scale)
                {
                    for (float z = -PocketsNeeded; z < PocketsNeeded; z += scale)
                    {
                        GameObject go = Instantiate(Model, transform.position, transform.rotation);
                        go.name = "Grid" + i;
                        go.AddComponent<myTransformation>();
                        go.AddComponent<CollisionGrid>();
                        go.AddComponent<BoxUpdater>();
                        Grids.Add(go.GetComponent<CollisionGrid>());
                        Grids[i].StartPosition = new MyVector3(x, y, z);
                        Grids[i].StartScale = new MyVector3(scale, scale, scale);
                        GOHandle.Add(go);
                        i++;
                    }
                }
            }
        }


     
      PocketLast = PocketsNeeded;
    }
}
