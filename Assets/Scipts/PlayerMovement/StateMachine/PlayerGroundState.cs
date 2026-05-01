using System.Net.NetworkInformation;
using UnityEngine;

[System.Serializable]
public class PlayerGroundState : PlayerState
{
    [SerializeField] float maxSpeed; // Max running speed
    [SerializeField] float accel; // Rate of acceleration
    [SerializeField] float decel; // Rate of deceleration
    [SerializeField] float fric; // Amount of friction to be applied if there's no input
    [SerializeField] float turnSpeed; // Speed to change direction
    [SerializeField] float turnAngle; // Maximum angle the player can turn without losing speed.
    [SerializeField] float stickForce; // Downward force to keep player on the ground.
    [SerializeField] float jumpSpeed;

    float moveSpeed; // The current target speed the player is to be moving at

    bool canJump; // Control check to determine if the player can jump

    public override void StartState(PlayerController player)
    {
        // Set jump check to false
        // Upon entering the ground state, the player shouldn't be able to just jump right away.
        // This prevents the player from simply holding the jump button and jumping continuously.
        canJump = false;

        // Set the player's vertical speed to the stick force.
        // As long as the player is in the ground state, their vertical speed won't change.
        // This will keep the player grounded at all times.
        player.VerticalSpeed = stickForce;

        // Set the player's direction property to the direction the player is currently facing.
        // This prevents a look vector is 0 zero.
        player.Direction = player.transform.forward;
    }

    public override void UpdateState(PlayerController player)
    {
        // Get the direction of input in 3D space
        // Remove any vertical value from the input direction.
        // Normalize the input
        Vector3 direction = Camera.main.transform.right * player.Input.Move.x + Camera.main.transform.forward * player.Input.Move.y;
        direction.y = 0f;
        direction = direction.normalized; 

        // Calculate current speed
        // First check if the player is pressing move input
        if (player.Input.Move != Vector2.zero)
        {
            // Compare the input direction to the direction the player is facing
            // If the angle is greater than turn angle, apply deceleration
            // Otherwise, apply acceleration
            if (Vector3.Angle(direction, player.Direction) > turnAngle)
            {
                // Check if the player has any speed
                if (player.CurrentSpeed > 0f)
                {
                    // Reduce the player's speed until it hits zero.
                    player.CurrentSpeed -= decel * Time.deltaTime;
                }
                else
                {
                    // Make sure the player's speed doesn't fall below zero.
                    // Otherwise, the player could wind up moving backwards.
                    player.CurrentSpeed = 0f;

                    // Set the player's direction to the input direction.
                    // This will make the angle check false, which means the player can now accelerate
                    player.Direction = direction;
                }
            }
            else
            {
                // Check if the player's speed is less than the move speed
                if (player.CurrentSpeed < moveSpeed)
                {
                    // Increase the player's speed until it hits the move speed value
                    player.CurrentSpeed += accel * Time.deltaTime;
                }
                else
                {
                    // Cap the player's speed at the move speed
                    player.CurrentSpeed = moveSpeed;
                }

                // Set the player's direction to the input direction.
                // This will keep the player moving in the desired direction unless the angle between the player's direction and input direction is greater than turn angle
                player.Direction = direction;
            }
        }
        else
        {
            // If there's no input, the player's speed must be affected by friction to come to a stop.
            // If the player's speed is less than the friction, it subtracts the value of the speed to reach zero.
            player.CurrentSpeed -= Mathf.Min(player.CurrentSpeed, fric * Time.deltaTime);
        }

        // Set target move speed
        // Multiply the max speed by the move input magnitude to get a float value between 0 and max speed.
        // This is done after accel/ decel/ fric calculations to avoid bugs where the player releases all inputs and immediately stops
        moveSpeed = maxSpeed * player.Input.Move.magnitude;

        if (player.Input.Jump && canJump)
        {
            player.VerticalSpeed = jumpSpeed;
        }

        if (!player.Input.Jump && !canJump)
        {
            canJump = true;
        }

        // Make the player face the desired direction
        // Because of the way we're handling the movement calculations, we want the player's direction to be the desired direction.
        player.FaceDirection(player.Direction, turnSpeed);

        player.ApplyMovement();
    }

    public override void ChangeState(PlayerController player)
    {
        if (player.VerticalSpeed > 0f || !Physics.CheckSphere(player.transform.position, player.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
        {
            player.SetState(player.AirState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        
    }
}
