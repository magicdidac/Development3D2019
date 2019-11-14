using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public CharacterController controller = null;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [Space]
    [SerializeField] private Transform groundCheck = null;
    [SerializeField] public PortalGun gun = null;
    [SerializeField] private float groundDistance = .4f;
    [SerializeField] private LayerMask groundMask = 0;


    private Vector3 velocity;
    private bool isGrounded;
    [HideInInspector] public bool isDead { get; private set; }


    private void Update()
    {
        if (isDead)
            return;

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

    public void TeleportToPortal(Portal portal, float offset)
    {

        controller.enabled = false;
        transform.position = GetOtherPortalPosition(portal, offset);
        controller.enabled = true;

        Vector3 direction = portal.transform.InverseTransformDirection(-transform.forward);
        transform.forward = portal.otherPortal.transform.TransformDirection(direction);

        portal.otherPortal.showMock = false;

    }

    private Vector3 GetOtherPortalPosition(Portal portal, float offset)
    {
        Vector3 otherForward = portal.otherPortal.transform.forward;
        Vector3 other = portal.otherPortal.transform.position;

        offset += .01f;

        Vector3 l_Position = portal.transform.InverseTransformPoint(transform.position);
        Vector3 returnPosition = portal.otherPortal.transform.TransformPoint(l_Position);

        if(!Utilities.IsForwarNearToZero(otherForward.z))
            returnPosition.z = other.z + (otherForward.z * offset);
        else if (!Utilities.IsForwarNearToZero(otherForward.x))
            returnPosition.x = other.x + (otherForward.x * offset);

        return returnPosition;
    }

    public void Dead()
    {
        Debug.Log("Dead");

        isDead = true;
    }

}
