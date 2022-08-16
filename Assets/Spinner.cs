using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public float speed = 0.0f;
    void Update()
    {
        transform.Rotate(transform.up, speed);
    }
}
