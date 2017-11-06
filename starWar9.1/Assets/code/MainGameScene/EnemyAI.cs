using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //connection
    public GameObject connStarGroup;
    public Scr_StarGroup connScrGroup;
    public GameObject[] connArryStarGroup;
    public GameObject connTargetStar;



    //attribute
    public Scr_StarAttribute attrEnemySour;
    public Scr_StarAttribute attrEnemyTarget;


    
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChooseTargetStar()
    {
        int tempHP= 999;
        foreach(Scr_StarAttribute child in connScrGroup.attrListFreeStar)
        {
            if (child.attrHP < tempHP)
                attrEnemyTarget = child;
        }
    }

    void SendShip()
    {
        foreach(Scr_StarAttribute child in connScrGroup.attrListEnemyStar)
        {
            foreach(Scr_ShipController i in child.attrListEnemyShip)
            {
                i.connTargetStar = attrEnemyTarget.GetComponent<GameObject>();
                Debug.Log(i.connTargetStar);

            }
        }
    }

    public void ini()
    {
        connArryStarGroup = connStarGroup.GetComponentsInChildren<GameObject>();
        connScrGroup = connStarGroup.GetComponent<Scr_StarGroup>();
    }


}
