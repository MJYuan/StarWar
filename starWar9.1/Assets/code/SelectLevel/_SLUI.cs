using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class _SLUI : MonoBehaviour {

    //connection
    public GameObject connMyUI;
    Text connMyText;


	// Use this for initialization
	void Start () {

        connMyText = connMyUI.GetComponent<Text>();
        
        //connMyUI.GetComponent<text>
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ShowWetherLock(bool unlock)
    {
        if(unlock)
        {
            connMyText.text = "UNLOCK";
        }
        else
        {
            connMyText.text = "LOCK";
        }
    }
}
