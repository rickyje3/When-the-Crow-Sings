using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSpherical : MonoBehaviour
{
    public float sphereRadius = 1f;
    public int pointCount = 1000;
    public float viewAngle = 60f;

    List<Vector3> spherePoints = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        spherePoints = DistributePointsAcrossSphere();
    }

    private void FixedUpdate()
    {
        FindUnobstructedDirection(transform.forward);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawLineList(DistributePointsAcrossSphere().ToArray());
        Gizmos.DrawLine(Vector3.zero, FindUnobstructedDirection(transform.forward));
    }

    List<Vector3> DistributePointsAcrossSphere()
    {
        List<Vector3> points = new List<Vector3>();
        const float GoldenRatio = 1.61803f;
        float pow = .5f; // Same as the square root.
        for (int i = 0; i < pointCount; i++)
        {
            float t = i / (pointCount - 1f);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * GoldenRatio * i;

            float angle = 2 * Mathf.PI * GoldenRatio * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            points.Add(new Vector3(x, y, z) + transform.position);// *sphereRadius);
        }

        return points;
    }

    Vector3 FindUnobstructedDirection(Vector3 targetDirection)
    {
        Vector3 bestDirection = targetDirection;
        Vector3 currentForward = transform.forward; // ?????
        float furthestUnobstructedDistance = 0f;
        RaycastHit hit;

        for (int i = 0; i < 1; i++) // TODO: find what "i < BoidHelper.rayDirections.Length" should be to replace i < 1.
        {
            Vector3 direction = transform.TransformDirection(spherePoints[i]);
            if (Physics.SphereCast(transform.position, 1f ,direction, out hit, sphereRadius))
            {
                if (hit.distance > furthestUnobstructedDistance)
                {
                    bestDirection = direction;
                    furthestUnobstructedDistance = hit.distance;
                }
            }
            else
            {
                Debug.Log("Best direction: " + direction.ToString());
                return direction;
            }
        }

        Debug.Log("Best direction: " + bestDirection.ToString());
        return bestDirection;
    }
}
