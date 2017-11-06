using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StarAttribute : MonoBehaviour {

    //Connector
    public GameObject connMyShipModel;
    public GameObject connEnemyShipModel;
    public Scr_ShipController connScrShip;
    public Scr_StarGroup connStarGroup;
    public Scr_GameGui connGui;
    public MeshRenderer connStarMeshRenderer;


    //Animations
    public AnimationClip animIdle;
    public AnimationClip animMyStarAround;
    public AnimationClip animEnemyStarAround;
    public AnimationClip animFreeStarAround;
    public AnimationClip animWar;
    private Animation animPlayer;


    //Attribute
    public float attrProduceSpeed;
    public int attrMaxShipNumber;
    public int attrShipNumber;
    public int attrHP;
    public int attrBelong;
    public int attrShipKind;
    public List<Scr_ShipController> attrListMyShip;
    public List<Scr_ShipController> attrListEnemyShip;

    // Flage
    public bool flgIsWar;
    //public bool flgIsBegin;


    //control
    float contTimerDestory;
    float contTimerPoduce;


    //temporary
    public Scr_ShipController tempShip;


    // Use this for initialization
    void Start()
    {
        attrProduceSpeed = 2.5f;
        attrMaxShipNumber = 5;
        attrShipNumber = 0;
        attrShipKind = 0;
        attrHP = (int)Random.Range(50, 100);
        flgIsWar = false;
        animPlayer = gameObject.GetComponent<Animation>();
        connStarGroup.attrListAllStar.Add(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        if (attrListEnemyShip.Count != 0 && attrListMyShip.Count != 0)
            flgIsWar = true;
        else
        {
            flgIsWar = false;
        }
        if(flgIsWar)
        {
            animPlayer.CrossFade(animWar.name);
        }
        else if(connStarGroup.flgFirstChoose)
        {
            if(attrBelong==1)
            {
                animPlayer.CrossFade(animMyStarAround.name);
            }
            else if(attrBelong==-1)
            {
                animPlayer.CrossFade(animEnemyStarAround.name);
            }
        }       
        contTimerPoduce -= Time.deltaTime;
 
        if(!flgIsWar && contTimerPoduce<=0 && ((attrBelong==1 && attrListMyShip.Count<attrMaxShipNumber)||(attrBelong == -1 && attrListEnemyShip.Count < attrMaxShipNumber)))
        {
            ProductShip();
            contTimerPoduce = attrProduceSpeed;
        }
    }

    //use this function to prodct ship 
    void ProductShip()
    {
        GameObject tempShip;
        float offset = Random.Range(-2.0f, 2.0f);
        
        if (attrBelong!=0 && !flgIsWar)
        {
            Vector3 tempShipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 100 + offset);
            Quaternion tempShipRotation = Quaternion.Euler(0, 90, 0);
            if (attrBelong == 1)
            {
                tempShip = Instantiate(connMyShipModel, tempShipPosition, tempShipRotation) as GameObject;
            }
            else if(attrBelong == -1)
            {
                tempShip = Instantiate(connEnemyShipModel, tempShipPosition, tempShipRotation) as GameObject;
            }
            else
            {
                tempShip = null;
            }
            if(tempShip!=null)
            {
                connScrShip = tempShip.GetComponent<Scr_ShipController>();
                tempShip.transform.parent = transform;
                connScrShip.attrBelong = attrBelong;
                connScrShip.connSourceStar = gameObject;
                connScrShip.connTargetStar = gameObject;
                connScrShip.attrHP = attrHP;
                if (attrBelong == 1)
                    attrListMyShip.Add(connScrShip);
                else
                    attrListEnemyShip.Add(connScrShip);
            }
            
        }
    }
  
    void OnMouseOver()
    {

        if (!connStarGroup.flgFirstChoose && Input.GetMouseButtonUp(0))
        {
            //choose my star
            attrBelong = 1;
            connStarGroup.MakeStarGroup();
            connGui.ShowStarInfo(gameObject.GetComponent<Scr_StarAttribute>());
        }
        else
        {
            // choose target star
            if (Input.GetMouseButtonUp(0))
            {
                connStarGroup.ChooseTargetStar(gameObject);

                connGui.ShowStarInfo(gameObject.GetComponent<Scr_StarAttribute>());
                // return targaet star
               // connStarGroup.attrBelong;
                // Debug.Log(gameObject);
            }

            //examine the namber of this star 
            if (Input.GetMouseButtonDown(0))
            {
                
                connStarGroup.ChooseSourceStar(gameObject);


            }
        }

    }

    public void  RemoveShip(int who,int p)
    {
        if (who == 1)
        {
            tempShip = attrListMyShip[p];
            attrListMyShip.RemoveAt(p);
            tempShip.DestorySelf();
            
        }
        else if (who == -1)
        {
            tempShip = attrListEnemyShip[p];
            attrListEnemyShip.RemoveAt(p);
            tempShip.DestorySelf();
        }

    }
    
}
