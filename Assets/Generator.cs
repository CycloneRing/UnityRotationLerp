using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Generator : MonoBehaviour
{
    // Public Values
    public StartPoint spt;
    public EndPoint ept;
    public int segments = 8;
    public float length = 2.0f;

    // Internal Datasets
    private List<Vector3> genPts = new List<Vector3>();
    private List<Vector3> genExtraPts = new List<Vector3>();

    // Generate Points
    void Update()
    {
        genPts.Clear();
        genExtraPts.Clear();

        var start = spt.gameObject.GetComponent<RotationDump>();
        var end = ept.gameObject.GetComponent<RotationDump>();

        // Base Points
        for (int i = 1; i < segments; i++)
        {
            Vector3 pt = Vector3.Lerp(start.gameObject.transform.position, end.gameObject.transform.position, (i * 1.0f) / segments);
            genPts.Add(pt);
        }

        // Extra points
        for (int i = 0; i < genPts.Count; i++)
        {
            float blendValue = ConvertRange(0, genPts.Count, 0.0f, 1.0f, i);
            var localEulerFrom = start.localRotation;
            var leftAngleFrom = spt.angleLeft;
            var rightAngleFrom = spt.angleRight;

            var localEulerTo = end.localRotation;
            var leftAngleTo = ept.angleLeft;
            var rightAngleTo = ept.angleRight;

            var localEulerBlend = LerpAngles(localEulerFrom, localEulerTo, blendValue);
            var leftAngleBlend = Mathf.Lerp(leftAngleFrom, leftAngleTo, blendValue);
            var rightAngleBlend = Mathf.Lerp(rightAngleFrom, rightAngleTo, blendValue);

            var base_left_angle = Quaternion.Euler(localEulerBlend.x, localEulerBlend.y, localEulerBlend.z + leftAngleBlend) * Vector3.right;
            var base_right_angle = Quaternion.Euler(localEulerBlend.x, localEulerBlend.y, localEulerBlend.z + -rightAngleBlend) * Vector3.right;
            var leftPos = genPts[i] + -base_left_angle.normalized * length;
            var rightPos = genPts[i] + base_right_angle.normalized * length;

            genExtraPts.Add(leftPos);
            genExtraPts.Add(rightPos);
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
        result.x = Mathf.Lerp(from.x, to.x, lerp);
        result.y = Mathf.Lerp(from.y, to.y, lerp);
        result.z = Mathf.Lerp(from.z, to.z, lerp);
        return result;
    }
}
