using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour {

    public Transform target;                        // target to follow
    Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.175f;               // delay time to follow target
    public float xMinValue;                         // minimum x bound
    public float xMaxValue;                         // maximum x bound
    public float yMinValue;                         // minimum y bound
    public float yMaxValue;                         // maximum y bound

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position;        // the target's position

        // clamp camera position.x to target within min/max
        if (target.position.x > xMinValue && target.position.x < xMaxValue)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        }
        else if (target.position.x < xMinValue)
        {
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, target.position.x);
        }
        else if (target.position.x > xMaxValue)
        {
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);
        }

        // clamp camera position.x to target within min/max
        if (target.position.y > yMinValue && target.position.y < yMaxValue)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        }
        else if (target.position.y < yMinValue)
        {
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, target.position.y);
        }
        else if (target.position.y > yMaxValue)
        {
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);
        }

        targetPos.z = transform.position.z;                                         

        // smooth a delayed follow
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
