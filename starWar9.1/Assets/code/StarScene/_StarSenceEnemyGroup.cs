using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _StarSenceEnemyGroup : MonoBehaviour {

    //control
    private float contTimer;

    //flag
    public bool flgAttack;

    //Animation
    public AnimationClip animEnemyFight;
    public Animation animPlayer;
	// Use this for initialization
	void Start () {
        //gameObject.renderer.enabled = false;
        //gameObject.active = false;
        flgAttack = false;
        animPlayer = gameObject.GetComponent<Animation>();
        

    }
	
	// Update is called once per frame
	void Update () {
        if(flgAttack)
        {
            animPlayer.Play(animEnemyFight.name);
        }
        
	}
}
