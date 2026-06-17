using UnityEngine;

public class Enemy_Snail : Enemy, ICounterable
{
    public bool CanBeCounterable { get => canBeStunned; }

    public Enemy_HideState hideState;

    [Header("Hide details")]
    public float hideDuration = 3f;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        hideState = new Enemy_HideState(this, stateMachine, "hide", hideDuration);
        battleState = hideState;
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
    }

    override protected void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public void HandleCounter()
    {
        if (CanBeCounterable == false) return;

        stateMachine.ChangeState(stunnedState);
    }
}
