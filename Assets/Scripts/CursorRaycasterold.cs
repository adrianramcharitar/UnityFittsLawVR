using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Examples;
using VRStandardAssets.Utils;


//This class handles the raycasting and objects hit detection

public class CursorRaycasterold : MonoBehaviour {


    public GameObject pointer;

    public bool ShowDebugRay;


    //Object Interacion


    private VRInteractiveItem m_CurrentInteractible;                //The current interactive item
    private VRInteractiveItem m_LastInteractible;                   //The last interactive item

    public VRInput m_VrInput;

    public static Vector3 rayHitPos;



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
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        RaycastReticle();


    }

    void RaycastReticle() {


        // Create a ray that points forwards from the camera.


        Vector3 fromPosition = Camera.main.transform.position;
        Vector3 toPosition = pointer.transform.position;


        Vector3 direction = toPosition - fromPosition;


        Ray ray = new Ray(Camera.main.gameObject.transform.position, direction);
        RaycastHit hit;

        // Show the debug ray if required
        if (ShowDebugRay) {

            //This is where it draws the ray from the camera to the cursor
            Debug.DrawRay(Camera.main.gameObject.transform.position, direction * 100, Color.blue);
        }


        // Do the raycast forweards to see if we hit an interactive item
        if (Physics.Raycast(ray, out hit)) {

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


            // Something was hit, set at the hit position.
            if (this.transform)

                //Fix this reticle later
                //     m_Reticle.SetPosition(hit);

                if (OnRaycasthit != null)
                    OnRaycasthit(hit);
        } else {
            // Nothing was hit, deactive the last interactive item.
            DeactiveLastInteractible();
            m_CurrentInteractible = null;



            // Position the reticle at default distance.
            // if (m_Reticle)
            //     m_Reticle.SetPosition();
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


            //Play around with this to avoid click error on temrinted experiment
            //     if (ExperimentController.i < ExperimentController.permuArray.GetLength(0)) {
            //This increases error number if ray is clicked while not on an interactable object
            // Debug.Log("MISS");
            Logger.hitMissArray[Logger.hitMissArrayCounter] = 1;
            Logger.error = 1;
            
            PlaceSpheres.orderedArray[PlaceSpheres.currentSphere].GetComponent<Renderer>().material = Resources.Load("red", typeof(Material)) as Material;

            //Delta of clicked time to the last clicked time
        //    Debug.Log("Sphere " + PlaceSpheres.currentSphere + ": " + (Time.time - Logger.clickTime));
            Logger.timerArray[Logger.timerArrayMarker] = Time.time - Logger.clickTime;
            Logger.mt = Time.time - Logger.clickTime;

            //Set previous click time to current time
            Logger.clickTime = Time.time;
            Logger.numRoundErrors++;

            Logger.timerArrayMarker++;
            Logger.hitMissArrayCounter++;

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
