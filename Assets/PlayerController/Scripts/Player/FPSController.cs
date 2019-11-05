using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{

    /* Controls */
    [HideInInspector] public PlayerControls controls { get; private set; }

    [HideInInspector] private bool jumpInput;
    [HideInInspector] private bool runInput;
    [HideInInspector] public bool actionInput;
    [HideInInspector] private Vector2 moveInput;
    [HideInInspector] private Vector2 lookInput;

    /* LOOK */
    [HideInInspector] private float yaw; // Rotación Y
    [HideInInspector] private float ptich; // Rotación de la cámara
    [SerializeField] private float sensitivity = 0;
    [SerializeField] private float minPitch = -80.0f;
    [SerializeField] private float maxPitch = 50.0f;
    [SerializeField] private Transform pitchControllerTransform = null;

    /* MOVEMENT */
    [HideInInspector] public CharacterController characterController;
    [SerializeField] private float speed = 10.0f;

    /* GRAVITY */
    [HideInInspector] private float verticalSpeed = 0.0f;
    [HideInInspector] private bool onGround = false;
    [SerializeField] private Vector3 gravity = new Vector3();

    /* JUMP & RUN */
    [SerializeField] private float m_FastSpeedMultiplier = 1.2f;
    [SerializeField] private float m_JumpSpeed = 10.0f;


    [HideInInspector] public bool haveGun;
    [SerializeField] public Gun gun = null;
    [HideInInspector] public DamagerWithShield dmgShield;
    [HideInInspector] public UIController uiController = null;

    [SerializeField] private float maxActionDistance = 3;

    [HideInInspector] private InteractableObjectDetection ioDetection;
    [HideInInspector] private TriggerDetection triggerDetection;
    [HideInInspector] public AnimationController animationController;

    private void Start()
    {
        yaw = transform.rotation.eulerAngles.y;
        ptich = pitchControllerTransform.localRotation.eulerAngles.x;
        characterController = GetComponent<CharacterController>();

        controls = new PlayerControls();

        controls.Player.Jump.started += _ => jumpInput = true;
        controls.Player.Jump.canceled += _ => jumpInput = false;

        controls.Player.Run.performed += _ => runInput = true;
        controls.Player.Run.canceled += _ => runInput = false;

        controls.Player.Action.performed += _ => actionInput = true;
        controls.Player.Action.canceled += _ => actionInput = false;

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += _ => moveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += _ => lookInput = Vector2.zero;

        controls.Enable();

        uiController = GameManager.instance.uiController;
        dmgShield = transform.GetChild(0).GetComponent<DamagerWithShield>();

        ioDetection = new InteractableObjectDetection(Camera.main, maxActionDistance, this);
        triggerDetection = new TriggerDetection(this);
        animationController = new AnimationController(gun.GetComponent<Animation>());


        Cursor.lockState = CursorLockMode.Locked;


    }

    private void Update()
    {

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        ioDetection.NameMustBeChange();

        /* LOOK */
        float axisY = -lookInput.y;
        ptich += axisY * sensitivity * Time.deltaTime;
        ptich = Mathf.Clamp(ptich, minPitch, maxPitch);
        pitchControllerTransform.localRotation = Quaternion.Euler(ptich, 0.0f, 0.0f);

        float axisX = lookInput.x;
        yaw += axisX * sensitivity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, yaw, 0);


        /* MOVEMENT */

        Vector3 movement = Vector3.zero;

        float yawInRadius = yaw * Mathf.Deg2Rad;
        float yaw90InRadius = (yaw + 90.0f) * Mathf.Deg2Rad;
        Vector3 forward = new Vector3(Mathf.Sin(yawInRadius), 0.0f, Mathf.Cos(yawInRadius));
        Vector3 right = new Vector3(Mathf.Sin(yaw90InRadius), 0.0f, Mathf.Cos(yaw90InRadius));


        if (moveInput.y > 0)
            movement = forward;
        else if (moveInput.y < 0)
            movement = -forward;

        if (moveInput.x > 0)
            movement += right;
        else if (moveInput.x < 0)
            movement -= right;

        movement.Normalize();

        float speedMultiplier = 1;
        if (runInput)
            speedMultiplier = m_FastSpeedMultiplier;

        movement *= Time.deltaTime * speed * speedMultiplier;


        /* GRAVITY */

        verticalSpeed += gravity.y * Time.deltaTime;
        movement.y = verticalSpeed * Time.deltaTime;

        if (movement.x != 0 || movement.z != 0)
        {
            if (runInput)
                animationController.StartAnimation("Run", true);
            else
                animationController.StartAnimation("Walk", true);
        }
        else
            animationController.StartAnimation("Idle", true);

        CollisionFlags collisionFlags = characterController.Move(movement);

        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            onGround = true;
            verticalSpeed = 0.0f;
        }
        else
            onGround = false;

        if ((collisionFlags & CollisionFlags.Above) != 0 && verticalSpeed > 0.0f)
            verticalSpeed = 0.0f;


        /* SALTO */

        if (onGround && jumpInput)
        {
            jumpInput = false;

            animationController.StartAnimation("Jump",false);

            verticalSpeed = m_JumpSpeed;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        triggerDetection.TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        triggerDetection.TriggerExit(other);
    }

    public void TeleportTo(Vector3 position)
    {
        characterController.enabled = false;
        transform.position = position;
        characterController.enabled = true;
    }

}
