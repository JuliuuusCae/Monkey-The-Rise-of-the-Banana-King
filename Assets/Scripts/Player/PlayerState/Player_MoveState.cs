using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();
        
        if (player.moveInput.x == 0 || (player.moveInput.x == player.facingDir && player.wallDetection))
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            player.SetVelocity(player.moveInput.x * player.moveSpeed, rb.linearVelocity.y);
        }
    }
}
