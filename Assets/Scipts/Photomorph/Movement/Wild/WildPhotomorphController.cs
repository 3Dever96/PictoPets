using UnityEngine;
using UnityEngine.AI;

public class WildPhotomorphController : MonoBehaviour
{ 
    public WildState CurrentState { get; private set; }
    public WildRoamState RoamState { get { return roamState; } }

    public NavMeshAgent Agent { get; private set; }
    public CharacterController Controller { get; private set; }

    public Transform Target { get; private set; }

    public Transform SpawnPoint { get; private set; }
    public PhotomorphAvatar Avatar { get; private set; }

    public float ChaseDistance { get { return chaseDistance; } }

    [SerializeField] float chaseDistance;

    [SerializeField] WildRoamState roamState;

    private void Start()
    {
        Agent.updatePosition = false;
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState(this);
            CurrentState.ChangeState(this);
        }
    }

    public void SpawnIn(Transform newSpawnPoint, PhotomorphEntity entity)
    {
        SpawnPoint = newSpawnPoint;
        Avatar.Initialize(entity);

        Controller.enabled = false;
        transform.position = SpawnPoint.position;
        Controller.enabled = true;
    }

    public void OnEnable()
    {
        if (Agent == null)
        {
            Agent = GetComponent<NavMeshAgent>();
            Agent.updatePosition = false;
            Agent.updateRotation = false;
        }

        if (Controller == null)
        {
            Controller = GetComponent<CharacterController>();
        }

        if (Avatar == null)
        {
            Avatar = GetComponentInChildren<PhotomorphAvatar>();
        }

        Target = FindAnyObjectByType<PlayerController>().transform;

        SetState(roamState);
    }

    public void SetState(WildState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState(this);
        }

        CurrentState = newState;

        if (CurrentState != null)
        {
            CurrentState.StartState(this);
        }
    }

    public void ApplyMovement(Vector3 velocity)
    {
        Controller.Move(velocity * Time.deltaTime);
        Agent.nextPosition = transform.position;
    }

    public void FaceDirection(Vector3 forward, float speed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(forward), speed * Time.deltaTime);
    }
}
