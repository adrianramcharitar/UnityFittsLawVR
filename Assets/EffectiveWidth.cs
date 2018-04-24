using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRStandardAssets.Utils;


public class EffectiveWidth : MonoBehaviour {

    //Sphere Coordinates
    public static Vector3 currentSphereCoor;
    public static Vector3 prevSphereCoor;
    public static Vector3 clickCoor;


    //current length and length array
    public static float length;
    public static float[] lengtharray;
    public static int lengtharraycounter;


    public static float effectiveWidth;
    public static float effectiveID;

    public static float ID;

    public static float throughput;



    // Use this for initialization
    void Start () {

        prevSphereCoor = new Vector3(0,0,0);
        lengtharray = new float[ExperimentController.numberSpheres];
        lengtharraycounter = 0;

        TestSD();

    }
	
	// Update is called once per frame
	void Update () {

    }


    public static void ConsolePrint() {

      //  Debug.Log("CU: " + currentSphereCoor);
      //  Debug.Log("PR: " + prevSphereCoor);
      //  Debug.Log("ClickPos: " + clickCoor);


        //Show length in debug
      //  Debug.Log("Length: " + length);

    }

    public static void GetVectors() {

        clickCoor = CursorRaycaster.rayHitPos;
        prevSphereCoor = currentSphereCoor;
        currentSphereCoor = PlaceSpheres.orderedArray[PlaceSpheres.currentSphere].GetComponent<Renderer>().bounds.center;

    }


    public static void Calculatelength() {

        GetVectors();
        Vector3 centToSelection = clickCoor - currentSphereCoor;
        Vector3 taskAxis = currentSphereCoor - prevSphereCoor;
        Vector3 taNorm = taskAxis.normalized;

        length = Vector3.Dot(taNorm,centToSelection);

        ConsolePrint();
    }



    public static void CalculateEffectiveWidth() {

        //effectiveWidth = (float) 4.133 * getStandardDeviation(lengtharray);
        //  Debug.Log("Effective Width " + effectiveWidth);

        effectiveWidth = 4.133f * StandardDeviationSampleMod(lengtharray);


    }


    public static void CalculateEffectiveID() {

        effectiveID = (float) Math.Log((ExperimentController.permuArray[ExperimentController.i - 2][0] / effectiveWidth) + 1, 2);
     //   Debug.Log("EffectiveID " + effectiveID);


    }

    public static void CalculateID() {

        ID =(float) Math.Log((ExperimentController.permuArray[ExperimentController.i - 2][0] / ExperimentController.permuArray[ExperimentController.i - 2][1]) + 1, 2);
      //  Debug.Log(ExperimentController.permuArray[ExperimentController.i - 2][0]);
       // Debug.Log(ExperimentController.permuArray[ExperimentController.i - 2][1]);

     //   Debug.Log("ID " + ID);

    }


    public static void CalculateTP() {

        // throughput = (float) (effectiveID / Logger.timerArray.Average());

        throughput = (float)(effectiveID / TimerArrayAverage.CalculateTimerAverage(Logger.timerArray));
      

      //  Debug.Log("Throughput " + throughput);
    }


    //Console test for printing length array
    public static void PrintLenghArray() {

        for(int i = 0; i < lengtharray.Length; i++) {
          //  Debug.Log(lengtharray[i]);
        }

    }





    public void TestSD() {

         float[] testArray = { 9, 2, 5, 4, 12, 7, 8, 11, 9, 3, 7, 4, 12, 5, 4, 10, 9, 6, 9, 4 };

       // Debug.Log("SDDD" + StandardDeviationSample(testArray));
        



       // Debug.Log("TESTSD " + getStandardDeviation(testArray));

        




    }



    public static float StandardDeviationSampleMod(float[] valueList) {
        double M = 0.0;
        double S = 0.0;
        int k = 1;
        for (int i = 1; i < valueList.Length; i ++) {
            double tmpM = M;
            M += (valueList[i] - tmpM) / k;
            S += (valueList[i] - tmpM) * (valueList[i] - M);
            k++;
        }
        return (float)Math.Sqrt(S / (k - 2));
    }

    /*

    public static float getStandardDeviationPopulation(float[] doubleList) {
        double average = doubleList.Average();
        double sumOfDerivation = 0;
        foreach (double value in doubleList) {
            sumOfDerivation += (value) * (value);
        }
        double sumOfDerivationAverage = sumOfDerivation / doubleList.Length;
        return (float) Math.Sqrt(sumOfDerivationAverage - (average * average));
    }

    public static float getStandardDeviationSample(float[] values) {

        float ret = 0;
        if (values.Count() > 0) {
            //Compute the Average      
            double avg = values.Average();
            //Perform the Sum of (value-avg)_2_2      
            double sum = values.Sum(d => Math.Pow(d - avg, 2));
            //Put it all together      
            ret = (float)Math.Sqrt((sum) / (float)(values.Count() - 1));
        }
        return ret;

    */



}






