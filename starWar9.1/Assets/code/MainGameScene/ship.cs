using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship : MonoBehaviour
{    
    public GameObject SourceStar;
    public GameObject TargetStar;
    public GameObject BulletMold;
    public AnimationClip ShipRandomlyMove;
    public AnimationClip ShipMove;
    public float ShipSpeed;
    public float Range;
    public float n_ship_around_radius;
    public bool IsMove;
    public int BelongWho;
    float AttackTimer;
    float AttackSpeed;
    bool IsFire;
	// Use this for initialization
	void Start ()
    {
        IsFire = false;
        AttackSpeed = 1.0f;
        AttackTimer = AttackSpeed;
        ShipSpeed = 10;
        Range = 10 + Random.Range(-2, 2);
        IsMove = false;
       // this.gameObject.GetComponent<Animation>().Play(ShipRandomlyMove.name);
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {

        AttackTimer -= Time.deltaTime;
        if (IsMove||IsFire)
        {
            this.gameObject.GetComponent<Animation>().Play(ShipMove.name);
            
        }
        else
        {
            this.gameObject.GetComponent<Animation>().Play(ShipRandomlyMove.name);
        }
        if(SourceStar != TargetStar)
        {
            if(IsMove == true)
            {
                MoveShip();
                if (InRange())
                {
                    SourceStar.GetComponent<star>().ShipNum -= 1;
                    SourceStar.GetComponent<star>().ShipKind -= BelongWho;
                    SourceStar = TargetStar;
                    TargetStar.GetComponent<star>().ShipNum += 1;
                    TargetStar.GetComponent<star>().ShipKind += BelongWho;
                    Quaternion now_rotation = transform.rotation;
                    Quaternion ship_r = Quaternion.Euler(now_rotation.x, now_rotation.y + 90, now_rotation.z);
                    transform.rotation = Quaternion.Euler(ship_r.x, ship_r.y, ship_r.z);
                    gameObject.transform.parent = TargetStar.transform;
                    IsMove = false;
                    TargetStar.GetComponent<star>().AddShip(gameObject);
                }
            }
        }
        if (AttackTimer < 0 && SourceStar.GetComponent<star>().GetShipKinds() > 1)
        {
            IsFire = true;
            GameObject aimEnemty;
            aimEnemty = ChooseEnemy();
            if (aimEnemty != null)
            {
                gameObject.transform.LookAt(aimEnemty.transform.position);
                Shoot();
            }
            AttackTimer = AttackSpeed;
        }


    }


    bool InRange()
    {
        if (Vector3.Distance(transform.position, TargetStar.transform.position) < Range)
        {
            IsMove = false;
            return true;
        }    
        else
        {
            return false;
        }
            
    }
    public void MoveShip()
    {
            this.gameObject.transform.LookAt(TargetStar.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime*ShipSpeed);
    }

    public GameObject ChooseEnemy()
    {
        int EnemyNo=-1;
        float dis=9999999.0f;
        List<GameObject> tempShip = SourceStar.GetComponent<star>().ShipList;

        for(int i=0;i<tempShip.Count;i++)
        {
            float td = Vector3.Distance(transform.position, tempShip[i].transform.position);

            if (td <= dis && BelongWho!=tempShip[i].GetComponent<ship>().BelongWho)
            {
                Debug.Log(i + " " + td);    
                EnemyNo = i;
            }
        }
        if (EnemyNo != -1)
        {
            
            return tempShip[EnemyNo];

        }           
        else
            return null; 
    }


    public void Shoot()
    {
        
        Transform p = null;
        Transform EnemyTransform;
        EnemyTransform= ChooseEnemy().transform;
        foreach (Transform child in this.GetComponentInChildren<Transform>())
        {
            if(child.name == "gun")
            {
                p = child.transform;
            }
        }
        if(p!=null)
        {
            GameObject tempBullet = Instantiate(BulletMold, p.position, p.rotation);
           // tempBullet.transform.parent = gameObject.transform;
           // tempBullet.transform.LookAt(ChooseEnemy().transform.position);
            tempBullet.GetComponent<Bullet>().BelongWho = BelongWho;
            tempBullet.GetComponent<Rigidbody>().AddForce(tempBullet.transform.forward*30,ForceMode.Impulse);
            Destroy(tempBullet, 10.0f);
        }
         
    }
}
