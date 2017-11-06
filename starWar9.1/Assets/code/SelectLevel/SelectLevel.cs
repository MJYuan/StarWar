using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour {

    //connection
    public GameObject child;
    public GameObject connUI;
    public GameObject connShipGo;

    //attribute
    public int attrID;
    public _SLUI attrUI;
    //control


    //flag
    private bool flgMouseDown;
    public bool flgUnLock;

    //Animation
    // Use this for initialization
    void Start () {
        flgMouseDown = false;
        attrUI = connUI.GetComponent<_SLUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseOver()
    {
        
        
        if (Input.GetMouseButtonDown(0))
        {
            flgMouseDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (flgMouseDown == true)
            {
                if (flgUnLock)
                {
                    connShipGo.GetComponent<ShipGo>().PlayShipGo(attrID);
                }
                   
            }
            
        }
    }

    public void BeLook()
    {
        attrUI.ShowWetherLock(flgUnLock);
    }
}
