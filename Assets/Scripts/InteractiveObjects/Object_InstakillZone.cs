using UnityEngine;

public class Object_InstakillZone : MonoBehaviour
{
    [SerializeField] private Transform safeDropPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity_Health health = collision.GetComponent<Entity_Health>();
        if (health == null) return;

        if (safeDropPoint != null)
        {
            Entity_DropManager dropManager = collision.GetComponent<Entity_DropManager>();
            if (dropManager != null)
                dropManager.dropPositionOverride = safeDropPoint.position;
        }

        health.TakeDamage(float.MaxValue, transform);

        Entity_DropManager dm = collision.GetComponent<Entity_DropManager>();
        if (dm != null) dm.dropPositionOverride = null;
    }
}
