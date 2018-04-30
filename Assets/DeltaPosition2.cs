using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeltaPosition2 : MonoBehaviour {


    private Vector3 lastPosCamera;
    private Vector3 lastPosCursor;


    public GameObject canvas;
    public GameObject cursor;

    // Use this for initialization
    void Start() {

        lastPosCamera = Vector3.zero;
        lastPosCursor = Vector3.zero;
      
    }

    // Update is called once per frame
    void Update() {



        //cursor relative to plane center
      //  Vector3 difference = PlaneGenerator.cubeCenter - cursor.transform.position;
       


        Vector2 cursorPos = new Vector2 (cursor.transform.position.x, cursor.transform.position.y);
       

        //canvas relative to plane center
        Vector3 difference2 = PlaneGenerator.cubeCenter - canvas.transform.position;


        //Cursor relative to canvas


        Vector3 difference3 = canvas.GetComponent<RectTransform>().position;
     

        Vector3 difference4 = cursor.GetComponent<RectTransform>().position;
     


        float mag = Vector3.Distance(canvas.transform.position, cursor.transform.position);
        float mag2 = Vector3.Distance(canvas.transform.position, PlaneGenerator.cubeCenter);
  



            if (Input.GetButtonUp("Fire1")) {

                

                //Delta for Camera
                Vector3 deltaCamera = canvas.transform.position - lastPosCamera;

            //Delta for Cursor
            Vector3 deltaCursor = cursor.transform.position - lastPosCursor;

            lastPosCamera = canvas.transform.position;
            lastPosCursor = cursor.transform.position;

            Debug.Log("Camera: " + deltaCamera.magnitude + " Cursor: " + deltaCursor.magnitude);


        }


            //   Debug.Log("Cursor X: " + Mathf.Abs(difference3.x) + "  " + " Cursor Y: " + Mathf.Abs(difference3.y));

            /*
            if (Input.GetButtonUp("Fire1")) {

               // Vector3 delta1 = difference - lastPos1;

                //Delta for Camera
                Vector3 delta2 = difference2 - lastPos1;

                //Delta for Cursor
                Vector3 delta3 = difference3 - lastPos2;


                //Uncomment after cursor only fix

              //  Debug.Log("Delta X Cursor: " + delta3.x + " Delta Y Cursor: " + delta3.y + " || Delta Cam X: " + delta2.x + " Delta Cam Y: " + delta2.y);
              //  Debug.Log("X Ratio: " + Mathf.Abs (delta3.x / delta2.x) + " Y Ratio: " + Mathf.Abs(delta3.y / delta2.y));

              //  lastPos1 = difference;
                lastPos1 = difference2;
                lastPos2 = difference3;


            }
    */

            // Debug.Log("Cursor X: " + Mathf.Abs(difference.x) + "  " + " Cursor Y: " + Mathf.Abs(difference.y) + " Cam X: " + Mathf.Abs(difference2.x) + "  " + " Cam Y: " + Mathf.Abs(difference2.y));



        }
}
