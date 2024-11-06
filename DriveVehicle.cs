using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveVehicle : MonoBehaviour
{

    public WheelCollider WC;
    public float torque = 300;
    public GameObject wheelMesh;
    public float maxSteerAngle = 15;
    public bool canTurn = true;
    public float maxBrakeTorque = 500;
    // Start is called before the first frame update
    void Start()
    {
        WC = GetComponent<WheelCollider>();

    }

    public void Go(float accel, float steer, float brake)
    {

        accel = Mathf.Clamp(accel, 1, -1); 
        float thrustTorque = accel * torque;    
        WC.motorTorque = thrustTorque;
        WC.steerAngle = steer;
        if (canTurn)
        {
            steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
            WC.steerAngle = steer;
        }
        else
        {
            brake = Mathf.Clamp(brake, -1, 1) * maxBrakeTorque;
            WC.brakeTorque = brake;
        }

        // this makes your wheels turn with colliders
        Quaternion quat;
        Vector3 pos;
        WC.GetWorldPose(out pos, out quat);
        wheelMesh.transform.position = pos;
        wheelMesh.transform.rotation = quat;

    }

    // Update is called once per frame
   /* void Update()
    {
        float a = Input.GetAxis("Vertical"); // up and down arrow keys
        float s = Input.GetAxis("Horizontal"); // right and left arrow keys
        Go(a, s);
    }
   */
}
