using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController2 : MonoBehaviour {

    public OVRInput.Controller Raycontroller;
    LineRenderer laserLine;

    public bool drawRay;
    public GameObject pointer;
    Vector3 PointerPos;

    public int ControllerSpeed = 1500;



    // Use this for initialization
    void Start() {

        laserLine = GetComponent<LineRenderer>();

        if (drawRay == true) {
            laserLine.enabled = true;
        } else if (drawRay == false) {
            laserLine.enabled = false;
        }



    }

    // Update is called once per frame
    void Update() {



        //Tracked postion of Cube hand object
        transform.localPosition = OVRInput.GetLocalControllerPosition(Raycontroller);
        transform.localRotation = OVRInput.GetLocalControllerRotation(Raycontroller);

        


        //NEW STUFF 

        PointerPos = pointer.GetComponent<RectTransform>().localPosition;

        // Vector2 ContPos = pointer.GetComponent<RectTransform>().localPosition;
      //   ContPos = new Vector2(transform.TransformDirection(Vector3.forward).x, transform.TransformDirection(Vector3.forward).y) * ControllerSpeed;



        

        float currentPanelWidth = GameObject.Find("ReticleCursor").GetComponent<RectTransform>().rect.width;
        float currentPanelHeight = GameObject.Find("ReticleCursor").GetComponent<RectTransform>().rect.height;
        //  ContPos.x = Mathf.Clamp(ContPos.x, -currentPanelWidth / 2, currentPanelWidth / 2);
        //  ContPos.y = Mathf.Clamp(ContPos.y, -currentPanelHeight / 2, currentPanelHeight / 2);


        PointerPos.x = Mathf.Clamp(PointerPos.x, -currentPanelWidth / 2, currentPanelWidth / 2);
        PointerPos.y = Mathf.Clamp(PointerPos.y, -currentPanelHeight / 2, currentPanelHeight / 2);



        //  SetLocalPosition(ContPos);

        laserLine.positionCount = 2;
        laserLine.SetPosition(0, transform.position);
        //  laserLine.SetPosition(1, transform.forward * 20 + transform.position);
        laserLine.SetPosition(1, transform.forward * 2000000 + transform.position);


    }

    public void SetLocalPosition(Vector3 pos) {

     //   Debug.Log(pointer.GetComponent<RectTransform>().localPosition);
  
        Vector3 temp = pointer.GetComponent<RectTransform>().localPosition;
        temp.x = pos.x;
        temp.y = pos.y;
        pointer.GetComponent<RectTransform>().localPosition = temp;
    }




}
