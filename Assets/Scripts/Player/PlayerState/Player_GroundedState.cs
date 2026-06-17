using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {

    }

    public override void Update()
    {
        base.Update();

        if(rb.linearVelocity.y < 0 && player.groundDetection == false)
            stateMachine.ChangeState(player.fallState);
        
        if (input.Player.Jump.triggered)
            stateMachine.ChangeState(player.jumpState);

        if (input.Player.Attack.triggered)
            stateMachine.ChangeState(player.basicAttackState);  

        if(input.Player.CounterAttack.triggered)
            stateMachine.ChangeState(player.counterAttackState);
    }
}
