using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    override public void Update()
    {
        base.Update();
        HandleWallSlide();

        if(input.Player.Jump.triggered)
        {
            stateMachine.ChangeState(player.wallJumpState);
        }

        if (player.wallDetection == false)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (player.groundDetection)
        {
            stateMachine.ChangeState(player.idleState);

            if(player.facingDir != player.moveInput.x)
            player.Flip();
        }
    }

    private void HandleWallSlide()
    {
        if (player.moveInput.y < 0)
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y);
        }
        else
        {
            player.SetVelocity(player.moveInput.x, rb.linearVelocity.y * player.wallSlideSlowMultiplier);
        }
    }   
}
