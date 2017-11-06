using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExpStar : MonoBehaviour {

    //animation
    public Animation animPalyer;
    public AnimationClip animRound;

    // Use this for initialization
    void Start () {
        animPalyer = gameObject.GetComponent<Animation>();
        animPalyer.Play(animRound.name);

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
