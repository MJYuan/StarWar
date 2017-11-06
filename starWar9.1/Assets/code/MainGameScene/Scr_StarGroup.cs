using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Scr_StarGroup : MonoBehaviour {

    //connector
    public GameObject connSourceStar;
    public GameObject connTargetStar;
    public EnemyAI connEnemyAI;
    public GameObject Win;
    public GameObject Lose;


    //flage
    public bool flgFirstChoose;


    //attribute
    public int attrMyStarNumber;
    public int attrEnemyStarNumber;
    public List<GameObject> attrListAllStar = new List<GameObject>();
    public Scr_StarAttribute[] attrArrAllStar;
    public List<Scr_StarAttribute> attrListFreeStar = new List<Scr_StarAttribute>();
    public List<Scr_StarAttribute> attrListMyStar = new List<Scr_StarAttribute>();
    public List<Scr_StarAttribute> attrListEnemyStar = new List<Scr_StarAttribute>();
    public List<Scr_ShipController> attrLisrMyShip = new List<Scr_ShipController>();
    public List<Scr_ShipController> attrListEnemyShip = new List<Scr_ShipController>();

    //control
    public float contTimerEnemyAttack;

    // Use this for initialization
    void Start () {
        contTimerEnemyAttack = 7.0f;
        flgFirstChoose = false;
        
	}
	
	// Update is called once per frame
	void Update () {
		if(flgFirstChoose)
        {
            if (attrListEnemyStar.Count==0)
            {
                
                Win.SetActive(true);
                gameObject.SetActive(false);
            }
            if(attrListMyStar.Count==0 )
            {
                gameObject.SetActive(false);
                Lose.SetActive(true);
            }
            contTimerEnemyAttack -= Time.deltaTime;
            if(contTimerEnemyAttack <=0)
            {
                EnemyChooseTarget();
                contTimerEnemyAttack = 7.0f;
            }
        }
	}



     public void MakeStarGroup()
    {
        
        attrArrAllStar = gameObject.GetComponentsInChildren<Scr_StarAttribute>();
        foreach (Scr_StarAttribute child in attrArrAllStar)
        {
            if(child.GetComponent<Scr_StarAttribute>().attrBelong==1)
            {
                attrListMyStar.Add(child.GetComponent<Scr_StarAttribute>());
            }
            else if(child.GetComponent<Scr_StarAttribute>().attrBelong==0)
            {
                attrListFreeStar.Add(child.GetComponent<Scr_StarAttribute>());
            }
        }
        int p = Random.Range(0, attrListFreeStar.Count);
        attrListEnemyStar.Add(attrListFreeStar[p]);
        attrListFreeStar.RemoveAt(p);
        attrListEnemyStar[0].attrBelong = -1;
        flgFirstChoose = true;
        
    }

    public void ChooseSourceStar(GameObject s)
    {
        connSourceStar = s;

    }

    public void ChooseTargetStar(GameObject t)
    {
        connTargetStar = t;
        if(connSourceStar!=connTargetStar)
            SendMoveMessage();
    }

    public void SendMoveMessage()
    {
        List<Scr_ShipController> tempSC = new List<Scr_ShipController>();
        tempSC = connSourceStar.GetComponent<Scr_StarAttribute>().attrListMyShip;
        foreach(Scr_ShipController child in tempSC)    
        {
            child.connSourceStar = connSourceStar;
            child.connTargetStar = connTargetStar;
            child.transform.parent = gameObject.transform;
            child.flgIsMove = true;
        }
        attrLisrMyShip = tempSC;
        tempSC.Clear();
    }

    //This code is for EnemyAI

    void SendEnemyShip(GameObject target)
    {
        foreach(Scr_StarAttribute child in attrListEnemyStar)
        {
            if(child.attrBelong == -1)
            {
                Debug.Log(child);
                foreach(Scr_ShipController i in child.attrListEnemyShip)
                {
                    i.connTargetStar = target;
                    i.transform.parent = gameObject.transform;
                    i.flgIsMove = true;
                }
                child.attrListEnemyShip.Clear();
            }
        }
    }

    void EnemyChooseTarget()
    {
        int tempHP = 999;
        GameObject tempTarget = null;

        //血最少的星球
        foreach(GameObject child in attrListAllStar)
        {
            child.GetComponent<Scr_StarAttribute>();
            if(child.GetComponent<Scr_StarAttribute>().attrBelong!=-1 && child.GetComponent<Scr_StarAttribute>().attrHP<tempHP)
            {
                tempHP = child.GetComponent<Scr_StarAttribute>().attrHP;
                tempTarget = child;
            }
        }

        SendEnemyShip(tempTarget);
    }


}
