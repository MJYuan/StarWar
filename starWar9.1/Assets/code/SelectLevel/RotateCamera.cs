using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{


    //connection
    public Transform connLevel;


    //attribute
    private float attrMousX;
    public float attrRoteSpeed;

    //control


    //flag


    //Animation

    // Use this for initialization
    void Start()
    {
        attrRoteSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            attrMousX = Input.GetAxis("Mouse X") * attrRoteSpeed;
            connLevel.transform.Rotate(new Vector3(0, attrMousX, 0));
        }
        
    }

    public void LoginLevel(int attrSelectID)
    {
        SceneManager.LoadScene(attrSelectID+2);
    }

    public void Next()
    {
        connLevel.transform.Rotate(new Vector3(0, 45, 0));
    }

    public void Previous()
    {
        connLevel.transform.Rotate(new Vector3(0, -45, 0));
    }


}
