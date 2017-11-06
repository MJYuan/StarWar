using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGui : MonoBehaviour {
	public GameObject ChosenStar;
	GameObject myui;
	// Use this for initialization
	void Start () {
		myui = GameObject.FindGameObjectWithTag ("ShowStarInfo");
        
	}
	
	// Update is called once per frame
	void Update () {
		if (ChosenStar != null) 
		{
			ShowStarInfo();
		}
		//Debug.Log (ChosenStar);

	}
	void ShowStarInfo()
	{
		if (ChosenStar != null)
		{
            int tempBelong;
            tempBelong = ChosenStar.GetComponent<star>().BelongWho;

            myui.GetComponent<Text>().text ="兵力："+ ChosenStar.GetComponent<star>().GetShipNumber().ToString()+"\n星球血量：";
            myui.GetComponent<Text>().text += ChosenStar.GetComponent<star>().HP.ToString();
            if (tempBelong == -1)
                myui.GetComponent<Text>().text += "\n敌人的星球。";
            else if(tempBelong == 0)
                myui.GetComponent<Text>().text += "\n自由的星球。";
            else if(tempBelong == 1)
                myui.GetComponent<Text>().text += "\n我的星球。";

        }

	}

    private void OnMouseOver()
    {
        
    }
   
   

}
