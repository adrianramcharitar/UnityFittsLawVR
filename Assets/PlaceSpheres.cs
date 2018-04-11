using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;
using VRStandardAssets.Examples;


//This class generates the spheres with the appropriate scrips attached to it and handels the ordering
public class PlaceSpheres : MonoBehaviour {


    // public int numPoints = 20;                        //number of points on radius to place prefabs


    //   public Vector3 centerPos = new Vector3(0, 0, 32);    //center of circle/elipsoid

    public Camera cam;

    //  public float DistanceFromCamera = 0;
    //   public float scale = 1;

    public static Vector3 centerPos;


    //   public GameObject pointPrefab;                    //generic prefab to place on each point
    //    public float radiusX, radiusY;                    //radii for each x,y axes, respectively

    public bool isCircular = false;                    //is the drawn shape a complete circle?
    public bool vertical = true;                       //is the drawb shape on the xy-plane?

    private GameObject[] sphereArray;


    //changed to static might mess up in the future when running placespheres more than once.....
    public static GameObject[] orderedArray;
    private int[] order;
    public static int currentSphere;





    Vector3 pointPos;                                //position to place each prefab along the given circle/eliptoid
                                                     //*is set during each iteration of the loop

    // Use this for initialization
    void Start() {


    }



    void Update() {


    }



    public void GenerateSpheres(float amplitude, float width, float depth, int numPoints) {


        //Initalize both arrays to size that number of spheres

        sphereArray = new GameObject[numPoints];
        orderedArray = new GameObject[numPoints + 1];
        currentSphere = 0;


        centerPos = cam.transform.position + new Vector3(0, 0, depth);



        for (int i = 0; i < numPoints; i++) {

            //multiply 'i' by '1.0f' to ensure the result is a fraction
            float pointNum = (i * 1.0f) / numPoints;

            //angle along the unit circle for placing points
            float angle = pointNum * Mathf.PI * 2;

            float x = Mathf.Sin(angle) * amplitude;
            float y = Mathf.Cos(angle) * amplitude;

            //position for the point prefab
            if (vertical)
                pointPos = new Vector3(x, y) + centerPos;
            else if (!vertical) {

                pointPos = new Vector3(x, 0, y) + centerPos;

            }

            //place the prefab at given position
            // Instantiate(pointPrefab, pointPos, Quaternion.identity);
            // Instantiate(sphere, pointPos, Quaternion.identity);


            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.name = "dynamic_sphere " + i;
            sphereArray[i] = sphere;
            sphere.gameObject.transform.localScale = new Vector3(width, width, width);
            sphere.gameObject.transform.position = pointPos;


            //   sphere.AddComponent<VRInteractiveItem>();


            //Enables script


            //      VRInteractiveItem script2 = sphereArray[i].AddComponent<VRInteractiveItem>();
            //      script2.enabled = true;

            //       VRInteractColour script = sphereArray[i].AddComponent<VRInteractColour>();
            //       script.enabled = true;


            VRInteractiveItem script2 = sphere.AddComponent<VRInteractiveItem>();
            VRInteractColour script = sphere.AddComponent<VRInteractColour>();

            script.m_InteractiveItem = sphere.GetComponent<VRInteractiveItem>();
            script.m_Renderer = sphere.GetComponent<MeshRenderer>();

            script.m_NormalMaterial = Resources.Load("black", typeof(Material)) as Material;
            script.m_OverMaterial = Resources.Load("orange", typeof(Material)) as Material;
            //   script.m_SelectMeMaterial = Resources.Load("green", typeof(Material)) as Material;
            //   script.m_ClickedMaterial = Resources.Load("red", typeof(Material)) as Material;

            script.m_SelectMeMaterial = Resources.Load("blue", typeof(Material)) as Material;
            script.m_CorrectMaterial = Resources.Load("green", typeof(Material)) as Material;
            script.m_IncorrectMaterial = Resources.Load("red", typeof(Material)) as Material;
           



            script2.enabled = true;
            script.enabled = true;

            //This seems to works, but not the above....
            //    sphere.GetComponent<MeshRenderer>().material = script.m_NormalMaterial;





        }//End for loop for creating targets


        //    SphereColourOrder();

        //Generate sphere ordering
        order = new int[numPoints + 1];
        order[0] = numPoints / 4 * 3; // 1st target is at the top (3/4 around circle)

        int inc = numPoints / 2 + numPoints % 2;
        if (numPoints % 2 == 0) // even number of targets
            for (int i = 1; i < numPoints + 1; ++i)
                order[i] = (order[i - 1] + (numPoints + 1) / 2 + ((i + 1) % 2)) % numPoints;
        else
            // odd number of targets
            for (int i = 1; i < numPoints + 1; ++i)
                order[i] = (inc + order[i - 1]) % numPoints;


        //Put sphere objects in the ordered array 
        for (int i = 0; i < orderedArray.Length; i++) {

            orderedArray[i] = sphereArray[order[i]];
        }


        /*

        //Print the generated spheres
        foreach (GameObject sp in orderedArray) {
            Debug.Log(sp);
        }
*/



    }


    public void DestroySpheres() {

        for (int i = 0; i < orderedArray.Length; i++) {

            Destroy(orderedArray[i]);
        }
    }

    /*
    public void SphereColourOrder() {


        //Order sequence taken from 
        order = new int[numPoints + 1];
        order[0] = numPoints / 4 * 3; // 1st target is at the top (3/4 around circle)

        int inc = numPoints / 2 + numPoints % 2;
        if (numPoints % 2 == 0) // even number of targets
            for (int i = 1; i < numPoints + 1; ++i)
                order[i] = (order[i - 1] + (numPoints + 1) / 2 + ((i + 1) % 2)) % numPoints;
        else
            // odd number of targets
            for (int i = 1; i < numPoints + 1; ++i)
                order[i] = (inc + order[i - 1]) % numPoints;


        //Put objects in the ordered array 
        for(int i = 0; i < orderedArray.Length; i++) {

            orderedArray[i] = sphereArray[order[i]];
        }

    }

    */





}


