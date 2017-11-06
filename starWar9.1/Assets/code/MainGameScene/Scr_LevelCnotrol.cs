using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scr_LevelCnotrol : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReGame()
    {
        SceneManager.LoadScene(3);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
