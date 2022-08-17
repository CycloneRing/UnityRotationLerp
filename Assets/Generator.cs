using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Generator : MonoBehaviour
{
    // Public Values
    public StartPoint spt;
    public EndPoint ept;
    public Vector3 startPos;
    public Vector3 endPos;
    public float startRot;
    public float endRot;
    
    public int segments = 8;
    public float length = 2.0f;

    // Internal Datasets
    private List<Vector3> genPts = new List<Vector3>();
    private List<Vector3> genExtraPts = new List<Vector3>();

    

    // Generate Points
    void Update()
    {
        startPos = spt.gameObject.transform.position;
        endPos = ept.gameObject.transform.position;
        
        genPts.Clear();
        genExtraPts.Clear();

        //var start = spt.gameObject.transform;
        //var end = ept.gameObject.transform;
        

        // Base Points
        for (int i = 1; i < segments; i++)
        {
            Vector3 pt = Vector3.Lerp(startPos, endPos, (i * 1.0f) / segments);
            genPts.Add(pt);
        }

        var up = (endPos - startPos).normalized;
        var baseRight = Vector3.Cross(up, new Vector3(0.343f, -0.234f, 0.612f)).normalized;
        // Extra points
        for (int i = 0; i < genPts.Count; i++)
        {
            var blendValue = i / (float)genPts.Count;
            
            var rot = Mathf.Lerp(startRot, endRot, blendValue);
            var pt = Vector3.Lerp(startPos, endPos, blendValue);
            var rightDir = Quaternion.AngleAxis(rot, up) * baseRight;
            var leftDir = -rightDir;
            
            genExtraPts.Add(pt + rightDir);
            genExtraPts.Add(pt + leftDir);
        }
    }

    // Draw Points
    void OnDrawGizmos()
    {
        for (int i = 0; i < genPts.Count; i++)
        {
            // Draw Base Points
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(genPts[i], 0.1f);

            // Draw Extra Points
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(genExtraPts[i * 2], 0.15f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(genExtraPts[i * 2 + 1], 0.15f);

            // Draw Extra Points Line
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(genPts[i], genExtraPts[i * 2]);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(genPts[i], genExtraPts[i * 2 + 1]);
        }
    }

    // Utilities
    float ConvertRange(float min1, float max1, float min2, float max2, float value)
    {
        var range1 = max1 - min1;
        var range2 = max2 - min2;
        return (value - min1) * range2 / range1 + min2;
    }
    Vector3 LerpAngles(Vector3 from, Vector3 to, float lerp)
    {
        Vector3 result = new Vector3();
        result.x = Mathf.LerpAngle(from.x, to.x, lerp);
        result.y = Mathf.LerpAngle(from.y, to.y, lerp);
        result.z = Mathf.LerpAngle(from.z, to.z, lerp);
        return result;
    }
}
