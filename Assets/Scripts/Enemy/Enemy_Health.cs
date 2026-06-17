using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    override public bool TakeDamage(float damage, Transform damageDealer)
    {
        if(canTakeDamage == false) return false;

        bool wasHit = base.TakeDamage(damage, damageDealer);

        if (wasHit == false) return false;

        if (damageDealer.GetComponent<Player>() != null)
            enemy.TryEnterBattleState(damageDealer);

        return true;
    }

}
