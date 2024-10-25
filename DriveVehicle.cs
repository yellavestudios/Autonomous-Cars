using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveVehicle : MonoBehaviour
{

    public WheelCollider WC;
    public float torque = 100;
    public GameObject wheelMesh;
    public float maxSteerAngle = 20;
    public bool canTurn = false;
    // Start is called before the first frame update
    void Start()
    {
        WC = GetComponent<WheelCollider>();

    }

    void Go(float accel, float steer)
    {

        accel = Mathf.Clamp(accel, 1, -1); 
        float thrustTorque = accel * torque;    
        WC.motorTorque = thrustTorque;
        WC.steerAngle = steer;
        if (canTurn)
        {
            steer = Mathf.Clamp(steer, 1, -1) * maxSteerAngle;
            WC.steerAngle = steer;
        }

        Quaternion quat;
        Vector3 pos;
        WC.GetWorldPose(out pos, out quat);
        wheelMesh.transform.position = pos;
        wheelMesh.transform.rotation = quat;

    }

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Vertical"); // up and down arrow keys
        float s = Input.GetAxis("Horizontal"); // right and left arrow keys
        Go(a,s);
    }
}
