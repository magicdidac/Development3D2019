using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float maxDistanceToLookAt = 5;
    [SerializeField] private float minDistanceToLookAt = 1;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private float offsetOnCollision = .5f;
    [Space]
    [SerializeField] private float sensitivity = 6;
    [SerializeField] private float minPitch = -50;
    [SerializeField] private float maxPitch = 80;
    [Space]
    [SerializeField] private Transform player = null;


    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 desiredPosition = transform.position;

        Vector3 direction = transform.forward;
        float distance = Vector3.Distance(transform.position, player.position);

        //if ( Mathf.Abs(mouseX) > .01f || Mathf.Abs(mouseY) > .01f)
        //{
            Vector3 eulerAngles = transform.eulerAngles;
            float yaw = (eulerAngles.y + 180);
            float pitch = eulerAngles.x;

            yaw += sensitivity * mouseX * Time.deltaTime;
            yaw *= Mathf.Deg2Rad;

            if (pitch > 180f)
                pitch -= 360;

            pitch += sensitivity * (-mouseY) * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
            pitch *= Mathf.Deg2Rad;

            desiredPosition = player.position + new Vector3(Mathf.Sin(yaw) * Mathf.Cos(pitch) * distance, Mathf.Sin(pitch) * distance, Mathf.Cos(yaw) * Mathf.Cos(pitch) * distance);

            direction = player.position - desiredPosition;

        //}

        direction.Normalize();

        if(distance > maxDistanceToLookAt)
        {
            distance = maxDistanceToLookAt;
            desiredPosition = player.position - direction * maxDistanceToLookAt;
        }

        if (distance < minDistanceToLookAt)
        {
            distance = minDistanceToLookAt;
            desiredPosition = player.position - direction * minDistanceToLookAt;
        }

        RaycastHit hit;

        Ray ray = new Ray(player.position, -direction);
        if (Physics.Raycast(ray, out hit, distance, raycastLayerMask.value))
        {

            desiredPosition = hit.point + direction * offsetOnCollision;
        }

        transform.forward = direction;
        transform.position = desiredPosition;

    }

}
