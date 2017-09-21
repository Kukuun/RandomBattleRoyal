using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    // The position that that camera will be following.
    public Transform target;
    // The speed with which the camera will be following.
    public float cameraSmoothing = 5f;
    // The initial offset from the target.
    Vector3 offset;                     

    void Start() {
        // Calculate the initial offset.
        offset = transform.position - target.position;
    }

    void FixedUpdate() {
        // Create a postion the camera is aiming for based on the offset from the target.
        Vector3 targetCamPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        // Smoothly interpolate between the camera's current position and it's target position.
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, cameraSmoothing * Time.deltaTime);
    }
}
