using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _StartSenceShip : MonoBehaviour {
    //connection


    //control
    public float contTimerAnim01;
    public float contTimerAnim02;
    

    //Animation
    public AnimationClip anim01;
    public AnimationClip anim02;
    private Animation animPlayer;

    //flag
    private bool flgAinm01;
    private bool flgAnim02;

    // Use this for initialization
    void Start () {
        contTimerAnim01 = 5.0f;
        contTimerAnim02 = 6.5f;
        flgAinm01 = false;
        flgAnim02 = false;
        animPlayer = gameObject.GetComponent<Animation>();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        contTimerAnim01 -= Time.deltaTime;
        contTimerAnim02 -= Time.deltaTime;
        
        if(!flgAinm01 && flgAnim02)
        {
            animPlayer.Play(anim02.name);
        }
        if (flgAinm01 && !flgAnim02)
        {
            animPlayer.Play(anim01.name);
        }

        if (contTimerAnim01 <=0)
        {
            flgAinm01 = true;
        }

        if(contTimerAnim02 <=0)
        {
            flgAinm01 = false;
            flgAnim02 = true;
        }
	}
}
