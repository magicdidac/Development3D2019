using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2;
    [SerializeField] private float runSpeed = 5;
    [Space]
    [SerializeField] private Animator anim = null;
    

    [HideInInspector] private Transform camTransform;
    [HideInInspector] private CharacterController characterController;
    [HideInInspector] private float speed;
    [HideInInspector] private Vector3 lastForward = Vector3.zero;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
        camTransform = Camera.main.transform;
    }

    private void Update()
    {
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

        characterController.Move(movement * speed * Time.deltaTime);


        transform.forward = (hasMovement) ? movement : lastForward;
        lastForward = transform.forward;

        anim.SetFloat("Speed", hasMovement ? (speed == runSpeed ? 1 : .2f) : 0f);

    }

}
