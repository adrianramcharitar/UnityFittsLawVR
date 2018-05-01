using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
//using UnityEngine.VR;
public class DisableRotation : MonoBehaviour {

    // Use this for initialization
    void Start() {



    }

    // Update is called once per frame
    void Update() {

        //   transform.localPosition = -InputTracking.GetLocalPosition(VRNode.CenterEye);
        transform.localRotation = Quaternion.Inverse(InputTracking.GetLocalRotation(VRNode.CenterEye));


    }
}
