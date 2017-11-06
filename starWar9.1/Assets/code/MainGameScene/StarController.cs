using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StarController : MonoBehaviour {


    public GameObject SourceStar;
    public GameObject TargetStar;
    public star []StarChild;
    public star[] temp;
    List<star> MyStar = new List<star>();
    List<star> EnemyStar = new List<star>();
    List<star> FreeStar = new List<star>();
    static public bool FristChoose;
    public GameObject MyNewStar;
    public ship[] ChooseShip;
    // Use this for initialization
    void Start () {
        StarChild = gameObject.GetComponentsInChildren<star>();
        FristChoose = true;
        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AddNewStarForME()
    {
        if(FristChoose)
        {
            MyStar.Add(MyNewStar.GetComponent<star>());
            MyNewStar.GetComponent<star>().BelongWho = 1;
            AddFreeStar();
            ChooseEnemyStar();
            FristChoose = false;
        }
       
    }

    void AddFreeStar()
    {
        
        temp = gameObject.GetComponentsInChildren<star>();
        foreach(star child in temp)
        {
            if(child.GetComponent<star>().BelongWho==0)
            {
                FreeStar.Add(child);
            }
        }
    }

    void ChooseEnemyStar()
    {
        System.Random rm = new System.Random();

        int i = rm.Next(FreeStar.Count);
        FreeStar[i].GetComponent<star>().BelongWho = -1;
        EnemyStar.Add(FreeStar[i]);
        FreeStar.RemoveAt(i);
    }

    public void SendShip()
    {
        if(TargetStar!=SourceStar)
        {
            foreach(GameObject child in SourceStar.GetComponent<star>().ShipList)
            {
                if (child.GetComponent<ship>().BelongWho == 1)
                {
                    child.GetComponent<ship>().SourceStar = SourceStar;
                    child.GetComponent<ship>().TargetStar = TargetStar;
                    child.transform.parent = this.gameObject.transform;
                    child.GetComponent<ship>().IsMove = true;
                }
            }
            /*
            ChooseShip = SourceStar.GetComponentsInChildren<ship>();
            foreach (ship child in ChooseShip)
            {
                
            }
            */
        }
        
    }

}
