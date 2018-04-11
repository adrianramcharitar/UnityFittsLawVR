using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbstickController : MonoBehaviour {


    public OVRInput.Controller ThumbStickController;
    public GameObject ThumbStickPointer;
    public float ControllerSpeed = 0.05f;
    Vector3 ThumbStickPointerPos;
    public bool PosControl;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ThumbStickPointerPos = ThumbStickPointer.GetComponent<RectTransform>().localPosition;

        Vector2 ContPos = ThumbStickPointer.GetComponent<RectTransform>().localPosition;

        if (PosControl) {
            ContPos = new Vector2(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x, OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y) * ControllerSpeed;
           
                //* ThumbStickPointer.transform.localPosition.z;
        } else {
            ContPos += new Vector2(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x, OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y) * ControllerSpeed;
        }

        float currentPanelWidth = GetComponent<RectTransform>().rect.width;
        float currentPanelHeight = GetComponent<RectTransform>().rect.height;
        ContPos.x = Mathf.Clamp(ContPos.x, -currentPanelWidth/2, currentPanelWidth/2);
        ContPos.y = Mathf.Clamp(ContPos.y, -currentPanelHeight/2, currentPanelHeight/2);

      //  ContPos.x = Mathf.Clamp(ContPos.x, (-currentPanelWidth) * ThumbStickPointer.transform.localPosition.z / 1000, (currentPanelWidth) * ThumbStickPointer.transform.localPosition.z / 1000);
      //  ContPos.y = Mathf.Clamp(ContPos.y, (-currentPanelWidth) * ThumbStickPointer.transform.localPosition.z / 1000, (currentPanelWidth) * ThumbStickPointer.transform.localPosition.z / 1000);

     //   Debug.Log((currentPanelWidth / 2) * ThumbStickPointer.transform.localPosition.z / 1000);

        SetLocalPosition(ContPos);

       

    //    Debug.Log(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick));

    }

    public void SetLocalPosition(Vector3 pos) {
        if ((ThumbStickPointer.GetComponent<RectTransform>().localPosition - pos).magnitude > 0.001f) {
            //   lastMouseActivityTime = Time.time;
        }
        //ThumbStickPointer.GetComponent<RectTransform>().localPosition = pos;

        Vector3 temp = ThumbStickPointer.GetComponent<RectTransform>().localPosition;
        temp.x = pos.x;
        temp.y = pos.y;
        ThumbStickPointer.GetComponent<RectTransform>().localPosition = temp;


    }
}
