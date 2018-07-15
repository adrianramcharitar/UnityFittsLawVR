using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Handles mouse acceleration
public class MouseAcceleration : MonoBehaviour {


    //  public bool mouseAcceleration;

    public float accelerationFactorX;
    public float accelerationFactorY;


    public static float linearity;
    public static float intersection;


    public float setLinearity = 4f;
    public float setIntersection = 0.5f;

    public float accelerationMagnitude;

    //Sigmoid function parameters
    public float a;
    public float k;
    public float b;

    public bool sigmoid;
    public bool step;



    // Use this for initialization
    void Start() {

        linearity = setLinearity;
        intersection = setIntersection;

    }

    // Update is called once per frame
    void Update() {

        if (sigmoid) {
            Sigmoid(a, k, b);
        } else if (step) {
            Step();
            }else{
            float curMouseX = Input.GetAxisRaw("Mouse X");
            float curMouseY = Input.GetAxisRaw("Mouse Y");

            Debug.Log("Normal Cons: " + Mathf.Sqrt(Mathf.Pow(curMouseX, 2) + Mathf.Pow(curMouseY, 2)));

            //  Debug.Log(Input.GetAxisRaw("Mouse X"));


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
            accelerationMagnitude = new Vector2(accelerationFactorX, accelerationFactorY).magnitude;

            //  Debug.Log("Acc Normal: " + accelerationMagnitude);
        }
    }

    void Sigmoid(float a, float k, float b) {



        float curMouseX = Input.GetAxisRaw("Mouse X");
        float curMouseY = Input.GetAxisRaw("Mouse Y");

        //Debug.Log("Normal: " + Mathf.Sqrt(Mathf.Pow(curMouseX,2) + Mathf.Pow(curMouseY,2)));

        curMouseX = (k / (1 + Mathf.Exp(a + b * Mathf.Abs(curMouseX))));
        curMouseY = (k / (1 + Mathf.Exp(a + b * Mathf.Abs(curMouseY))));

        
        Debug.Log("Sig: " + Mathf.Sqrt(Mathf.Pow(curMouseX,2) + Mathf.Pow(curMouseY,2)));

        accelerationFactorX = Mathf.Abs(curMouseX);
        accelerationFactorY = Mathf.Abs(curMouseY);

        accelerationMagnitude = new Vector2(accelerationFactorX, accelerationFactorY).magnitude;
        Debug.Log(accelerationMagnitude);


    }

    void Step() {

        float stepmag;

        float curMouseX = Input.GetAxisRaw("Mouse X");
        float curMouseY = Input.GetAxisRaw("Mouse Y");

        stepmag = Mathf.Sqrt(Mathf.Pow(curMouseX, 2) + Mathf.Pow(curMouseY, 2));

        Debug.Log("Step: " + stepmag);

        if (stepmag < 3.3f) {

            accelerationFactorX = 0.3f * Mathf.Pow(curMouseX, 2);
            accelerationFactorY = 0.3f * Mathf.Pow(curMouseY, 2);

        }else if(stepmag > 3.3f && stepmag <= 16) {

            accelerationFactorX = curMouseX;
            accelerationFactorY = curMouseY;

        }else if(stepmag > 16) {
            accelerationFactorX = 0;
            accelerationFactorY = 0;
        }

        accelerationMagnitude = new Vector2(accelerationFactorX, accelerationFactorY).magnitude;



    }


}

