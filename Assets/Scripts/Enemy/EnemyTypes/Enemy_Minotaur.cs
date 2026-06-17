using UnityEngine;

public class Enemy_Minotaur : Enemy, ISaveable
{
    [Header("Save")]
    [SerializeField] private string enemyId;

    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        deadState = new Enemy_DeadState(this, stateMachine, "idle");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    public override void EntityDeath()
    {
        base.EntityDeath();
        SaveManager.instance.SaveGame();
    }

    public void SaveData(ref GameData data)
    {
        if (string.IsNullOrEmpty(enemyId)) return;

        data.killedEnemies[enemyId] = health.IsDead();
    }

    public void LoadData(GameData data)
    {
        if (string.IsNullOrEmpty(enemyId)) return;

        if (data.killedEnemies.TryGetValue(enemyId, out bool isKilled) && isKilled)
            gameObject.SetActive(false);
    }
}
