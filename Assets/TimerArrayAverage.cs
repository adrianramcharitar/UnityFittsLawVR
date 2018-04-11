using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerArrayAverage : MonoBehaviour {

 //This class counts the average of the sphere timer array (not counting the 1st one due to it being a rest sphere

    public static double CalculateTimerAverage(double[] timer) {

        double sum = 0;
      

        for(int i = 1; i < timer.Length; i++) {

            sum += timer[i];
        //    Debug.Log(timer[i]);
        }
        return (sum /( timer.Length - 1));

    }
	


}
