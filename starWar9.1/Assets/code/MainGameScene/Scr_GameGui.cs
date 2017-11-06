using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Scr_GameGui : MonoBehaviour {


    //controller
    Text contText;
    Scr_StarAttribute contScrChosen;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(contScrChosen!=null)
            ShowStarInfo(contScrChosen);
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
	}

    public void ShowStarInfo(Scr_StarAttribute ChosenStar)
    {
        contScrChosen = ChosenStar;
        contText = gameObject.GetComponent<Text>();
        if (ChosenStar != null)
        {
            int tempBelong;
            tempBelong = contScrChosen.attrBelong;
            if(tempBelong==1)
            {
                contText.text = "我的星球\n星球血量： ";
                contText.text += contScrChosen.attrHP.ToString();
                if(contScrChosen.flgIsWar)
                {
                    contText.text += "\n交战中。\n";
                    contText.text += "敌人的兵力：" + contScrChosen.attrListEnemyShip.Count.ToString();
                }
                contText.text += "\n我的兵力: " + contScrChosen.attrListMyShip.Count.ToString();
            }
            else if(tempBelong==0)
            {
                contText.text = "自由的星球\n星球血量： ";
                contText.text += contScrChosen.attrHP.ToString();
                if (contScrChosen.flgIsWar)
                {
                    contText.text += "\n交战中。\n";                    
                }
                contText.text += "\n敌人的兵力：" + contScrChosen.attrListEnemyShip.Count.ToString();
                contText.text += "\n我的兵力: " + contScrChosen.attrListMyShip.Count.ToString();
            }
            else if(tempBelong == -1)
            {
                contText.text = "敌人的星球\n星球血量： ";
                contText.text += contScrChosen.attrHP.ToString();
                if (contScrChosen.flgIsWar)
                {
                    contText.text += "\n交战中。";
                }
                contText.text += "\n我的兵力: " + contScrChosen.attrListMyShip.Count.ToString();
                contText.text += "\n敌人的兵力：" + contScrChosen.attrListEnemyShip.Count.ToString();
            }

        }

    }
}
