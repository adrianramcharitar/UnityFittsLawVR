using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//C01 = Mouse
//C02 = Joystick Rate
//C03 = Joystick Position
//C04 = 2D Cursor Motion
//C05 = Raycast
//C06 = Head only


public enum State {
    mouse, joystickrate, joystickpos, motiontracked, motionray, head
}

public class SetControlScheme : MonoBehaviour {

    // Use this for initialization

    public State input;

    private KBMove scriptmouse;
    private ThumbstickController scriptthumbstick;
    private MotionControllerRaycast scriptmotionray;
    //private MotionControllerRaycast scriptmotiontracked;
    private MotionController2 scriptmotiontracked;

    public float trackPosSpeed;
    public int trackRateSpeed;
    public int motionGain;

    public string participantCode = "null";
    public string conditonCode = "null";


   
    void Start () {

        scriptmouse = Camera.main.GetComponentInChildren<KBMove>();
        scriptthumbstick = Camera.main.GetComponentInChildren<ThumbstickController>();
        scriptmotionray = GameObject.Find("TrackedHand").GetComponent<MotionControllerRaycast>();
        //  scriptmotiontracked = GameObject.Find("TrackedHand").GetComponent<MotionControllerRaycast>();
        scriptmotiontracked = GameObject.Find("TrackedHand").GetComponent<MotionController2>();



        if (input == State.mouse) {

            conditonCode = "C01";
         

            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;
  

            scriptmouse.enabled = true;

           

        }

        if(input == State.joystickpos) {

            conditonCode = "C03";

            scriptmouse.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;

            scriptthumbstick.enabled = true;
            scriptthumbstick.PosControl = enabled;

            //set speed here
            scriptthumbstick.ControllerSpeed = trackPosSpeed;

        }

        if (input == State.joystickrate) {

            conditonCode = "C02";

            scriptmouse.enabled = false;
            scriptmotionray.enabled = false;
            scriptmotiontracked.enabled = false;

            scriptthumbstick.enabled = true;
            scriptthumbstick.PosControl = !enabled;

            //set speed here
            scriptthumbstick.ControllerSpeed = trackRateSpeed;

        }

        if (input == State.motiontracked) {

            conditonCode = "C04";

            scriptmouse.enabled = false;
            scriptthumbstick.enabled = false;
            scriptmotionray.enabled = false;
            // scriptmotiontracked.enabled = true;
            scriptmotiontracked.ControllerSpeed = motionGain;


            scriptmotiontracked.enabled = true;

        }

        if (input == State.motionray) {

            conditonCode = "C05";

            scriptmotionray.enabled = true;

            scriptmouse.enabled = false;
            scriptthumbstick.enabled = false;
            scriptmotiontracked.enabled = false;

        }

        if(input == State.head) {

            conditonCode = "C06";

            scriptmotionray.enabled = false;
            scriptmouse.enabled = false;
            scriptthumbstick.enabled = false;
            scriptmotiontracked.enabled = false;


        }

      

        Logger.participant = participantCode;
        Logger.condition = conditonCode;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
