using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class WildRoamState : WildState
{
    [SerializeField] float moveSpeed;
    [SerializeField] float roamRadius;
    [SerializeField] float roamTime;

    float currentTime;

    bool isMoving;

    NavMeshPath path;

    Vector3 destination;

    public override void StartState(WildPhotomorphController wild)
    {
        isMoving = false;

        path = new NavMeshPath();
    }

    public override void UpdateState(WildPhotomorphController wild)
    {
        if (!isMoving)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0f)
            {
                float x = Random.Range(-roamRadius, roamRadius);
                float z = Random.Range(-roamRadius, roamRadius);

                destination = wild.SpawnPoint.position + new Vector3(x, 0f, z);

                wild.Agent.CalculatePath(destination, path);

                if (wild.Agent.SetPath(path))
                {
                    isMoving = true;
                }
            }
        }
        else
        {
            currentTime = roamTime;

            Vector3 direction = wild.Agent.desiredVelocity.normalized;
            direction.y = 0f;

            Vector3 velocity = direction * moveSpeed;
            velocity.y = -5f;

            wild.FaceDirection(direction, 500f);

            wild.ApplyMovement(velocity);

            if (Vector3.Distance(destination, wild.transform.position) < 1f)
            {
                isMoving = false;
            }
        }
    }

    public override void ChangeState(WildPhotomorphController wild)
    {
        
    }

    public override void ExitState(WildPhotomorphController wild)
    {
        
    }
}
