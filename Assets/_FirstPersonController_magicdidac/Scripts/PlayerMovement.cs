using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller = null;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [Space]
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private float groundDistance = .4f;
    [SerializeField] private LayerMask groundMask = 0;


    private Vector3 velocity;
    private bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    /*public void TeleportToPortal(Portal portal)
    {

        Vector3 position = portal.otherPortal.transform.InverseTransformPoint(transform.position);

        controller.enabled = false;
        transform.position = portal.transform.TransformPoint(position) + (portal.transform.forward * .2f);
        controller.enabled = true;

        Vector3 direction = portal.otherPortal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = portal.transform.TransformDirection(direction);



    }*/

    public void TeleportToPortal(Portal portal, float offset)
    {
        float yDistance = transform.position.y - portal.transform.position.y;
        float xDistance = transform.position.x - portal.transform.position.x;

        controller.enabled = false;
        transform.position = portal.otherPortal.transform.position + (Vector3.right * - xDistance) + (Vector3.up * yDistance) + (portal.otherPortal.transform.forward * Mathf.Abs(offset));
        controller.enabled = true;

        //transform.forward = portal.otherPortal.transform.forward;

        Vector3 direction = portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = portal.otherPortal.transform.TransformDirection(direction);

    }

}
