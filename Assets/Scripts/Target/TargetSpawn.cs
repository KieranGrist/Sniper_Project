using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetSpawn : MonoBehaviour {
     List<GameObject> Targets = new List<GameObject>();
    public GameObject cube;
    public float TargetDistance = 150, TargetMax = 10;
     float TargetMaxLast = 5, ResawnTimer;
    public Slider TargetMaxSlider, TargetDistanceSlider;

    public Sniper sniper;
    public myTransformation Floor;

    // Use this for initialization
    void Start()
    {
        TargetDistance = 150;
        TargetMax = 10;
        TargetMaxLast = TargetMax;
        CreateCube();
        TargetMaxChange();
        TargetDistanceChange();
    }
    public void TargetMaxChange()
    {
        TargetMax = TargetMaxSlider.value;
  
    }
    public void TargetDistanceChange()

    {
       
       TargetDistance = TargetDistanceSlider.value;
       
    }
    // Update is called once per frame
    void CreateCube()
    {
        for (int i = 0; i < TargetMax; i++)
        {
            GameObject go = Instantiate(cube, new Vector3(0, 0, 0), transform.rotation);
            go.name = "Cube"+i;
            go.AddComponent<myTransformation>();
            go.AddComponent<TargetScript>();
            TargetScript target = go.GetComponent<TargetScript>();
            target.Floor = Floor;
            target.sniper = sniper;
            target.TaretSpawner = this;
     
            Targets.Add(go);
        }
    }
    void DestroyCube(GameObject Dead)
    {
        Destroy(Dead);
    }
    void Update()
    {
        TargetMaxChange();
        TargetDistanceChange();
        for (int i = 0; i < Targets.Count; i++)
        {
            if (Targets[i].GetComponent<TargetScript>().Alive == false)
            {
                Destroy(Targets[i]);
                Targets.Remove(Targets[i]);
                ResawnTimer = 0;
            }
        }
        if (ResawnTimer >=5)
        {
            CreateCube();
            ResawnTimer = -2;
        }
        else if (ResawnTimer ==-2)
        {
            ResawnTimer = -2;
        }
        else
        {
            ResawnTimer += Time.deltaTime;
        }
        if (TargetMax < Targets.Count)
        {
            for (int i = 0; i < Targets.Count; i++)
            {
                DestroyCube(Targets[i]);

            }
            Targets.Clear();
            CreateCube();
        }
            if (TargetMax != TargetMaxLast)
            {
            for (int i = 0; i < Targets.Count; i++)
            {
                DestroyCube(Targets[i]);
         
            }
            Targets.Clear();
            CreateCube();
        }
        TargetMaxLast = TargetMax;
    }
}
