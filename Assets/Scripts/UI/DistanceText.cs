using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class DistanceText : MonoBehaviour
{
    public Text text;
    public TargetSpawn TargetSpawner;
    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Max Spawn Distance : " + TargetSpawner.TargetDistance;
    }
}
