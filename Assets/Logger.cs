using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Logger : MonoBehaviour {

    //Get Control Scheme Script to write to Logger files

    public SetControlScheme controlScript;

    //ERROR RATE
    //This class logs and outputs relvant information for selection trials
    public static int numRoundErrors = 0;
    public static int totalErrors = 0;


    //Hit miss array
    public static int[] hitMissArray;
    public static int hitMissArrayCounter = 0;



    //MOVEMENT TIME
    //Movment time from one sphere to the other should be printed an array
    public static double movementTime = 0;

    public static int timerArrayMarker = 0;
    public static double[] timerArray;

    public static float clickTime;
    public static float curTime;

    //keep track of time array
    int timerarraycounter = 0;



    public static StreamWriter writeRound = new StreamWriter(@"C:\Users\adria\Desktop\log_round.txt");
    public static StreamWriter writeSummary = new StreamWriter(@"C:\Users\adria\Desktop\log_summary.txt");

    public static StreamWriter writeRound2 = new StreamWriter(@"C:\Users\adria\Desktop\log_round2.txt" , true);
    public static StreamWriter writeSummary2 = new StreamWriter(@"C:\Users\adria\Desktop\log_summary2.txt" , true);


    //PER SELECTION VARIABLES

    public static string participant;
    public static string condition;
    public static int block = 1;
    public static int trial;
    public static float amp;
    public static float width;
    public static float depth;
    public static float dx;
    public static float mt;
    public static int error;
    public static int headmove;

    //Magnitude is cursor change
    public static float cursorMag;
    public static float cameraMag;

    //PER ROUND VARIABLES

    public static string participantRound; 
    public static string conditionRound; 
    public static int blockRound = 1;
    public static int totalNumTrials;
    public static float ampRound;
    public static float widthRound;
    public static float depthRound;
    public static float id;
    public static float effectiveWidth;
    public static float effectiveid;
    public static float averageMt;
    public static float errorRate;
    public static float TP;

    //Acceleration function
    public static float acclin;
    public static float accint;





    // Use this for initialization

    void Start () {


        //Flushes automatically after every WriteLine call
        writeRound.AutoFlush = true;
        writeSummary.AutoFlush = true;
        writeRound2.AutoFlush = true;
        writeSummary2.AutoFlush = true; 


        timerArray = new double[ExperimentController.numberSpheres];
        hitMissArray = new int[ExperimentController.numberSpheres];

        clickTime = 0;

       
        writeRound2.WriteLine("Participant,Condition,Block,Trial,A,W,Depth,dx,MT(s),Error,Head Tracking,CursorMag,CameraMag");

        if (KBMove.gain == false) {
            writeSummary2.WriteLine("Participant,Condition,Block,Trials,A,W,Depth,id,We,IDe,MT(s),Error Rate,TP,Head Tracking,Intersection,Linearity");
        } else {
            writeSummary2.WriteLine("Participant,Condition,Block,Trials,A,W,Depth,id,We,IDe,MT(s),Error Rate,TP");
        }

       

    }
	
	// Update is called once per frame
	void Update () {

    
    }

    // Comback and Fix sphere numbering of spheres
    public static void  PrintRoundInfo() {

      
        trial = PlaceSpheres.currentSphere;
        amp = ExperimentController.permuArray[ExperimentController.i - 1][0];
        width = ExperimentController.permuArray[ExperimentController.i - 1][1];
        depth = ExperimentController.permuArray[ExperimentController.i - 1][2];
        dx = EffectiveWidth.length;

        cameraMag = DeltaPosition2.cameraMag;
        cursorMag = DeltaPosition2.cursorMag;

        /*
        if(PlaceSpheres.currentSphere == 1) {

            writeRound2.Write("NULLSPHERE ");
        }
        */

        writeRound2.WriteLine(participant + "," + condition + "," + block + "," + trial + "," + amp + "," + width + "," + depth + "," + dx + "," + mt + "," + error + "," + headmove + "," + cursorMag + "," + cameraMag);

        //  Debug.Log(participant + " " + condition + " " + block + " " + trial + " " + amp + " " + width + " " + depth + " " + dx + " " + mt + " " + error);

       


    }


    public static void PrintSummaryInfo() {

        //Print participant and Condition Info
        /*
        participant = SetControlScheme.participantCode;
        condition = SetControlScheme.conditonCode;

        participantRound = SetControlScheme.participantCode;
        conditionRound = SetControlScheme.conditonCode;
        */

        totalNumTrials = ExperimentController.totalNumRounds;
        ampRound = ExperimentController.permuArray[ExperimentController.i-2][0];
        widthRound = ExperimentController.permuArray[ExperimentController.i-2][1];
        depthRound = ExperimentController.permuArray[ExperimentController.i-2][2];
        id = EffectiveWidth.ID;
        effectiveWidth = EffectiveWidth.effectiveWidth;
        effectiveid = EffectiveWidth.effectiveID;
        // averageMt = (float) timerArray.Average();
        averageMt = (float) TimerArrayAverage.CalculateTimerAverage(timerArray);
        TP = EffectiveWidth.throughput;

        accint = MouseAcceleration.intersection;
        acclin = MouseAcceleration.linearity;

        if (KBMove.gain == false) {
            writeSummary2.WriteLine(participant + "," + condition + "," + block + "," + totalNumTrials + "," + ampRound + "," + widthRound + "," + depthRound + "," + id + "," + effectiveWidth + "," + effectiveid + "," + averageMt + "," + errorRate + "," + TP + "," + headmove + "," + accint + "," + acclin);
        } else {
            writeSummary2.WriteLine(participant + "," + condition + "," + block + "," + totalNumTrials + "," + ampRound + "," + widthRound + "," + depthRound + "," + id + "," + effectiveWidth + "," + effectiveid + "," + averageMt + "," + errorRate + "," + TP);
        }
      


    }


   public void PrintErrors() {


       // if (PlaceSpheres.currentSphere == PlaceSpheres.orderedArray.Length - 1) {


     //       Debug.Log("Number of Errors for this round: " + numErrors);

        writeRound.WriteLine("Number of Errors for this round: " + numRoundErrors);
        writeRound.WriteLine("Total Number of Errors: " + totalErrors);

        //Fix total errors later.....
        writeRound.WriteLine("Error Rate for this round: " + (numRoundErrors/ExperimentController.numberSpheres) * 100 + "%");
        writeRound.WriteLine("Overall Error Rate: " + (totalErrors /(ExperimentController.numberSpheres * ExperimentController.totalNumRounds)) * 100 + "%");
        writeRound.WriteLine(" ");

        //   }

    }


    public void PrintTimeArray() {

        for(int i = 0; i < timerArray.Length; i++) {

            if (i == 0) {

                writeRound.WriteLine("Sphere: " + i + ": Initializer Sphere");

            } else {


                writeRound.WriteLine("Sphere: " + i + " Time: " + timerArray[i] + " ms" + " " + PrinthitMissArray(i));
            }
        }

        writeRound.WriteLine("----------------------------------------------------");

    }

    public string PrinthitMissArray(int i) {


            if(hitMissArray[i] == 0) {
                return("HIT");
            }else if(hitMissArray[i] == 1) {
                return("MISS");
        } else {
            return null;
        }
    }



    public static void CalculateErrorRate() {

        int numErrors = 0;

        for (int i = 0; i < hitMissArray.Length; i++) {
            if(hitMissArray[i] == 1) {
                numErrors++;
            }
        }

        errorRate =  ((float)numErrors /(float) hitMissArray.Length) * 100;
       // Debug.Log(errorRate);

    }

  public void PrintPreRoundInfo() {

        //Prints information about selection round
        writeRound.WriteLine("Round number " + (ExperimentController.i) + " out of " + (ExperimentController.totalNumRounds));
        writeRound.WriteLine("Amplitude: " + ExperimentController.permuArray[ExperimentController.i-1][0] + " Width: " + ExperimentController.permuArray[ExperimentController.i-1][1] + " Depth: " + ExperimentController.permuArray[ExperimentController.i-1][2]);
        writeRound.WriteLine(" ");
    }


}
