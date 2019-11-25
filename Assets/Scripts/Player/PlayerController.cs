using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Animator anim = null;
    [SerializeField] private PlayerLifeController lifeController = null;
    [Header("Movement")]
    [SerializeField] public float walkSpeed = 2;
    [SerializeField] public float runSpeed = 5;
    [SerializeField] public float jumpForce = 7;
    [SerializeField] public float doubleJumpForce = 10;
    [SerializeField] public float tripleJumpForce = 13;
    [SerializeField] private float bridgeForce = 2;
    [SerializeField] public float gravity = -9.8f;
    [SerializeField] private LayerMask groundMask = 0;

    /** Hide Atributes **/
    [HideInInspector] private StateMachine myStateMachine = new StateMachine();
    [HideInInspector] private CharacterController characterController;

    [HideInInspector] private CollisionFlags collisionFlags;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public float verticalSpeed;
    [HideInInspector] private Transform camTransform;
    [HideInInspector] private Vector3 lastForward;
    [HideInInspector] public float speed;

    [HideInInspector] private int punch = 0;
    [HideInInspector] private float lastPunchTime;

    [HideInInspector] private int jump = 0;
    [HideInInspector] private float lastJumpTime;
    [HideInInspector] public bool recentJump;
    [HideInInspector] private bool needsJump = false;

    [HideInInspector] private Transform platform;

    /** Initialization **/
    private void Start()
    {
        GameManager.instance.player = this;

        characterController = GetComponent<CharacterController>();
        myStateMachine.ChangeState(new IdleState(anim));

        camTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    /** Update **/

    private void Update()
    {
        myStateMachine.ExecuteStateUpdate();

        if (!isGrounded && recentJump)
            lastJumpTime = Time.time;
        else if (isGrounded && verticalSpeed <= 0 && recentJump)
            recentJump = false;

        ChangeState();

        UpdatePlatform();

        Move();
        ResetAnimPosition();
    }

    private void UpdatePlatform()
    {
        if(platform != null)
        {
            if (platform.eulerAngles.x != 270)
                DetachPlatform();
        }
    }

    private void ChangeState()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            myStateMachine.ChangeState(new PunchState(anim, GetNextPunch()));
            return;
        }

        if (needsJump)
        {
            myStateMachine.ChangeState(new JumpState(anim, this, 0));

            needsJump = false;

            return;
        }

        if((isGrounded && Input.GetButtonDown("Jump")))
        {
            if (Time.time > lastJumpTime + .2f)
                jump = 0;

            myStateMachine.ChangeState(new JumpState(anim, this, jump));

            jump++;

            if (jump > 2)
                jump = 2;

            needsJump = false;

            return;
        }

        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetButton("Run"))
                myStateMachine.ChangeState(new RunState(anim, this));
            else
                myStateMachine.ChangeState(new WalkState(anim, this));

            return;
        }

        myStateMachine.ChangeState(new IdleState(anim));
    }

    private void ResetAnimPosition()
    {
        anim.transform.localPosition = Vector3.zero;
    }

    /** Punchs **/

    public void EnableLeftHandPunch(bool enableHandPunch)
    {

    }

    public void EnableRightHandPunch(bool enableHandPunch)
    {

    }

    public void EnableKick(bool enableHandPunch)
    {

    }

    private PunchBehaviour.TPunchType GetNextPunch()
    {
        if (Time.time > lastPunchTime + 1.5f)
            punch = 0;

        PunchBehaviour.TPunchType type = PunchBehaviour.TPunchType.RIGHT_HAND;

        switch (punch)
        {
            case 0:
                type = PunchBehaviour.TPunchType.RIGHT_HAND;
                break;
            case 1:
                type = PunchBehaviour.TPunchType.LEFT_HAND;
                break;
            case 2:
                type = PunchBehaviour.TPunchType.FOOT;
                break;
        }

        punch++;
        lastPunchTime = Time.time;

        if (punch > 2)
            punch = 0;

        return type;

    }

    /** Movement **/
    public void Move()
    {
        Vector3 movement = Vector3.zero;
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;

        forward.y = 0;
        forward.Normalize();
        right.y = 0;
        right.Normalize();

        if (Input.GetAxisRaw("Vertical") > 0)
            movement = forward;
        else if (Input.GetAxisRaw("Vertical") < 0)
            movement = -forward;

        if (Input.GetAxisRaw("Horizontal") > 0)
            movement += right;
        else if (Input.GetAxisRaw("Horizontal") < 0)
            movement += -right;

        bool hasMovement = movement != Vector3.zero;
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

        /*if (Physics.Raycast(transform.position, Vector3.down, .1f, groundMask))
        {
            isGrounded = true;
            verticalSpeed = 0;
        }
        else
            isGrounded = false;*/

        /*isGrounded = Physics.CheckSphere(transform.position, .4f, groundMask);

        if (isGrounded && !recentJump)
            verticalSpeed = 0;*/


        anim.SetBool("isGrounded", isGrounded);

        if ((collisionFlags & CollisionFlags.Above) != 0 && verticalSpeed > 0.0f)
            verticalSpeed = 0;

        transform.forward = (hasMovement) ? new Vector3(movement.x, 0, movement.z) : lastForward;
        lastForward = transform.forward;

    }


    public string GetCurrentState()
    {
        return myStateMachine.currentState.GetType().ToString();
    }

    /** Triggers **/

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag == "Bridge")
        {
        Debug.Log("SI");
            Rigidbody rb = hit.collider.attachedRigidbody;
            rb.AddForceAtPosition(-hit.normal * bridgeForce, hit.point);
        }

        if(hit.gameObject.GetComponent<Goomba>())
        {
            if (verticalSpeed < 0)
            {
                hit.gameObject.GetComponent<Goomba>().Die();
                needsJump = true;
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Platform" && platform == null)
        {
            AttachPlatform(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Platform" && platform != null)
            DetachPlatform();
    }

    private void AttachPlatform(Transform platform)
    {
        this.platform = platform;

        transform.parent = this.platform;

    }

    private void DetachPlatform()
    {
        this.platform = null;
        transform.parent = null;
    }

    public void Hit()
    {
        lifeController.DecreaseLifes();
    }

}
