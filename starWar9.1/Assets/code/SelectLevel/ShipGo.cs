using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGo : MonoBehaviour {

    //connection
    public RotateCamera connCamera;

    //attribute
    public int attrID;

    //control
    public float contTimerLoad;

    //flag
    public bool flgIsChoose;

    //animation
    public AnimationClip animShipGo;
    private Animation animPlayer;

	// Use this for initialization
	void Start () {
        contTimerLoad = 1.0f;
        flgIsChoose = false;
        animPlayer = gameObject.GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        if(flgIsChoose)
        {
            contTimerLoad -= Time.deltaTime;
        }
		if(contTimerLoad <=0)
        {
            connCamera.GetComponent<RotateCamera>().LoginLevel(attrID);
        }
	}

    public void PlayShipGo(int id)
    {
        animPlayer.Play(animShipGo.name);
        attrID = id;
        flgIsChoose = true;

    }
}
