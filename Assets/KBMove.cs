using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBMove : MonoBehaviour {

    public GameObject pointer;

    public float mouseMoveSpeed = 5;

    public MouseAcceleration accScript;
    public static bool gain;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {



        Vector2 mousePos = pointer.GetComponent<RectTransform>().localPosition;
        if (gain == true) {
            mousePos += new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * mouseMoveSpeed;
        }else if(gain == false) {
          //mousePos += new Vector2(Input.GetAxis("Mouse X") * accScript.accelerationFactorX, Input.GetAxis("Mouse Y") * accScript.accelerationFactorY) * mouseMoveSpeed;
            mousePos += new Vector2(Input.GetAxis("Mouse X") * accScript.accelerationFactorX, Input.GetAxis("Mouse Y") * accScript.accelerationFactorY) * mouseMoveSpeed;
        }
        float currentPanelWidth = GetComponent<RectTransform>().rect.width;
        float currentPanelHeight = GetComponent<RectTransform>().rect.height;
        mousePos.x = Mathf.Clamp(mousePos.x, -currentPanelWidth / 2, currentPanelWidth / 2);
        mousePos.y = Mathf.Clamp(mousePos.y, -currentPanelHeight / 2, currentPanelHeight / 2);

       // mousePos.x = Mathf.Clamp(mousePos.x, (-currentPanelWidth / 2) * pointer.transform.localPosition.z / 1000, (currentPanelWidth / 2) * pointer.transform.localPosition.z / 1000);
       // mousePos.y = Mathf.Clamp(mousePos.y, (-currentPanelHeight / 2) * pointer.transform.localPosition.z / 1000, (currentPanelHeight / 2) * pointer.transform.localPosition.z / 1000);

       // Debug.Log(currentPanelWidth);
       // Debug.Log(currentPanelHeight);


        SetLocalPosition(mousePos);


//        Debug.DrawRay(pointer.transform.position, pointer.transform.rotation * Vector3.forward * 100, Color.green);

    }

    public void SetLocalPosition(Vector3 pos) {
        if ((pointer.GetComponent<RectTransform>().localPosition - pos).magnitude > 0.001f) {
         //   lastMouseActivityTime = Time.time;
        }
     //   pointer.GetComponent<RectTransform>().localPosition = pos;

        //TESTING THIS
        Vector3 temp = pointer.GetComponent<RectTransform>().localPosition;
        temp.x = pos.x;
        temp.y = pos.y;
        pointer.GetComponent<RectTransform>().localPosition = temp;
    }
}
