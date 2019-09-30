using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
[System.Serializable]
public class TargetSpawn : MonoBehaviour {
     List<GameObject> Targets = new List<GameObject>(); //Create a game object list of targets
    public GameObject cube; //Target Game object
    public float TargetDistance = 150; //Biggest distance to a target
    public float TargetMax = 10; //Ammount of targets
    float TargetMaxLast = 5; //Last Target Max
    public float ResawnTimer; //Respawn timer
    public Slider TargetMaxSlider; //Target Ammount of slider
    public Slider TargetDistanceSlider; //Target Distance Slider
    // Use this for initialization
    void Start()
    {
        TargetDistance = 150; //Sets target distance to be 150
        TargetMax = 10; //Set target ammount to be 10
        TargetMaxLast = TargetMax; //Sets target max last to be target max
        CreateCube(); //Calls create Cube Function
        TargetMaxChange(); //Calls target max change function
        TargetDistanceChange(); //Calls target distance change funciton
    }
    public void TargetMaxChange()
    {
        TargetMax = TargetMaxSlider.value; //Set target max to be slider value
  
    }
    public void TargetDistanceChange()
    {

        TargetDistance = TargetDistanceSlider.value; //Sets target distance to be slider value
    }
    // Update is called once per frame
    void CreateCube()
    {
        for (int i = 0;  i < TargetMax;  i++)
        {
            GameObject go = Instantiate(cube, new Vector3(0, 0, 0), transform.rotation); //Creates the target game object
            go.name = "Target"+i; //Sets name to be target + i
            go.AddComponent<myTransformation>(); //Adds transformation to object
            go.AddComponent<BoxUpdater>(); //Adds box updater to object
            go.AddComponent<MyPhysics>(); //Adds myphysics to object
            go.AddComponent<TargetScript>(); //Adds targetscript to object
            TargetScript target = go.GetComponent<TargetScript>(); //Gets the target script component
            target.TaretSpawner = this; //Sets the target spawner to this object
    
            Targets.Add(go); //Add game object to targets list
        }
    }
    void DestroyCube(GameObject Dead)
    {
        Destroy(Dead); //Destroy game object
    }
    void Update()
   {
        TargetMaxChange(); //Call targetmaxchange function
        TargetDistanceChange(); //Call targetdistancechange
        for (int i = 0;  i < Targets.Count;  i++)
        {
            if (Targets[i].GetComponent<TargetScript>().Alive == false)
            {
                Destroy(Targets[i]); //Destroy target
                Targets.Remove(Targets[i]); //Remove target from list
            }
        }
        if (ResawnTimer >=50)
        {
            CreateCube(); //Call create cube function
            ResawnTimer = 0; //Set respawn timer to 0 
        }

        ResawnTimer += Time.deltaTime; //Increasing respawn timer by time

        if (TargetMax < Targets.Count)
        {
            for (int i = 0;  i < Targets.Count;  i++)
            {
                DestroyCube(Targets[i]); //destroy targets

            }
            Targets.Clear(); //Clear targets list
            CreateCube(); //calls create cube function
        }
        if (TargetMax != TargetMaxLast)
        {
            for (int i = 0;  i < Targets.Count;  i++)
            {
                DestroyCube(Targets[i]); //destroy targets

            }
            Targets.Clear(); //Clear targets list
            CreateCube(); //calls create cube function
        }
        TargetMaxLast = TargetMax; //Set Target max last to target max
    }
}
