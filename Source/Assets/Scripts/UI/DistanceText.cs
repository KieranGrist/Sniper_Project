using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI; 
[System.Serializable]
public class DistanceText : MonoBehaviour
{
    public TargetSpawn TargetSpawner; //Target Spawner reference
    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {    
         Text text = GetComponent<Text>(); //Get text component of object
          text.text = "Max Spawn Distance : " + TargetSpawner.TargetDistance; //Set text to be maxium spawn distance : + target distance
    }
}
