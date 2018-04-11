using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples {
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class InteractDestroy : MonoBehaviour {
        [SerializeField] private Material m_NormalMaterial2;
        [SerializeField] private Material m_OverMaterial2;
 //       [SerializeField] private Material m_ClickedMaterial2;
        [SerializeField] private VRInteractiveItem m_InteractiveItem2;
        [SerializeField] private Renderer m_Renderer2;


        private void Awake() {
            m_Renderer2.material = m_NormalMaterial2;
        }


        private void OnEnable() {
            m_InteractiveItem2.OnOver += CursOver;
            m_InteractiveItem2.OnOut += CursOut;
            m_InteractiveItem2.OnClick += CursClick;
   
        }


        private void OnDisable() {
            m_InteractiveItem2.OnOver -= CursOver;
            m_InteractiveItem2.OnOut -= CursOut;
            m_InteractiveItem2.OnClick -= CursClick;  
        }


        //Handle the Over event
        private void CursOver() {
            Debug.Log("Show over state");
            m_Renderer2.material = m_OverMaterial2;
        }


        //Handle the Out event
        private void CursOut() {
            Debug.Log("Show out state");
            m_Renderer2.material = m_NormalMaterial2;
        }


        //Handle the Click event
        private void CursClick() {
            Debug.Log("Show click state");
            //          m_Renderer2.material = m_ClickedMaterial2;

            Destroy(m_InteractiveItem2.gameObject);
       
        }


    }

}