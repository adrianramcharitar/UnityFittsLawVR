using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

    public OVRInput.Controller controller;
    public GameObject pointer;
    public int ControllerSpeed = 6;
    Vector3 PointerPos;


    

	// Use this for initialization
	void Start () {

    //    UnityEngine.VR.InputTracking.Recenter();


    }
	
	// Update is called once per frame
	void Update () {

        PointerPos = pointer.GetComponent<RectTransform>().localPosition;

        Vector2 ContPos = pointer.GetComponent<RectTransform>().localPosition;
        ContPos = new Vector2(OVRInput.GetLocalControllerPosition(controller).x, OVRInput.GetLocalControllerPosition(controller).y) * ControllerSpeed;

        float currentPanelWidth = GetComponent<RectTransform>().rect.width;
        float currentPanelHeight = GetComponent<RectTransform>().rect.height;
        ContPos.x = Mathf.Clamp(ContPos.x, -currentPanelWidth / 2, currentPanelWidth / 2);
        ContPos.y = Mathf.Clamp(ContPos.y, -currentPanelHeight / 2, currentPanelHeight / 2);

        //  ContPos.x = Mathf.Clamp(ContPos.x, -currentPanelWidth * pointer.transform.localPosition.z / 1000, currentPanelWidth * pointer.transform.localPosition.z / 1000);
        //  ContPos.y = Mathf.Clamp(ContPos.y, -currentPanelHeight * pointer.transform.localPosition.z / 1000, currentPanelHeight * pointer.transform.localPosition.z / 1000);

        SetLocalPosition(ContPos);



        //  pointer.GetComponent<RectTransform>().localPosition = OVRInput.GetLocalControllerPosition(controller) * ControllerSpeed;

     //   Debug.Log(OVRInput.GetLocalControllerPosition(controller) * ControllerSpeed);


            //Rotation element (Might Exclude this)
          // pointer.GetComponent<RectTransform>().localRotation = OVRInput.GetLocalControllerRotation(controller);


    }

    public void SetLocalPosition(Vector3 pos) {
        if ((pointer.GetComponent<RectTransform>().localPosition - pos).magnitude > 0.001f) {
            //   lastMouseActivityTime = Time.time;
        }
        // pointer.GetComponent<RectTransform>().localPosition = pos;

        Vector3 temp = pointer.GetComponent<RectTransform>().localPosition;
        temp.x = pos.x;
        temp.y = pos.y;
        pointer.GetComponent<RectTransform>().localPosition = temp;
    }
}
