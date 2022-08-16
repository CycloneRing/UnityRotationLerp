using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RotationDump : MonoBehaviour
{
    public Vector3 localRotation; // Maximum to Return to 0 is 48240
    public Vector3 distances;
    public Quaternion lastRotation;
    public float maximumDistance = 90.0f;

    // Update is called once per frame
    void Update()
    {
        if (lastRotation != this.transform.rotation)
        {
            // Track Changes
            distances.x = transform.rotation.eulerAngles.x - lastRotation.eulerAngles.x;
            distances.y = transform.rotation.eulerAngles.y - lastRotation.eulerAngles.y;
            distances.z = transform.rotation.eulerAngles.z - lastRotation.eulerAngles.z;
            if (distances.x < 0) distances.x = -distances.x;
            if (distances.y < 0) distances.y = -distances.y;
            if (distances.z < 0) distances.z = -distances.z;
            if (distances.x < maximumDistance) localRotation.x += distances.x;
            if (distances.y < maximumDistance) localRotation.y += distances.y;
            if (distances.z < maximumDistance) localRotation.z += distances.z;

            // Update
            lastRotation = this.transform.rotation;
        }

        // localRotation = this.transform.localEulerAngles;
    }
}
