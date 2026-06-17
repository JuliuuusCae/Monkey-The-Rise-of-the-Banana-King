using UnityEngine;

public class Enemy_AttackState : EnemyState
{
    public Enemy_AttackState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    override public void Enter()
    {
        base.Enter();
        SyncAttackSpeed();
    }

    override public void Update()
    {
        base.Update();

        if(triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }
    }

        
}
