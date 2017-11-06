using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class _StartScenceMainCamera : MonoBehaviour {

    //connection
    public GameObject connEnemyGroup;
    public GameObject connMainCamera;
    public GameObject connEnemyCamera;
    public GameObject connMyShip;
    public GameObject connEnemyShip;

    //control
    public float contTimerEnemyGroup;
    public float contTimerCamera;


    //Animation
    public AnimationClip animCamera;
    private Animation animPlayer;
   
	// Use this for initialization
	void Start () {
        contTimerEnemyGroup = 6.0f;
        contTimerCamera = 23.0f;
        connEnemyGroup.SetActive(false);
        //animPlayer = gameObject.GetComponent<Animation>();
        //animPlayer.Play(animCamera.name);
	}
	
    private void ActiveFalse()
    {
        connEnemyCamera.SetActive(false);
        connMainCamera.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        contTimerEnemyGroup -= Time.deltaTime;
        contTimerCamera -= Time.deltaTime;
        if (contTimerEnemyGroup <= 0)
            connEnemyGroup.SetActive(true);
        if (contTimerCamera <=0)
        {
            ActiveFalse();
            connEnemyCamera.SetActive(true);
            connEnemyShip.SetActive(true);
            connMyShip.SetActive(true);
            connEnemyGroup.GetComponent<_StarSenceEnemyGroup>().flgAttack = true;
        }
        if(contTimerCamera <= -11)
        {
            SceneManager.LoadScene(2);
        }
	}
}
