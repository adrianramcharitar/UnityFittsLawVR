using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//C01 = Mouse
//C02 = Joystick Rate
//C03 = Joystick Position
//C04 = 2D Cursor Motion
//C05 = Raycast
//C06 = Head only

//C07 = Mouse Only - Head Movement On
//C08 = Transfer Function 1 - Head Movement On
//C09 = Transfer Function 2 - Head Movement On
//C10 = Mouse Only - Head Movement Off
//C11 = Transfer Function 1 - Head Movement Off
//C12 = Transfer Function 2 - Head Movement Off


public enum State {
    C01Mouse, C02Joystickrate, C03Joystickpos, C04Motiontracked, C05Motionray, C06HeadOnly, C07MouseConstant, C08TransferFunction1, C09TransferFunction2, C10MouseConstantHOff,
    C11TransferFunction1HOff, C12TransferFunction2HOff
}

public class SetControlScheme : MonoBehaviour {

    // Use this for initialization

    public State input;

    private KBMove scriptmouse;
    private ThumbstickController scriptthumbstick;
    private MotionControllerRaycast scriptmotionray;
    //private MotionControllerRaycast scriptmotiontracked;
    private MotionController2 scriptmotiontracked;
    private DisableRotation disablerotationscript;
    private MouseAcceleration mouseaccscript;

    public float trackPosSpeed;
    public int trackRateSpeed;
    public int motionGain;
    public bool headRotationOn = true;

    public string participantCode = "null";
    public string conditonCode = "null";


   
    void Start () {

        scriptmouse = Camera.main.GetComponentInChildren<KBMove>();
        scriptthumbstick = Camera.main.GetComponentInChildren<ThumbstickController>();
        scriptmotionray = GameObject.Find("TrackedHand").GetComponent<MotionControllerRaycast>();
        //  scriptmotiontracked = GameObject.Find("TrackedHand").GetComponent<MotionControllerRaycast>();
        scriptmotiontracked = GameObject.Find("TrackedHand").GetComponent<MotionController2>();
        disablerotationscript = GameObject.Find("GameObject").GetComponent<DisableRotation>();
        mouseaccscript = GameObject.Find("ReticleCursor").GetComponent<MouseAcceleration>();
   

        if (headRotationOn == false) {
            disablerotationscript.enabled = true;
            Logger.headmove = 0;
        } else {
            disablerotationscript.enabled = false;
            Logger.headmove = 1;
        }



        if (input == State.C01Mouse) {

            conditonCode = "C01";
         

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
  
            scriptmouse.enabled = true;

        }

        if(input == State.C03Joystickpos) {

            conditonCode = "C03";

            scriptmouse.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;

            scriptthumbstick.enabled = true;
            scriptthumbstick.PosControl = enabled;

            //set speed here
            scriptthumbstick.ControllerSpeed = trackPosSpeed;

        }

        if (input == State.C02Joystickrate) {

            conditonCode = "C02";

            scriptmouse.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;

            scriptthumbstick.enabled = true;
            scriptthumbstick.PosControl = !enabled;

            //set speed here
            scriptthumbstick.ControllerSpeed = trackRateSpeed;

        }

        if (input == State.C04Motiontracked) {

            conditonCode = "C04";

            scriptmouse.enabled = false;
            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            // scriptmotiontracked.enabled = true;
            scriptmotiontracked.ControllerSpeed = motionGain;


            scriptmotiontracked.enabled = true;

        }

        if (input == State.C05Motionray) {

            conditonCode = "C05";

            scriptmotionray.enabled = true;

            scriptmouse.enabled = false;
            scriptthumbstick.enabled = false;
            scriptmotiontracked.enabled = false;

        }

        if(input == State.C06HeadOnly) {

            conditonCode = "C06";

            scriptmotionray.enabled = false;
            scriptmouse.enabled = false;
            scriptthumbstick.enabled = false;
            scriptmotiontracked.enabled = false;


        }

        if(input == State.C07MouseConstant) {

            conditonCode = "C07";

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
            scriptmouse.enabled = true;

            KBMove.gain = true;
            disablerotationscript.enabled = false;
            Logger.headmove = 1;
        }

        if(input == State.C08TransferFunction1) {

            conditonCode = "C08";

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
            scriptmouse.enabled = true;

            KBMove.gain = false;
            mouseaccscript.setLinearity = 0.5f;
            mouseaccscript.setIntersection = 5f;
            disablerotationscript.enabled = false;
            Logger.headmove = 1;

        }

        if (input == State.C09TransferFunction2) {

            conditonCode = "C09";

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
            scriptmouse.enabled = true;

            KBMove.gain = false;
            mouseaccscript.setLinearity = 5f;
            mouseaccscript.setIntersection = 5f;
            disablerotationscript.enabled = false;
            Logger.headmove = 1;

        }

        if (input == State.C10MouseConstantHOff) {

            conditonCode = "C10";

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
            scriptmouse.enabled = true;

            KBMove.gain = true;
            disablerotationscript.enabled = true;
            Logger.headmove = 0;
        }

        if (input == State.C11TransferFunction1HOff) {

            conditonCode = "C11";

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
            scriptmouse.enabled = true;

            KBMove.gain = false;
            mouseaccscript.setLinearity = 0.9f;
            mouseaccscript.setIntersection = 0.2f;
            disablerotationscript.enabled = true;
            Logger.headmove = 0;
        }

        if (input == State.C12TransferFunction2HOff) {

            conditonCode = "C12";

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
            scriptmouse.enabled = true;

            KBMove.gain = false;
            mouseaccscript.setLinearity = 5f;
            mouseaccscript.setIntersection = 5f;
            disablerotationscript.enabled = true;
            Logger.headmove = 0;
        }






        Logger.participant = participantCode;
        Logger.condition = conditonCode;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
