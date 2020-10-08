using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoV : MonoBehaviour
{
    public Transform player;
    public float maxAngle;
    public float maxRadius;

    private bool isInFov = false;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        //quaternion is a rotation so when multiplying an angle with it you rotate the vector
        //transform.forward is the axis we revolve around
        Vector2 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.forward) * transform.up * maxRadius;
        Vector2 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.forward) * transform.up * maxRadius;

        //draw FOV angle
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        //draw line to princess
        if (!isInFov)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius);
        }

        //draw looking direction
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.up * maxRadius);

    }

    public bool inFov(Transform checkingObject, Transform target, float maxRadius, float maxAngle)
    {
        Collider2D[] overlaps = new Collider2D[10];
        int count = Physics2D.OverlapCircleNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if(overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.z *= 0;

                    float angle = Vector2.Angle(checkingObject.up, directionBetween);

                    if (angle <= maxAngle)
                    {
                        RaycastHit2D hit = new RaycastHit2D();
                        if (hit.transform == target)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    //
    private void Update()
    {
        isInFov = inFov(transform, player, maxRadius, maxAngle);
    }

}
