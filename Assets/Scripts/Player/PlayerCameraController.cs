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

    [HideInInspector] private Vector3 desiredPosition;
    [HideInInspector] private Vector3 direction;
    [HideInInspector] private float distance;

    [HideInInspector] private bool moveBack = false;

    [HideInInspector] private InputMaster controls;
    [HideInInspector] private Vector2 mouseInput;

    private void Start()
    {
        controls = GameManager.instance.controls;


        controls.Player.Look.performed += ctx => mouseInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += _ => mouseInput = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (GameManager.instance.player.lifeController.currentLifes <= 0)
            return;

        float mouseX = mouseInput.x;
        float mouseY = mouseInput.y;

        desiredPosition = transform.position;
        direction = transform.forward;
        distance = Vector3.Distance(transform.position, player.position);

        if (Mathf.Abs(mouseX) < .01f && Mathf.Abs(mouseY) < .01f && !Input.anyKey)
        {
            Invoke("ActiveMoveToBack", 5);
        }
        else
        {
            CancelInvoke();
            moveBack = false;
        }

        float yaw = 0;
        float pitch = 0;

        if (!moveBack)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            yaw = (eulerAngles.y + 180);
            pitch = eulerAngles.x;
        }
        else
        {
            yaw = player.eulerAngles.y + 180;
            pitch = 25;
        }


        CalculatePosition(mouseX, mouseY, yaw, pitch);

        transform.forward = (moveBack)? Vector3.MoveTowards(transform.forward, direction, .1f) : direction;
        transform.position = (moveBack)? Vector3.MoveTowards(transform.position, desiredPosition, .5f) : desiredPosition;

    }

    private void ActiveMoveToBack()
    {
        moveBack = true;
    }

    private void CalculatePosition(float mouseX, float mouseY, float yaw, float pitch)
    {
        yaw += sensitivity * mouseX * Time.deltaTime;
        yaw *= Mathf.Deg2Rad;

        if (pitch > 180f)
            pitch -= 360;

        pitch += sensitivity * (-mouseY) * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        pitch *= Mathf.Deg2Rad;

        desiredPosition = player.position + new Vector3(Mathf.Sin(yaw) * Mathf.Cos(pitch) * distance, Mathf.Sin(pitch) * distance, Mathf.Cos(yaw) * Mathf.Cos(pitch) * distance);

        direction = player.position - desiredPosition;

        direction.Normalize();

        if (distance > maxDistanceToLookAt)
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

    }

}
