using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour  //This script resets the car back to its original position and orientation if it flips over
{

    Rigidbody rb;
    float lastTimeChecked;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // rigidbody on car
    }

    void RightSideUpCar()
    {
        this.transform.position += Vector3.up;  // moves car up for rotation to correct direction
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward); // puts car in direction it was moving forward
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || rb.velocity.magnitude > 1f) 
        { 
            lastTimeChecked = Time.time;
        }

        if (Time.time > lastTimeChecked + 3)
        {
            RightSideUpCar();
        }

    }
}
