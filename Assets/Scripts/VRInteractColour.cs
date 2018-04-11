using UnityEngine;
using VRStandardAssets.Utils;


namespace VRStandardAssets.Examples {
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class VRInteractColour : MonoBehaviour {
        public Material m_NormalMaterial;
        public Material m_OverMaterial;
        public Material m_ClickedMaterial;



        public VRInteractiveItem m_InteractiveItem;
        public Renderer m_Renderer;


        //New stuff here

        public Material m_SelectMeMaterial;
        public Material m_CorrectMaterial;
        public Material m_IncorrectMaterial;


        private void Start() {



            if (m_Renderer == null) {
                Debug.Log("Renderer not Found @ Awake");
            }

            if (m_InteractiveItem == null) {
                Debug.Log("Interactive Item not Found @ Awake");
            }

            m_Renderer.material = m_NormalMaterial;


            //seems to work for now......
            //      m_InteractiveItem.OnOver += HandleOver;
            //      m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            //     m_InteractiveItem.OnDoubleClick += HandleDoubleClick;


            //     PlaceSpheres.orderedArray[0].GetComponent<Renderer>().material = m_SelectMeMaterial;

        }


        //Changes current sphere to select to green
        private void Update() {

            HighlightNextSphere();

        }



        private void HighlightNextSphere() {


            if (PlaceSpheres.orderedArray != null && PlaceSpheres.currentSphere < PlaceSpheres.orderedArray.Length - 1) {


                PlaceSpheres.orderedArray[PlaceSpheres.currentSphere].GetComponent<Renderer>().material = m_SelectMeMaterial;

            }

        }


        /*
        private void OnEnable()
        {


            if (m_InteractiveItem == null) {
                Debug.Log("Interactive Item not Found @ OnEnable");
            }

            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
            m_InteractiveItem.OnClick += HandleClick;
            m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
        }
*/

        private void OnDisable() {
            //     m_InteractiveItem.OnOver -= HandleOver;
            //     m_InteractiveItem.OnOut -= HandleOut;
            m_InteractiveItem.OnClick -= HandleClick;
            //    m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
        }

        /*
                //Handle the Over event
                private void HandleOver()
                {
                //    Debug.Log("Show over state");
                    m_Renderer.material = m_OverMaterial;
                }
        */

        //Handle the Out event - Don't need this

        /*
    private void HandleOut()
    {
   //     Debug.Log("Show out state");
        m_Renderer.material = m_NormalMaterial;
    }

    */

        //Handle the Click event only on Interactable objects
        private void HandleClick() {
            //    Debug.Log("Show click state");

            EffectiveWidth.Calculatelength();
            EffectiveWidth.lengtharray[EffectiveWidth.lengtharraycounter] = EffectiveWidth.length;
            EffectiveWidth.lengtharraycounter++;

            if (this.gameObject == PlaceSpheres.orderedArray[PlaceSpheres.currentSphere]) {

                //Hit or miss detection
              //  Debug.Log("HIT");
                Logger.hitMissArray[Logger.hitMissArrayCounter] = 0;
                Logger.hitMissArrayCounter++;

                Logger.error = 0;

                m_Renderer.material = m_CorrectMaterial;

                //Delta of clicked time to the last clicked time
             //   Debug.Log("Sphere " + PlaceSpheres.currentSphere + ": " + (Time.time - Logger.clickTime));
                Logger.timerArray[Logger.timerArrayMarker] = Time.time - Logger.clickTime;
                Logger.mt = Time.time - Logger.clickTime;


                //Set previous click time to current time
                Logger.clickTime = Time.time;
                Logger.timerArrayMarker++;

                PlaceSpheres.currentSphere++;
                Logger.PrintRoundInfo();


            } else {

              //  Debug.Log("MISS");
                Logger.hitMissArray[Logger.hitMissArrayCounter] = 1;
                Logger.hitMissArrayCounter++;

                Logger.error = 1;
                

                //increases error if user clicks on the wrong interactive object.

                // m_Renderer.material = m_IncorrectMaterial;

                PlaceSpheres.orderedArray[PlaceSpheres.currentSphere].GetComponent<Renderer>().material = m_IncorrectMaterial;

                //Delta of clicked time to the last clicked time
              //  Debug.Log("Sphere " + PlaceSpheres.currentSphere + ": " + (Time.time - Logger.clickTime));
                Logger.timerArray[Logger.timerArrayMarker] = Time.time - Logger.clickTime;
                Logger.mt = Time.time - Logger.clickTime;

                //Set previous click time to current time
                Logger.clickTime = Time.time;
                Logger.timerArrayMarker++;

                PlaceSpheres.currentSphere++;
                Logger.PrintRoundInfo();




            }



        }


        /*

        //Handle the DoubleClick event
        private void HandleDoubleClick()
        {
        //    Debug.Log("Show double click");
            m_Renderer.material = m_DoubleClickedMaterial;
        }

    */
    }

}