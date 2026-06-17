using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter AttackDetails")]
    [SerializeField] private float counterRecovery = 0.1f;

    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        foreach(var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null) continue; // Skip this target, go to next one

            if (counterable.CanBeCounterable)
            {
                counterable.HandleCounter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;

}
