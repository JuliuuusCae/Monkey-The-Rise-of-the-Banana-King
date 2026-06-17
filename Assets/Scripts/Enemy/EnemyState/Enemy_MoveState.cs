using UnityEngine;

public class Enemy_MoveState : Enemy_GroundedState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();

        if(enemy.groundDetection == false || enemy.wallDetection)
            enemy.Flip();
    }

    override public void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.linearVelocity.y);

        if(enemy.groundDetection == false || enemy.wallDetection)
            stateMachine.ChangeState(enemy.idleState);
    }
            
}
