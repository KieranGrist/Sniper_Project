using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class ForceText : MonoBehaviour {

    // Use this for initialization

    public Sniper Player;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
             Text text = GetComponent<Text>();
             text.text = "Firing Speed : " + Player.FireSpeed;
    }
}
