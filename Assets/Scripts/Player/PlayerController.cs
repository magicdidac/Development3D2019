using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Animator anim = null;
    [SerializeField] public PlayerLifeController lifeController = null;
    [SerializeField] public PlayerCoinController coinController = null;
    [Header("Movement")]
    [SerializeField] public float walkSpeed = 2;
    [SerializeField] public float runSpeed = 5;
    [SerializeField] public float jumpForce = 7;
    [SerializeField] public float doubleJumpForce = 10;
    [SerializeField] public float tripleJumpForce = 13;
    [SerializeField] private float bridgeForce = 2;
    [SerializeField] public float gravity = -9.8f;
    [SerializeField] private LayerMask groundMask = 0;
    [Header("Punch")]
    [SerializeField] public Collider punchCollider = null;
    [SerializeField] public Transform shellTarget = null;
    [Header("Wall Jump")]
    [SerializeField] private Transform wallChecker = null;
    [SerializeField] private Vector3 wallCheckerExtends = Vector3.one;

    /** Hide Atributes **/
    [HideInInspector] private StateMachine myStateMachine = new StateMachine();
    [HideInInspector] private CharacterController characterController;

    [HideInInspector] private CollisionFlags collisionFlags;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public float verticalSpeed;
    [HideInInspector] private Transform camTransform;
    [HideInInspector] public Vector3 lastForward;
    [HideInInspector] public float speed;

    [HideInInspector] private int punch = 0;
    [HideInInspector] private float lastPunchTime;

    [HideInInspector] private int jump = 0;
    [HideInInspector] private float lastJumpTime;
    [HideInInspector] public bool recentJump;
    [HideInInspector] private bool needsJump = false;

    [HideInInspector] private Transform platform;

    [HideInInspector] public bool punchIsActive = false;
    [HideInInspector] public Shell shell = null;

    [HideInInspector] public bool needsLongJump;
    [HideInInspector] public bool needsWallJump;
    [HideInInspector] public Vector3 wallForward = Vector3.zero;
    [HideInInspector] public bool canFall = false;

    /** Initialization **/
    private void Start()
    {
        GameManager.instance.player = this;

        characterController = GetComponent<CharacterController>();
        myStateMachine.ChangeState(new IdleState(anim, this));

        camTransform = Camera.main.transform;

    }

    /** Update **/

    private void Update()
    {
        if (lifeController.currentLifes <= 0)
        {
            myStateMachine.ChangeState(new DeathState(anim));
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        else
            Cursor.lockState = CursorLockMode.Locked;


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
        if (shell == null && Input.GetButtonDown("Fire1"))
        {
            myStateMachine.ChangeState(new PunchState(anim, GetNextPunch()));
            return;
        }

        if(shell != null && Input.GetButtonDown("Fire1"))
        {
            myStateMachine.ChangeState(new ThrowState(anim, this));
            return;
        }

        if(CanWallJump() && (canFall || !isGrounded) && Input.GetButtonDown("Jump"))
        {
            myStateMachine.ChangeState(new WallJumpState(anim, this));
            return;
        }

        if (needsJump)
        {
            myStateMachine.ChangeState(new JumpState(anim, this, 0));

            needsJump = false;

            return;
        }

        if(!canFall && Input.GetButtonDown("Jump") && Input.GetButton("LongJump"))
        {
            myStateMachine.ChangeState(new LongJumpState(anim, this));
            return;
        }

        if((!canFall && Input.GetButtonDown("Jump")))
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

        if (canFall){
            myStateMachine.ChangeState(new FallState(anim, this));
            return;
        }

        //if(Physics.CheckSphere(transform.position + (Vector3.up * .75f), 1, groundMask))


        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if(Input.GetButton("Run"))
                myStateMachine.ChangeState(new RunState(anim, this));
            else
                myStateMachine.ChangeState(new WalkState(anim, this));

            return;
        }

        myStateMachine.ChangeState(new IdleState(anim, this));
    }

    private void ResetAnimPosition()
    {
        anim.transform.localPosition = Vector3.zero;
    }

    /** Punchs **/

    public void EnableLeftHandPunch(bool enableHandPunch)
    {
        punchIsActive = enableHandPunch;
    }

    public void EnableRightHandPunch(bool enableHandPunch)
    {
        punchIsActive = enableHandPunch;
    }

    public void EnableKick(bool enableHandPunch)
    {
        punchIsActive = enableHandPunch;
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
        if (!needsLongJump && !needsWallJump)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
                movement = forward;
            else if (Input.GetAxisRaw("Vertical") < 0)
                movement = -forward;

            if (Input.GetAxisRaw("Horizontal") > 0)
                movement += right;
            else if (Input.GetAxisRaw("Horizontal") < 0)
                movement += -right;
        }
        else if (needsLongJump && !needsWallJump)
            movement = lastForward;
        else
        {
            movement = wallForward;
        }

        bool hasMovement = movement != Vector3.zero;
        movement.Normalize();

        movement *= speed * Time.deltaTime;

        movement *= (needsLongJump) ? 2 : 1;

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

        canFall = !Physics.CheckSphere(transform.position, .4f, groundMask);

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
            Rigidbody rb = hit.collider.attachedRigidbody;
            rb.AddForceAtPosition(-hit.normal * bridgeForce, hit.point);
        }

        if(hit.gameObject.GetComponent<AEnemy>() && collisionFlags == CollisionFlags.Below)
        {
            if (verticalSpeed < 0)
            {
                hit.gameObject.GetComponent<AEnemy>().Die();
                needsJump = true;
            }
        }

        if (hit.gameObject.GetComponent<Shell>() && verticalSpeed < 0)
        {
            hit.gameObject.GetComponent<Shell>().Throw();
            needsJump = true;
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Platform" && platform == null)
        {
            AttachPlatform(other.transform);
        }

        if (other.GetComponent<AEnemy>())
        {
            Debug.Log("Y");
        }

        if(other.GetComponent<AEnemy>() && punchIsActive)
        {
            Debug.Log("YOU");
            other.GetComponent<AEnemy>().Die();
        }

        if(other.GetComponent<Shell>() && Input.GetButtonDown("Interact"))
        {
            TakeShell(other.GetComponent<Shell>());
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
        if(myStateMachine.currentState.GetType() != typeof(DeathState))
            myStateMachine.ChangeState(new HitState(anim, lifeController));
    }

    private void DoIdlePlus()
    {
        anim.SetTrigger("IdlePlus");
        Invoke("DoIdlePlus", 10);
    }

    private void TakeShell(Shell shell)
    {
        this.shell = shell;
        myStateMachine.ChangeState(new TakeState(anim));
        this.shell.Take(shellTarget);
    }

    public bool CanWallJump()
    {
        return Physics.CheckBox(wallChecker.position, wallCheckerExtends/2, Quaternion.identity, groundMask);
    }

    private void OnDrawGizmos()
    {
        if(wallChecker != null)
        {
            Gizmos.DrawWireCube(wallChecker.position, wallCheckerExtends);
        }
    }

    private void ResetWallJump()
    {
        needsWallJump = false;
        wallForward = Vector3.zero;
    }

    public void Revive(Transform position)
    {
        characterController.enabled = false;
        transform.position = position.position;
        characterController.enabled = true;

        transform.forward = position.forward;

        myStateMachine.ChangeState(new IdleState(anim, this));

        lifeController.Revive();

    }

}
