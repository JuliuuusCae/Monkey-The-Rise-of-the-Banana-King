using UnityEngine;

public enum SkillUpgradeType
{
    None,

    Dash,
    Dash_CloneOnStart,
    Dash_CloneOnStartAndArrival,
    Dash_ShardOnStart,
    Dash_ShardOnStartAndArrival,

    Shard,
    Shard_MoveToEnemy,
    Shard_TripleCast,
    Shard_Teleport,
    Shard_TeleportAndHeal,

    TimeEcho,
    TimeEcho_SingleAttack,
    TimeEcho_MultiAttack,
    TimeEcho_ChanceToDuplicate,

    TimeEcho_HealWisp,

    TimeEcho_CleanseWisp,
    TimeEcho_CooldownWisp 
}

