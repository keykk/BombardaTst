using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 10f; // Velocidade de movimento do jogador
    public float jumpPower = 10f; // Força do pulo
    public float jumpMovementFactor = 0.35f;
    [HideInInspector] public StateMachine stateMachine;

    [HideInInspector] public Idle idleState;

    [HideInInspector] public Walking walkingState;
    [HideInInspector] public Jump jumpState;
    [HideInInspector] public Dead deadState;

    [HideInInspector] public Vector2 movementVector;
    [HideInInspector] public bool hasJumpInput;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Rigidbody thisRigidbody;
    [HideInInspector] public Collider thisCollider;
    [HideInInspector] public Animator thisAnimator;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
        thisCollider = GetComponent<Collider>();
        thisAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        stateMachine = new StateMachine();
        idleState = new Idle(this);
        walkingState = new Walking(this);
        jumpState = new Jump(this);
        deadState = new Dead(this);
        stateMachine.ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        //Check GameOver
        if (GameManager.Instance.isGameOver)
        {
            if (stateMachine.currentStateName != deadState.name)
            {
                stateMachine.ChangeState(deadState);
            }
        }
        bool isUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool isDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool isLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool isRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        float inputX = isRight ? 1 : isLeft ? -1 : 0;
        float inputY = isUp ? 1 : isDown ? -1 : 0;
        movementVector = new Vector2(inputX, inputY);
        hasJumpInput = Input.GetKeyDown(KeyCode.Space);

        float velocity = thisRigidbody.linearVelocity.magnitude;
        float velocityRate = velocity / movementSpeed;

        thisAnimator.SetFloat("fVelocity", velocityRate);
        

        DetectGround();

        stateMachine.Update();
    }

    void LateUpdate()
    {
        stateMachine.LateUpdate();
    }
    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public Quaternion GetForward()
    {
        Camera camera = Camera.main;
        float eulerY = camera.transform.eulerAngles.y;
        return Quaternion.Euler(0, eulerY, 0);
    }

    public void RotateBodyToFaceInput()
    {
        if(movementVector.IsZero())
        {
            return;
        }
        Camera camera = Camera.main;
        Vector3 inputVector = new Vector3(movementVector.x, 0, movementVector.y);
        Quaternion q1 = Quaternion.LookRotation(inputVector, Vector3.up);
        quaternion q2 = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);

        Quaternion toRotation = q1 * q2;
        Quaternion newRotation = Quaternion.LerpUnclamped(transform.rotation, toRotation, 0.5f);

        thisRigidbody.MoveRotation(newRotation);
    }

    private void DetectGround()
    {
        isGrounded = false;
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.25f;
        if (Physics.SphereCast(origin, radius, direction, out var hitInfo, maxDistance))
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                isGrounded = true;

            }
        }
    }
    
    /*void OnDrawGizmos()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;
        Bounds bounds = thisCollider.bounds;
        float radius = bounds.size.x * 0.33f;
        float maxDistance = bounds.size.y * 0.25f;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(origin, direction * maxDistance));

        Gizmos.color = Color.gray;
        Gizmos.DrawSphere(origin, radius * 0.1f);

        Vector3 spherePosition = direction * maxDistance + origin;
        Gizmos.color = isGrounded ? Color.magenta : Color.cyan;
        Gizmos.DrawSphere(spherePosition, radius);
    }*/
}
