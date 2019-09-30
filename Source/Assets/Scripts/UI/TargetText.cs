using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.UI;
[System.Serializable]
public class TargetText : MonoBehaviour
{


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Text text = GetComponent<Text>(); //Gets the text component of object
        TargetSpawn TargetSpawner = FindObjectOfType<TargetSpawn>(); //Finds target spawner and sets a temporary value to be a reference of target spawner
        text.text = "Target Ammount : " + TargetSpawner.TargetMax; //Sets text to be target amount : + target spawner target count/max


    }
}