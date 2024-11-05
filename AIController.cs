using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    DriveVehicle[] dhs;
    public Circuit circuit;
    public float steeringSensitivity = 0.0001f;
    Vector3 target;  // target car is going towards
    int currentWP = 0;
    Rigidbody rb; // needed for getting the speed of the car


    // Start is called before the first frame update
    void Start()
    {
        dhs = this.GetComponentsInChildren<DriveVehicle>();
        target = circuit.waypoints[currentWP].transform.position;
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //angle between where the car is facing and the distance of the waypoint from the car
        Vector3 worldTarget = this.transform.InverseTransformPoint(target);  // target relative to the car
        float distanceToTarget = Vector3.Distance(target, this.transform.position);  
        float targetAngle = Mathf.Atan2(worldTarget.x, worldTarget.z) * Mathf.Rad2Deg;  // translate from Radian to Degrees

        float a = 0.9f;
        float s = Mathf.Clamp(targetAngle * steeringSensitivity, -1,1) * Mathf.Sign(rb.velocity.magnitude); 
        for (int i = 0; i < dhs.Length; i++)
        {
            dhs[i].Go(a,s);
        }

        //update the waypoints
        if (distanceToTarget < 5) 
        {

            currentWP++;
            if (currentWP >= circuit.waypoints.Length)
            {
                currentWP = 0;
            }
            target = circuit.waypoints[currentWP].transform.position;

        }
    }
      
}
