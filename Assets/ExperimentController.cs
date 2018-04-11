using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExperimentController : MonoBehaviour {


    public PlaceSpheres SphereScript;
    public Logger LogScript;



    public static int numberSpheres = 9;


    //postion counter for permuArray
    public static int i = 0;

    public static int totalNumRounds;

    

    /*
    public List<float> targetAmplitude = new List<float>();
    public List<float> targetWidth = new List<float>();
    public List<float> targetDistance = new List<float>();

    private List<List<float>> masterList = new List<List<float>>();

        */


    //The individual user input array to be put into masArr
   static float[] tAmp = new float[] { 1, 2, 3 };
   static float[] tWid = new float[] {0.75f,0.5f, 0.25f};
   static float[] tDis = new float[] {10,20,30};


    //The array that the user input gets put into (3 parameters)
    private float[][] masArr =  {  tAmp, tWid, tDis  };


    //TODO: need to randomize this array
    public static float[][] permuArray;



    
    void Start () {

      


        permuArray =  Permutations(masArr);
    //    printArray(permuArray);
        Shuffle2d();
   //     printArray(permuArray);
        totalNumRounds = tAmp.Length * tWid.Length * tDis.Length;


        //if we have a min of 1 sphere
        if (numberSpheres != 0) {

            //initial round generated
            Debug.Log("Begin Experiment");
            PrintPreRoundInfo();
         //   LogScript.PrintPreRoundInfo();
            GenerateRound();
            i++;
        } else {
            Debug.Log("Number of Spheres is zero!");
        }


     //   test();

    }

    void Update() {


        //if the current green sphere is at the length of the ordering array call the next round
        if (PlaceSpheres.orderedArray != null && PlaceSpheres.currentSphere == PlaceSpheres.orderedArray.Length - 1) {


            //Logs pre Round info
            LogScript.PrintPreRoundInfo();

            //if i still hasen't reached the end of permu Array

            /*

            if (i < permuArray.GetLength(0)) {

                i++;
                Debug.Log(i);
                Debug.Log(permuArray.GetLength(0));

                SphereScript.DestroySpheres();
                PrintPreRoundInfo();
                GenerateRound();
                
            }//end while loop


    */

            //     LogScript.PrintPreRoundInfo();
            if (i < permuArray.GetLength(0)) {

                SphereScript.DestroySpheres();
             
                PrintPreRoundInfo();
                
                GenerateRound();

                i++;

            }else if (i == permuArray.GetLength(0)) {
                i++;
                SphereScript.DestroySpheres();
                Debug.Log("Experiment Has ended");
                GetComponent<ExperimentController>().enabled = false;

            }




            //Add the round errors to the total overall count of errors ***Prints after END of every round***
            Logger.totalErrors += Logger.numRoundErrors;
           
            LogScript.PrintErrors();
            LogScript.PrintTimeArray();
            EffectiveWidth.PrintLenghArray();
            EffectiveWidth.CalculateEffectiveWidth();
            EffectiveWidth.CalculateID();
            EffectiveWidth.CalculateEffectiveID();
            EffectiveWidth.CalculateTP();
            Logger.CalculateErrorRate();


            Logger.PrintSummaryInfo();

            //Reset counters
            Logger.numRoundErrors = 0;
            Logger.timerArrayMarker = 0;
            Logger.hitMissArrayCounter = 0;
            EffectiveWidth.lengtharraycounter = 0;

            /*
                        if (i == permuArray.GetLength(0)) {

                            Debug.Log("FINAL " + i);
                            Debug.Log(permuArray.GetLength(0));
                            SphereScript.DestroySpheres();
                            Debug.Log("Experiment Has ended");
                            GetComponent<ExperimentController>().enabled = false;
                        }
                */

        }
    }



    void GenerateRound() {

        SphereScript.GenerateSpheres((float)permuArray[i][0], (float)permuArray[i][1], (float)permuArray[i][2], (int)numberSpheres);

    }



    void PrintPreRoundInfo() {

        //Prints information about selection round
        Debug.Log("Round number " + (i + 1) + " out of " + (totalNumRounds));
        Debug.Log("Amplitude: " + permuArray[i][0] + " Width: " + permuArray[i][1] + " Depth: " + permuArray[i][2]);

    }




    void Test() {

      


        var rowId = 1;
        var colId = 0;

        foreach (var row in permuArray) {
            row.ToList().ForEach(value => Debug.Log(
                 string.Format("{0} - {1} - {2}", rowId, (char)('A' + colId++), value)));

            rowId += 1;

           Debug.Log("--------------------------");
        }

      //  Debug.Log(masArr.Length);
      //  Debug.Log(permuArray.Length);

    }



   public T[][] Permutations<T>(T[][] vals) {
        int numCols = vals.Length;
        int numRows = vals.Aggregate(1, (a, b) => a * b.Length);

        var results = Enumerable.Range(0, numRows)
                                .Select(c => new T[numCols])
                                .ToArray();

        int repeatFactor = 1;
        for (int c = 0; c < numCols; c++) {
            for (int r = 0; r < numRows; r++)
                results[r][c] = vals[c][r / repeatFactor % vals[c].Length];
            repeatFactor *= vals[c].Length;
        }

        return results;
    }


    void Shuffle2d() {
        System.Random rnd = new System.Random();

        for (int i = permuArray.Length - 1; i >= 1; i--) {
            // Random.Next generates numbers between min and max - 1 value, so we have to balance this
            int j = rnd.Next(0, i + 1);

            if (i != j) {
                var temp = permuArray[i];
                permuArray[i] = permuArray[j];
                permuArray[j] = temp;
            }
        }
    }

    void printArray(float[][] arr) {

        for (int i = 0; i < arr.Length; i++) {
            float[] innerArray = arr[i];
            for (int a = 0; a < innerArray.Length; a++) {
                Debug.Log(innerArray[a] + " ");
            }

            Debug.Log("\n");
        }

        Debug.Log("DONEARRAY!");
    }

}







