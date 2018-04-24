using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Handles mouse acceleration
public class MouseAcceleration : MonoBehaviour {


    //  public bool mouseAcceleration;

    public float accelerationFactorX;
    public float accelerationFactorY;


    public static float linearity = 4f;
    public static float intersection = 0.5f;






    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        float curMouseX = Input.GetAxisRaw("Mouse X");
        float curMouseY = Input.GetAxisRaw("Mouse Y");


        //   if (mouseAcceleration) {
        //check your input to calibrate intersection points
        // Debug.Log("Acc " + curMouseX);

        //(1/(0.5+4))*  x*(Abs(x)+4)
        // float linearity = 4F;   // higher values mean more linear curve, less acceleration
        // float intersection = 0.5F; // point where accelerated curve intersects with linear curve

        //Formula:(with Mathf.Abs we can keep the minus for negative values after the multiplication)
        curMouseX = (1F / (intersection + linearity)) * curMouseX * (Mathf.Abs(curMouseX) + linearity);
        curMouseY = (1F / (intersection + linearity)) * curMouseY * (Mathf.Abs(curMouseY) + linearity);

        //Clamp if needed
        //   curMouseX = Mathf.Clamp(curMouseX, -4F, 4F);
        //   curMouseY = Mathf.Clamp(curMouseY, -4F, 4F);

        accelerationFactorX = Mathf.Abs(curMouseX);
        accelerationFactorY = Mathf.Abs(curMouseY);
      //  Debug.Log("Acc Abs X: " + accelerationFactorX + " Acc Abs Y: " + accelerationFactorY);
    }


}

