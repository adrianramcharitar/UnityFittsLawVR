using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class DeltaPosition : MonoBehaviour {

    public Vector3 delta;
    private Vector3 lastPos;

    public GameObject pointer;


    // Use this for initialization
    void Start() {
        delta = Vector3.zero;
        lastPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {

        //   Debug.Log(pointer.GetComponent<RectTransform>().localPosition);

        if (Input.GetButtonUp("Fire1")) {

            delta = pointer.GetComponent<RectTransform>().localPosition - lastPos;

            GetMagnitude(delta);
            Debug.Log("Delta X: " + delta.x + " Delta Y: " + delta.y);

            lastPos = pointer.GetComponent<RectTransform>().localPosition;
        }
    }

    private void GetMagnitude(Vector3 vector) {
        delta.x = Mathf.Abs(vector.x);
        delta.y = Mathf.Abs(vector.y);
        delta.z = Mathf.Abs(vector.z);
    }
}
