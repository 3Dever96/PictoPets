using UnityEngine;

[System.Serializable]
public class PlayerAirState : PlayerState
{
    [SerializeField] float gravity;
    [SerializeField] float fallSpeed;

    public override void StartState(PlayerController player)
    {

    }

    public override void UpdateState(PlayerController player)
    {
        if (!player.Input.Jump || Physics.CheckSphere(player.transform.position + Vector3.up * player.Controller.height, player.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
        {
            player.VerticalSpeed = Mathf.Min(0f, player.VerticalSpeed);
        }

        if (player.VerticalSpeed > fallSpeed)
        {
            player.VerticalSpeed += gravity * Time.deltaTime;
        }

        player.FaceDirection(player.Direction, 500f);
        player.ApplyMovement();
    }

    public override void ChangeState(PlayerController player)
    {
        if (player.VerticalSpeed <= 0f && Physics.CheckSphere(player.transform.position, player.Controller.radius - 0.01f, LayerMask.GetMask("Solid")))
        {
            player.SetState(player.GroundState);
        }
    }

    public override void ExitState(PlayerController player)
    {

    }
}
