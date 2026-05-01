using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Properties
    // The character controller is used for applying the movement calculations
    public CharacterController Controller { get; private set; }
    
    // Input manager is used to read the inputs
    public InputManager Input { get; private set; }

    // Direction is the current direction the player is looking
    public Vector3 Direction { get; set; }

    // Velocity is stored movement data to be transfered between the different states
    public Vector3 Velocity { get; set; }

    // Current Speed is the current horizontal speed value
    public float CurrentSpeed { get; set; }

    // Vertical Speed is the current vertical speed value
    public float VerticalSpeed { get; set; }

    // Current State is the currently active state of the state machine
    public PlayerState CurrentState { get; private set; }

    // The ground state of the state machine
    public PlayerGroundState GroundState { get { return groundState; } }
    // The air state of the state machine
    public PlayerAirState AirState { get { return airState; } }

    // Variables
    // These are the state fields of the state machine.  By making them serializeable, their local fields can be modified in the inspector.
    [SerializeField] PlayerGroundState groundState = new PlayerGroundState();
    [SerializeField] PlayerAirState airState = new PlayerAirState();

    private void Start()
    {
        // Assign the references to the controller and input
        Controller = GetComponent<CharacterController>();
        Input = GetComponentInParent<InputManager>();

        // Set the default state
        SetState(groundState);
    }

    private void Update()
    {
        // Ensure the current state exists and run its update and change state functions.
        if (CurrentState != null)
        {
            CurrentState.UpdateState(this);
            CurrentState.ChangeState(this);
        }
    }

    // This function is used to change the state of the state machine
    public void SetState(PlayerState newState)
    {
        // Ensure the current state exists and run the exit state funtion
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }

        // Set the current state to the new desired state
        CurrentState = newState;

        // Ensure the current state exists and run the start state function
        if (CurrentState != null)
        {
            CurrentState.StartState(this);
        }
    }

    // Rotates the player to face the desired direction
    public void FaceDirection(Vector3 direction, float turnSpeed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
    }

    public void ApplyMovement()
    {
        Vector3 velocity = Direction * CurrentSpeed;
        velocity.y = VerticalSpeed;

        Velocity = velocity;

        Controller.Move(Velocity * Time.deltaTime);
    }
}
