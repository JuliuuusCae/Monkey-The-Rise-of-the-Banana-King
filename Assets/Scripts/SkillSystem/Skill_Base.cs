using UnityEngine;

public class Skill_Base : MonoBehaviour
{
    public Player_SkillManager skillManager { get; private set; }
    public Player player { get ; private set; }

    [Header("General Details")]
    [SerializeField] protected SkillType skillType;
    [SerializeField] protected SkillUpgradeType skillUpgradeType;
    [SerializeField] private float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        skillManager = GetComponentInParent<Player_SkillManager>();
        player = GetComponentInParent<Player>();
        lastTimeUsed = -cooldown; // So the skill is available at the start of the game
    }

    public virtual void TryUseSkill()
    {

    }

    public void SetSkillUpgrade(Skill_DataSO skillData)
    {
        UpgradeData upgrade = skillData.upgradeData;
        skillUpgradeType = upgrade.upgradeType;
        cooldown = upgrade.cooldown;

        player.ui.inGameUI.GetSkillSlot(skillType)?.SetupSkillSlot(skillData);
        ResetCooldown();
    }

    public void ResetSkillUpgrade()
    {
        if (skillType == SkillType.Dash)
        {
            skillUpgradeType = SkillUpgradeType.Dash;
            return;
        }

        skillUpgradeType = SkillUpgradeType.None;
    }

    public bool CanUseSkill()
    {
        if(skillUpgradeType == SkillUpgradeType.None)
            return false;

        if (OnCooldown())
            return false;

        return true;
    }

    protected bool Unlocked(SkillUpgradeType upgradeToCheck) => skillUpgradeType == upgradeToCheck;
    public SkillUpgradeType GetUpgrade() => skillUpgradeType;
    public SkillType GetSkillType() => skillType;

    private bool OnCooldown() => Time.time < lastTimeUsed + cooldown;
    public void SetSkillOnCooldown()
    {
        player.ui.inGameUI.GetSkillSlot(skillType).StartCooldown(cooldown);
        lastTimeUsed = Time.time;
    }
    public void ReduceCooldownBy(float cooldownReduction) => lastTimeUsed += cooldownReduction;
    public void ResetCooldown()
    {
        player.ui.inGameUI.GetSkillSlot(skillType).ResetCooldown();
        lastTimeUsed = Time.time - cooldown;
    }

}
