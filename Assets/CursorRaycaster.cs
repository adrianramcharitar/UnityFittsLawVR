using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Examples;
using VRStandardAssets.Utils;


//This class handles the raycasting and objects hit detection

public class CursorRaycaster : MonoBehaviour {

    public LayerMask lMask;

    public GameObject pointer;

    public bool ShowDebugRay;

    public static Vector3 rayHitPos;

    public GameObject trackedHand;

    public CursorPositioner m_Reticle;

    public CursorPositioner2 m_Reticle2;


    //Object Interacion


    private VRInteractiveItem m_CurrentInteractible;                //The current interactive item
    private VRInteractiveItem m_LastInteractible;                   //The last interactive item

    public VRInput m_VrInput;

    private Ray ray;
    private RaycastHit hit;
    private Vector3 rayStart;
    private Vector3 rayDir;

    private Ray ray2;
    private RaycastHit hit2;
    private Vector3 rayStart2;
    private Vector3 rayDir2;

    private Canvas myCanvas;

    // This event is called every frame that the user's gaze is over a collider.
    public event Action<RaycastHit> OnRaycasthit;



    private void OnEnable() {
        m_VrInput.OnClick += HandleClick;
     //   m_VrInput.OnDoubleClick += HandleDoubleClick;
        m_VrInput.OnUp += HandleUp;
        m_VrInput.OnDown += HandleDown;
    }


    private void OnDisable() {
        m_VrInput.OnClick -= HandleClick;
     //   m_VrInput.OnDoubleClick -= HandleDoubleClick;
        m_VrInput.OnUp -= HandleUp;
        m_VrInput.OnDown -= HandleDown;
    }


    // Use this for initialization
    void Start () {
        myCanvas = GetComponent<Canvas>();
        lMask = ~lMask;

    }
	
	// Update is called once per frame
	void Update () {

        RaycastReticle();

    }

    void RaycastReticle() {


        // Create a ray that points forwards from the camera.

        //if here for input type

        //   if (trackedHand.GetComponent<MotionControllerRaycast>().enabled == true) {

        if (trackedHand.GetComponent<MotionController2>().enabled == true) {

           
            Vector3 fromPosition = Camera.main.transform.position;
            Vector3 toPosition = pointer.transform.position;
            // Vector3 direction = toPosition - fromPosition;


            rayStart2 = trackedHand.transform.position;
            rayDir2 = trackedHand.transform.TransformDirection(Vector3.forward);

            ray2 = new Ray(rayStart2, rayDir2);

            //Main ray
            rayStart = Camera.main.gameObject.transform.position;
            rayDir = toPosition - fromPosition;

            ray = new Ray(rayStart, rayDir);

            Raycast2Intersect();


            /*

            rayStart = trackedHand.transform.position;
            rayDir = trackedHand.transform.TransformDirection(Vector3.forward);

            ray = new Ray(rayStart, rayDir);

            if (Physics.Raycast(rayStart, rayDir, out hit, Mathf.Infinity)) {


                if (hit.collider.tag == "2dCanvas") {

                    Debug.Log("Hit canvas");
                }

            }
            //pointer.SetActive(false); 
            */

        } else if(trackedHand.GetComponent<MotionControllerRaycast>().enabled == true) {

          
              rayStart = trackedHand.transform.position;
              rayDir = trackedHand.transform.TransformDirection(Vector3.forward);

              ray = new Ray(rayStart, rayDir);

            GameObject.Find("Quad").GetComponent<Renderer>().enabled = false;

           

        } else {

            Vector3 fromPosition = Camera.main.transform.position;
            Vector3 toPosition = pointer.transform.position;
           // Vector3 direction = toPosition - fromPosition;

            rayStart = Camera.main.gameObject.transform.position;
            rayDir = toPosition - fromPosition;

            ray = new Ray(rayStart, rayDir);     
        }

        // Show the debug ray if required
       if (ShowDebugRay) {

            //This is where it draws the ray from the camera to the cursor
            Debug.DrawRay(rayStart, rayDir * 1000, Color.blue);
            Debug.DrawRay(rayStart2, rayDir2 * 1000, Color.red);
        }


        // Do the raycast forweards to see if we hit an interactive item
        if (Physics.Raycast(ray, out hit,Mathf.Infinity, lMask)) { //Test t values here
            

            //Returns Vector3 positon of the raycast
            rayHitPos = hit.point;
           
            VRInteractiveItem interactible = hit.collider.GetComponent<VRInteractiveItem>(); //attempt to get the VRInteractiveItem on the hit object
            m_CurrentInteractible = interactible;

            // If we hit an interactive item and it's not the same as the last interactive item, then call Over
            if (interactible && interactible != m_LastInteractible)
                interactible.Over();

            // Deactive the last interactive item 
            if (interactible != m_LastInteractible)
                DeactiveLastInteractible();

               m_LastInteractible = interactible;

            // Something was hit, set at the hit position

            if (m_Reticle2) {
                // m_Reticle.SetPosition(hit);

                m_Reticle2.SetPosition(hit);
            }


            if (OnRaycasthit != null) {
                OnRaycasthit(hit);
            }

        } else {
            // Nothing was hit, deactive the last interactive item.
            DeactiveLastInteractible();
            m_CurrentInteractible = null;



            // Position the reticle at default distance.
            if (m_Reticle2) {
                //  m_Reticle.SetPosition();
                m_Reticle2.SetPosition();

          
            }
        }
    }


    public void Raycast2Intersect() {

        if (Physics.Raycast(ray2, out hit2, Mathf.Infinity)) {

            if (hit2.transform.gameObject.name == "CanvasCollide") {
                //   Debug.Log("Hit Canvas " + hit2.point);

                pointer.transform.position = hit2.point;

            }
        }

        else if (Physics.Raycast(ray2, out hit2, Mathf.Infinity)) {

            if (hit2.transform.gameObject.name == "CanvasCollideR") {
                //   Debug.Log("Hit Canvas " + hit2.point);

                pointer.transform.position = new Vector3(hit2.point.x, hit2.point.y, hit2.point.z) ;

            }
        }
    }

    private void DeactiveLastInteractible() {
        if (m_LastInteractible == null)
            return;

        m_LastInteractible.Out();
        m_LastInteractible = null;
    }


    private void HandleUp() {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.Up();
    }


    private void HandleDown() {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.Down();
    }


    private void HandleClick() {
        if (m_CurrentInteractible != null) {
            m_CurrentInteractible.Click();


        } else {


            EffectiveWidth.Calculatelength();
            EffectiveWidth.lengtharray[EffectiveWidth.lengtharraycounter] = EffectiveWidth.length;
            EffectiveWidth.lengtharraycounter++;


            /*
            
            EffectiveWidth.clickCoor = rayHitPos;
            EffectiveWidth.prevSphereCoor = EffectiveWidth.currentSphereCoor;
            EffectiveWidth.currentSphereCoor = PlaceSpheres.orderedArray[PlaceSpheres.currentSphere].GetComponent<Renderer>().bounds.center;

        */

            //Play around with this to avoid click error on temrinted experiment
            //     if (ExperimentController.i < ExperimentController.permuArray.GetLength(0)) {
            //This increases error number if ray is clicked while not on an interactable object

            Logger.hitMissArray[Logger.hitMissArrayCounter] = 1;
            Logger.error = 1;


            PlaceSpheres.orderedArray[PlaceSpheres.currentSphere].GetComponent<Renderer>().material = Resources.Load("red", typeof(Material)) as Material;

                //Delta of clicked time to the last clicked time
           //     Debug.Log(Time.time - Logger.clickTime);
                Logger.timerArray[Logger.timerArrayMarker] = Time.time - Logger.clickTime;
            Logger.mt = Time.time - Logger.clickTime;

            //Set previous click time to current time
            Logger.clickTime = Time.time;

                Logger.timerArrayMarker++;
                Logger.hitMissArrayCounter++;
                Logger.numRoundErrors++;
                PlaceSpheres.currentSphere++;
                Logger.PrintRoundInfo();

            //    }

        }
    }

    /*
    private void HandleDoubleClick() {
        if (m_CurrentInteractible != null)
            m_CurrentInteractible.DoubleClick();

    }

    */
}
