using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGenerator : MonoBehaviour {

    GameObject cube;
    public static Vector3 cubeCenter;



    void Start () {

        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.localScale = new Vector3(12, 6,(float) 0.000001);


        //Disable white background
        cube.GetComponent<Renderer>().enabled = false;

        
    }
	
	// Update is called once per frame
	void Update () {

        positionPlane();
        cubeCenter = cube.transform.position;

    }


    public void positionPlane() {

        cube.transform.position = PlaceSpheres.centerPos;

    }
}
