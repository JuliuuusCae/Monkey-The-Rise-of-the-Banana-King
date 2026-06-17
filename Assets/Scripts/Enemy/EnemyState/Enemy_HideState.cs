using UnityEngine;

public class Enemy_HideState : Enemy_BattleState
{
    private float hideDuration;

    public Enemy_HideState(Enemy enemy, StateMachine stateMachine, string animBoolName, float hideDuration)
        : base(enemy, stateMachine, animBoolName)
    {
        this.hideDuration = hideDuration;
    }

    public override void Enter()
    {
        base.Enter();
        rb.linearVelocity = Vector2.zero;
        stateTimer = hideDuration;

        if (enemy.player != null)
        {
            int dirToPlayer = enemy.player.position.x > enemy.transform.position.x ? 1 : -1;
            enemy.HandleFlip(dirToPlayer);
        }
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        UpdateAnimationParameters();
        rb.linearVelocity = Vector2.zero;

        if (stateTimer <= 0 && !enemy.PlayerDetection())
            stateMachine.ChangeState(enemy.idleState);
    }
}
