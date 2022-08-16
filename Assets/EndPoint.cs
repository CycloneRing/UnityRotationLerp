using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public float angleLeft = 10;
    public float angleRight = 10;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, 0.1f);
    }
}
