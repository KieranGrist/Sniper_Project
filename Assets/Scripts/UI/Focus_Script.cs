using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Focus_Script : MonoBehaviour {
    public Canvas UI;
    public Canvas[] Pages = new Canvas[5];
    public int Page =0;
    public Button NextPage, PreviousPage;
    float buttontimer;
	// Use this for initialization
	void Start () {
		
	}
  public void AddPage()
    {
        Page+=1;
        Mathf.Clamp(Page, 0, 4);
    }
    public void RemovePage()
    {
        Page-=1;
        Mathf.Clamp(Page, 0, 4);
    }
	// Update is called once per frame
	void Update () {
        buttontimer += Time.deltaTime;
        if (buttontimer >= 5)
        {
            NextPage.onClick.AddListener(AddPage);
            PreviousPage.onClick.AddListener(RemovePage);
            buttontimer = 0;
        }
        for (int i =0; i <5; i++ )
        {
            Pages[i].enabled = false; 
        }
        Pages[Page].enabled = true;
    
        
		if (Input.GetKey(KeyCode.LeftControl))
        {
            UI.enabled = true;
        }
        else
        {
            UI.enabled = false;
        }

    }
}
