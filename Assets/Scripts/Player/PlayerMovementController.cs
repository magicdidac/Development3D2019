using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] public float runSpeed = 5;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float gravity = -9.8f;
    [Space]
    [HideInInspector] public Animator anim = null;
    

    [HideInInspector] private Transform camTransform;
    [HideInInspector] private CharacterController characterController;
    [HideInInspector] private float speed;
    [HideInInspector] private Vector3 lastForward = Vector3.zero;
    [HideInInspector] private CollisionFlags collisionFlags;

    [HideInInspector] private float verticalSpeed;
    [HideInInspector] private bool isGrounded;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        camTransform = Camera.main.transform;
    }

    public void Initialice(Animator anim)
    {
        this.anim = anim;
    }

    public float Move()
    {
        anim.transform.localPosition = Vector3.zero;

        Vector3 movement = Vector3.zero;
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;

        forward.y = 0f;
        forward.Normalize();
        right.y = 0f;
        right.Normalize();

        if (Input.GetKey(KeyCode.W))
            movement = forward;
        else if (Input.GetKey(KeyCode.S))
            movement = -forward;

        if (Input.GetKey(KeyCode.D))
            movement += right;
        else if (Input.GetKey(KeyCode.A))
            movement += -right;

        bool hasMovement = movement != Vector3.zero;
        speed = (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);
        movement.Normalize();

        movement *= speed * Time.deltaTime;

        verticalSpeed += gravity * Time.deltaTime;
        movement.y = verticalSpeed * Time.deltaTime;

        collisionFlags = characterController.Move(movement);

        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            isGrounded = true;
            verticalSpeed = 0;
        }
        else
            isGrounded = false;

        if ((collisionFlags & CollisionFlags.Above) != 0 && verticalSpeed > 0.0f)
            verticalSpeed = 0;

        if(isGrounded && Input.GetKey(KeyCode.Space))
        {
            //Animation

            verticalSpeed = jumpSpeed;

        }
        
        transform.forward = (hasMovement) ? new Vector3 (movement.x, 0, movement.z) : lastForward;
        lastForward = transform.forward;

        return speed;
    }

}
