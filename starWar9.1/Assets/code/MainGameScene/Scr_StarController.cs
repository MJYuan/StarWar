using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StarController : MonoBehaviour {
    //connector
    public Scr_StarAttribute connScrAttr;
    public float contTimerDestory;
    public float contTimerRecover;
    public float contTimerAttack;
    //flage
    public bool flgIsRecover;
    public bool flgBeOccupide;


    //temporary
    public int tempHP;
    public Scr_ShipController tempShip;


    //attribute
    public int attrDestoryHP;
    

    // Use this for initialization
    void Start () {
        contTimerDestory = 3.0f;
        contTimerRecover = 2.0f;
        flgIsRecover = false;
        attrDestoryHP = 10;
        contTimerAttack = 0.1f;
        flgBeOccupide = false;
	}
	
	// Update is called once per frame
	void Update () {
        contTimerDestory -= Time.deltaTime;
        contTimerRecover -= Time.deltaTime;
        contTimerAttack -= Time.deltaTime;
        if (!flgIsRecover&&!connScrAttr.flgIsWar && contTimerDestory <= 0) 
        {
            contTimerDestory = 3.0f;
            if (connScrAttr.attrBelong!=1 && connScrAttr.attrListEnemyShip.Count==0 && connScrAttr.attrListMyShip.Count!=0)
            {
                flgBeOccupide = true;
                BeOccupide(1);                
            }
            else if(connScrAttr.attrBelong!=-1 && connScrAttr.attrListEnemyShip.Count != 0 && connScrAttr.attrListMyShip.Count == 0)
            {
                flgBeOccupide = true;
                BeOccupide(-1);
            }
        }

        if(!connScrAttr.flgIsWar&&flgBeOccupide&&flgIsRecover&&contTimerRecover<=0)
        {
            contTimerRecover = 2.0f;
            if (connScrAttr.attrBelong != 1 && connScrAttr.attrListEnemyShip.Count == 0 && connScrAttr.attrListMyShip.Count != 0)
            {
                
                RecoverHP(1);
            }
            else if (connScrAttr.attrBelong != -1 && connScrAttr.attrListEnemyShip.Count != 0 && connScrAttr.attrListMyShip.Count == 0)
            {
                RecoverHP(-1);                
            }
        }
        if(connScrAttr.flgIsWar&&contTimerAttack<=0)
        {
            contTimerAttack = 0.1f;
            War();
            if (connScrAttr.attrListEnemyShip.Count * connScrAttr.attrListMyShip.Count == 0)
            {
                connScrAttr.flgIsWar = false;
                foreach (Scr_ShipController child in connScrAttr.attrListMyShip)
                {
                    child.flgWarOver = true;
                    
                }
                foreach (Scr_ShipController child in connScrAttr.attrListEnemyShip)
                {
                    child.flgWarOver = true;
                }
            }
                
            
        }
	}


    //
    public void  BeOccupide(int f)
    {

        
        if(f==1)
        {
            if(connScrAttr.attrHP - attrDestoryHP <=0)
            {
                connScrAttr.attrHP = 0;
                tempHP = Random.Range(50, 100);
                flgIsRecover = true;
                flgBeOccupide = true;
                if(connScrAttr.attrBelong == -1)
                {
                    connScrAttr.connStarGroup.attrListEnemyStar.Remove(gameObject.GetComponent<Scr_StarAttribute>());
                    
                }
                connScrAttr.attrBelong = 0;

            }
            else
            {
                connScrAttr.attrHP -= attrDestoryHP; 
            }
            connScrAttr.RemoveShip(1, 0);
        }
        else if(f==-1)
        {
            if (connScrAttr.attrHP - attrDestoryHP <= 0)
            {
                connScrAttr.attrHP = 0;
                tempHP = Random.Range(50, 100);               
                flgIsRecover = true;
                flgBeOccupide = true;
                if (connScrAttr.attrBelong == 1)
                {
                    connScrAttr.connStarGroup.attrListMyStar.Remove(gameObject.GetComponent<Scr_StarAttribute>());
                }
                connScrAttr.attrBelong = 0;


            }
            else
            {
                connScrAttr.attrHP -= attrDestoryHP;
            }
            connScrAttr.RemoveShip(-1, 0);
        }
    }

    public void RecoverHP(int f )
    {
        
        if (connScrAttr.attrHP+2*attrDestoryHP < tempHP)
        {
            connScrAttr.attrHP += 2 * attrDestoryHP;
        }
        else if(connScrAttr.attrHP +2*attrDestoryHP >= tempHP)
        {
            connScrAttr.attrHP = tempHP;
            flgIsRecover = false;
            flgBeOccupide = false;
            connScrAttr.attrBelong = 0;
            if(f==1)
            {
                connScrAttr.attrBelong = 1;
                connScrAttr.connStarGroup.attrListMyStar.Add(gameObject.GetComponent<Scr_StarAttribute>());
                
                
            }
               
            else if(f==-1)
            {
                connScrAttr.attrBelong = -1;
                //connScrAttr.connStarGroup.attrListAllStar.Add(gameObject);
                connScrAttr.connStarGroup.attrListEnemyStar.Add(gameObject.GetComponent<Scr_StarAttribute>());
            }
        }
        
    }

//*************************
    public void War()
    {
        
        foreach(Scr_ShipController child in connScrAttr.attrListMyShip)
        {
            child.attrBorPosition = child.transform.position;
            child.flgIsFire = true;
            child.flgWarOver = false;
        }
        foreach(Scr_ShipController child in connScrAttr.attrListEnemyShip)
        {
            child.attrBorPosition = child.transform.position;
            child.flgIsFire = true;
            child.flgWarOver = false;
        }
        connScrAttr.attrListEnemyShip[0].Fight(connScrAttr.attrListMyShip.Count);
        
        connScrAttr.attrListMyShip[0].Fight(connScrAttr.attrListEnemyShip.Count);

        if(connScrAttr.attrListMyShip[0].attrHP<=0)
        {
            connScrAttr.RemoveShip(1, 0);
        }
        if (connScrAttr.attrListEnemyShip[0].attrHP <= 0)
        {
            connScrAttr.RemoveShip(-1, 0);
        }

    }

}
