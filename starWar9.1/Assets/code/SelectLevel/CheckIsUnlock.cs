using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIsUnlock : MonoBehaviour {

    //connection



    //attribute
    private GameObject attrOldGameObject;
    private GameObject attrNewGameObject;
    Vector3 attrTargetPosition;
    RaycastHit hitInfo;
    Ray ray;


    // Use this for initialization
    void Start () {
        attrTargetPosition = new Vector3(922.0f, 570.0f, 0.0f);
        attrNewGameObject = null;
        attrOldGameObject = null;
	}
	
	// Update is called once per frame
	void Update () {

        /*
         
        Debug.Log(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
            
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
                GameObject gameObj = hitInfo.collider.gameObject;
                Debug.Log("click object name is " + gameObj.name);
                
                if (gameObj.tag == "boot")//当射线碰撞目标为boot类型的物品 ，执行拾取操作
                {
                    Debug.Log("pick up!");
                }
                
            }
        }
        */
        

        
        ray = Camera.main.ScreenPointToRay(attrTargetPosition);//从摄像机发出到点击坐标的射线

        if (Physics.Raycast(ray, out hitInfo))
        {
            
            attrNewGameObject = hitInfo.collider.gameObject;
            if (attrNewGameObject != null && attrNewGameObject != attrOldGameObject)
            {
                
                attrNewGameObject.GetComponent<SelectLevel>().BeLook();
                attrOldGameObject = attrNewGameObject;
            }
        }
        
    
    }
}
