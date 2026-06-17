using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTriggers
{
    private Enemy enemy;
    private Enemy_VFX enemyVFX;

    override protected void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVFX = GetComponentInParent<Enemy_VFX>();
    }

    public void EnableCounterWindow()
    {
        enemyVFX.EnableAttackAlert(true);
        enemy.EnableCounterWindow(true);
    }

    public void DisableCounterWindow()
    {
        enemyVFX.EnableAttackAlert(false);
        enemy.EnableCounterWindow(false);
    }
} 
