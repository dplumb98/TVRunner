using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.125f;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 positionWanted = target.position + offset; // Get the position we want our camera to move towards
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, positionWanted, smoothTime * Time.deltaTime); // Lerp the camera towards that position
        smoothedPosition.z = -10; // Maintain the camera's -10 on the z axis in the vector
        transform.position = smoothedPosition; // Set our camera's transform to the smoothedPosition
    }
}
