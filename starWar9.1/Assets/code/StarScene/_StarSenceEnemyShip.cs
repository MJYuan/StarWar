using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _StarSenceEnemyShip : MonoBehaviour {

    //connection


    //control
    public float contTimerAnim01;

    //Animation
    public AnimationClip anim01;
    private Animation animPlayer;

    //flag
    private bool flgAinm01;
    // Use this for initialization
    void Start()
    {
        contTimerAnim01 = 8.0f;
        flgAinm01 = false;
        animPlayer = gameObject.GetComponent<Animation>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        contTimerAnim01 -= Time.deltaTime;
        if (contTimerAnim01 <= 0)
        {
            animPlayer.Play(anim01.name);
        }
    }
}
