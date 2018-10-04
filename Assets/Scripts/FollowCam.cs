using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    static public GameObject POI; // the static point of interest

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 destination;
        // if there is no poi, return to P:[0,0,0]
        if (POI == null)
        {
            destination = Vector3.zero;
        } else
        {
            // get the destination of the poi
            destination = POI.transform.position;
            // if poi is a Projectile, check to see if it's at rest
            if (POI.tag == "Projectile")
            {
                // if it is sleeping
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // return to default view
                    POI = null;
                    // in the next update
                    return;
                }
            }
        }

        // limit the x & y to minimum values
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        // interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        // force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;
        // set the camera to the destination
        transform.position = destination;
        // set the orthographicSize of the Camera to keep Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
