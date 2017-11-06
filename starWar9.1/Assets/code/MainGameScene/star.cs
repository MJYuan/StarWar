using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour
{


    public Transform n_star_transform;
    public float n_star_radius;
    public GameObject n_ship_model;
    public AnimationClip ship_around;
    public AnimationClip StarIdle;
    public float ProduceSpeed;
    public int MaxShipNum;
    public GameObject UI;
    public int ShipNum; 
    private float TimerProduce;
    public GameObject _StarController;
    //public ship[] SourceShip;
    bool isChoos;
    public int HP;
    public int BelongWho; // 1: mine , 0:free, -1:enemy;
    public float DestroySpeed;
    public int ShipKind;
    public float TimerDestroy;
    int OldHP;
    bool IsFire;
    public List<GameObject> ShipList = new List<GameObject>(); 

    void ini()
    {
        IsFire = false;
        ProduceSpeed = 2.5f;
        DestroySpeed = 1.0f;
        TimerProduce = ProduceSpeed ;
        TimerDestroy = DestroySpeed;
       // MaxShipNum = 10;
        ShipNum = 0;
    }
	// Use this for initialization
	void Start ()
    {
        
        UI = GameObject.FindGameObjectWithTag ("GUI");
        BelongWho = 0;
        HP = Random.Range(50, 100);
        ShipKind = 0;
        OldHP = HP;
        ini();
    }
	

	// Update is called once per frame
	void Update ()
    {
        if (GetShipKinds() !=0)
        {
            this.gameObject.GetComponent<Animation>().Play(ship_around.name);
        }
        else
        {
            this.gameObject.GetComponent<Animation>().Play(StarIdle.name);
        }
        TimerDestroy -= Time.deltaTime;
        TimerProduce -= Time.deltaTime;
        if (TimerProduce <= 0)
        {
            ProductShip();
            TimerProduce = ProduceSpeed;        
        }
        if (TimerDestroy <= 0&&GetShipKinds()==1&&GetShipKind()!=BelongWho)
        {

            BeDestroy(GetShipKind());
            TimerDestroy = DestroySpeed;

        }

        
    }

    //how many kind of ship this star have
    public int GetShipKinds()
    {
        bool[] kind = new bool[3];
        int KindNumber = 0;
        for (int i = 0; i < 3; i++)
        {
            kind[i] = false;
        }

        foreach (GameObject child in ShipList)
        {
            if (child.GetComponent<ship>().BelongWho == -1)
                kind[0] = true;
            if (child.GetComponent<ship>().BelongWho == 0)
                kind[1] = true;
            if (child.GetComponent<ship>().BelongWho == 1)
                kind[2] = true;
        }

        for (int i = 0; i < 3; i++)
        {
            if (kind[i] == true)
                KindNumber++;
        }
        return KindNumber;

    }

    // the ship belongWho 
    int GetShipKind()
    {
        return ShipList[0].GetComponent<ship>().BelongWho;
    }


    void ProductShip()
    {
        if(MaxShipNum>ShipNum && BelongWho!=0)
        {
            CreatShip();
        }
    }

    //use this to productspaceship
    void CreatShip()
    {
        float offset = Random.Range(-2.0f, 2.0f);
        Vector3 ship_p = new Vector3(n_star_transform.position.x ,n_star_transform.position.y,n_star_transform.position.z + 10 + offset);
        Quaternion ship_r = Quaternion.Euler(0,90,0);
        GameObject ship = Instantiate(n_ship_model, ship_p, ship_r) as GameObject;
        ShipList.Add(ship);
        ship.transform.parent = this.transform;
        ship.GetComponent<ship>().SourceStar = gameObject;
        ship.GetComponent<ship>().TargetStar = gameObject;
        ship.GetComponent<ship>().BelongWho = BelongWho;
        ShipKind += BelongWho;
		ShipNum++;
    }

    public void AddShip(GameObject e)
    {
        ShipList.Add(e);
    }

    public int GetShipNumber()
    {
        return ShipList.Count;
    }
	void OnMouseOver()
	{

        if (StarController.FristChoose && Input.GetMouseButtonUp(0))
        {
            //StarController.GetComponent<StarController>().FristChoose = true;
            ChooseMyFristStar();
        }
        else
        {
            // move all ship to another star
            if (Input.GetMouseButtonUp(0))
            {
                // return targaet star
                ChooseTargetStar();
                ExamineStarShipNomber();
                // Debug.Log(gameObject);
            }

            //examine the namber of this star 
            if ( Input.GetMouseButtonDown(0))
            {
                ChooseSourceStar();


            }
        }
       
	}


    void ChooseTargetStar()
    {


        _StarController.GetComponent<StarController>().TargetStar = gameObject;
        _StarController.GetComponent<StarController>().SendShip();
        
    }

    void ChooseSourceStar()
    {
        _StarController.GetComponent<StarController>().SourceStar = gameObject;
    }

    void ExamineStarShipNomber()
    {
        UI.GetComponent<GameGui>().ChosenStar = gameObject;
    }

    void ChooseMyFristStar()
    {
        _StarController.GetComponent<StarController>().MyNewStar = gameObject;
        _StarController.GetComponent<StarController>().AddNewStarForME();
    }

    public void BeDestroy(int shipbelong)
    {
        Debug.Log(HP);
        //if (tempKind==ShipNum && shipbelong!= BelongWho)    //if only have one kind of ship and the star is not belong those ship
        //{
        //capture the star
        if (HP - GetShipNumber() >= 0)
            {
                HP -= GetShipNumber();
            }
            else
            {
                HP = 0;
            }

            // if HP  = 0 then change the BelongWho to the ship 
            if (HP == 0)
            {
                BelongWho = shipbelong;
            HP = OldHP;
            }
            
        //}
    }


}
