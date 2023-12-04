using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Reference to the object the camera should follow
    public float smoothSpeed = 0.125f; // Speed at which the camera follows the target
    public Vector3 offset; // Offset from the target's position

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (offset.z != 0.0f)
            {
                offset.x = -offset.z;
                offset.z = 0.0f;
            }
            else if (offset.x != 0.0f)
            {
                offset.z = offset.x;
                offset.x = 0.0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (offset.z != 0.0f)
            {
                offset.x = offset.z;
                offset.z = 0.0f;
            }
            else if (offset.x != 0.0f)
            {
                offset.z = -offset.x;
                offset.x = 0.0f;
            }
        }
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned to the FollowCamera script.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target); // Make the camera look at the target
    }
}
