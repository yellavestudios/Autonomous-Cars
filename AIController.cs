using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    DriveVehicle[] dhs;
    public Circuit circuit;
    public float steeringSensitivity = 0.15f;
    Vector3 target;  // target car is going towards
    int currentWP = 0;
    Rigidbody rb; // needed for getting the speed of the car
    public GameObject brakeLight;
    private DataLogger logger; // logs data collected from car after app ends


    // Start is called before the first frame update
    void Start()
    {
        dhs = this.GetComponentsInChildren<DriveVehicle>();
        target = circuit.waypoints[currentWP].transform.position;
        rb = this.GetComponent<Rigidbody>();
        logger = GetComponent<DataLogger>(); // Initialize DataLogger

        // Start logging data
        logger.StartLogging();
    }

    // Update is called once per frame
    void Update()
    {

        // angle between where the car is facing and the distance of the waypoint from the car
        Vector3 worldTarget = this.transform.InverseTransformPoint(target);  // target relative to the car
        float distanceToTarget = Vector3.Distance(target, this.transform.position);  
        float targetAngle = Mathf.Atan2(worldTarget.x, worldTarget.z) * Mathf.Rad2Deg;  // translate from Radian to Degrees

        float a = -1f; //use -1 instead of 1, or car will accellerate backwards
        float s = Mathf.Clamp(targetAngle * steeringSensitivity, -1,1) * Mathf.Sign(rb.velocity.magnitude);
        float b = 0;

        if (distanceToTarget < 10)
        {
            b = 0.6f;
        }
        for (int i = 0; i < dhs.Length; i++)
        {
            dhs[i].Go(a,s,b);
        }

        if (b > 0)
        {
            brakeLight.SetActive(true);
        }
        else
        {
            brakeLight.SetActive(false);
        }

        logger.LogData(transform.position, rb.velocity.magnitude, s);

        // update the waypoints
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
      
    public void CaptureData()
    {
        logger.LogData(transform.position, rb.velocity.magnitude, rb.angularVelocity.magnitude);
    }

    public void OnDisable()
    {
        // Stop logging data when the script is disabled or play mode stops
        logger.StopLogging();
    }

}
