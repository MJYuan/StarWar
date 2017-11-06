using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_ShipController : MonoBehaviour {

    //Connector
    public GameObject connSourceStar;
    public GameObject connTargetStar;
    public GameObject connBulletMold;
    public GameObject connGameGui;
    public Scr_StarAttribute connScrTarget;
    public Scr_StarAttribute connScrSource;    
    public Scr_GameGui connGui;

    //Animation
    public AnimationClip animShipRandomlyMove;
    public AnimationClip animShipMove;
    public AnimationClip animShipFire;
    public Animation animPlayer;

    //Attribute
    public float attrShipSpeed;
    public float attrRange;
    public float attrAroundRadius; 
    public int attrBelong;
    public float attrAttackTimer;
    public float attrAttackSpeed;
    public float attrHP;
    public Vector3 attrBorPosition;


    //Flage
    public bool flgIsMove;
    public bool flgIsFire;
    public bool flgWarOver;

    //temporary
    public Scr_ShipController tempShip;

    //control
    public float contTimerRandomMove;



    // Use this for initialization
    void Start () {
        flgIsMove = false;
        attrShipSpeed = 100;
        attrRange = 130;
        connGameGui = GameObject.FindGameObjectWithTag("ShowStarInfo");
        connGui = connGameGui.GetComponent<Scr_GameGui>();
        animPlayer = gameObject.GetComponent<Animation>();
        contTimerRandomMove = 0.0f;
        flgWarOver = true;
        connScrSource = connSourceStar.GetComponent<Scr_StarAttribute>();
        
	}
	
	// Update is called once per frame
	void Update () {

        
        contTimerRandomMove -= Time.deltaTime;
        if ((connScrSource.attrBelong ==0) ||(flgIsFire && !flgWarOver))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * attrShipSpeed*0.5f);
            if (contTimerRandomMove <= 0 && !InRange())
            {
                contTimerRandomMove = 1;
                transform.LookAt(connSourceStar.transform.position);
            }
            if(InStar())
            {
                contTimerRandomMove = 1;
                transform.Rotate(Vector3.up * Time.deltaTime*10);
                //transform.LookAt(-Vector3.forward);
            }
                
            
            if (contTimerRandomMove < 0)
            {
                ShipFireMove();
                contTimerRandomMove = 1.0f;
            }            
        }
        if(flgIsFire && flgWarOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * attrShipSpeed*0.5f);
            WarOver();
            if(InRange())           
            {
                flgIsFire = false;
                flgWarOver = true;
            }
        }
        if (flgIsMove && connSourceStar!=connTargetStar)
        {
            ShipMove();
            if(InRange())
            {
                connSourceStar = connTargetStar;
                connScrSource = connSourceStar.GetComponent<Scr_StarAttribute>();
                connScrTarget = connTargetStar.GetComponent<Scr_StarAttribute>();
                transform.parent = connTargetStar.transform;
                if (attrBelong == 1)
                {
                    connScrTarget.attrListMyShip.Add(gameObject.GetComponent<Scr_ShipController>());
                }
                else if(attrBelong == -1)
                {
                    connScrTarget.attrListEnemyShip.Add(gameObject.GetComponent<Scr_ShipController>());
                }
                
                flgIsMove = false;
                
                if (connScrTarget.attrListEnemyShip.Count * connScrTarget.attrListMyShip.Count != 0)
                {
                    connScrTarget.flgIsWar = true;
                }
            }
        }
	}


    bool InRange()
    {
        if (Vector3.Distance(transform.position, connTargetStar.transform.position) < attrRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ShipMove()
    {
        transform.LookAt(connTargetStar.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * attrShipSpeed);

    }

    public void DestorySelf()
    {       
        Destroy(gameObject);
    }

    public void Fight(int dps)
    {
        attrHP -= dps*0.5f;
        if (attrHP <= 0)
            DestorySelf();
    }

    void ShipFireMove()
    {
        
         Vector3 tempNextPosition = new Vector3();
         tempNextPosition.x = transform.position.x + Random.Range(-0.1f, 0.1f);
         tempNextPosition.z = transform.position.z + Random.Range(-0.1f, 0.1f);
         transform.LookAt(tempNextPosition);
         
        //transform.Translate(0, 0, 0);
    }

    public void WarOver()
    {
        transform.LookAt(attrBorPosition);
        transform.Translate(Vector3.forward * Time.deltaTime * attrShipSpeed );
    }

    bool InStar()
    {
        if (Vector3.Distance(transform.position, connTargetStar.transform.position) < 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
