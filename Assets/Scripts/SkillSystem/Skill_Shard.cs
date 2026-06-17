using UnityEngine;

public class Skill_Shard : Skill_Base
{
    private SkillObject_Shard currentShard;

    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private float detonationTime = 2f;

    [Header("Moving Shard Upgrades")]
    [SerializeField] private float shardSpeed = 7f;
    [SerializeField] private float moveDelay = .5f; // Delay before the shard starts moving towards the enemy

    override public void TryUseSkill()
    {
        if(CanUseSkill() == false)
            return;

        if (Unlocked(SkillUpgradeType.Shard))
            HandleShardRegular();

        if (Unlocked(SkillUpgradeType.Shard_MoveToEnemy))
            HandleShardMoving();
    }

    private void HandleShardMoving()
    {
        CreateShard();
        currentShard.MoveTowardsClosestTarget(shardSpeed, moveDelay);

        SetSkillOnCooldown();
    }

    private void HandleShardRegular()
    {
        CreateShard();
        SetSkillOnCooldown();
    }

    public void CreateShard()
    {
        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        currentShard = shard.GetComponent<SkillObject_Shard>();
        currentShard.SetupShard(detonationTime);
    }

    public void CreateRawShard()
    {
        bool canMove = Unlocked(SkillUpgradeType.Shard_MoveToEnemy);

        GameObject shard = Instantiate(shardPrefab, transform.position, Quaternion.identity);
        SkillObject_Shard shardObj = shard.GetComponent<SkillObject_Shard>();
        shardObj.SetupShard(detonationTime);

        if (canMove)
            shardObj.MoveTowardsClosestTarget(shardSpeed, moveDelay);
    }
}
