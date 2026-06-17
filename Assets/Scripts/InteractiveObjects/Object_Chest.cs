using UnityEngine;

public class Object_Chest : MonoBehaviour, IDamageable, ISaveable
{
    private Rigidbody2D rb => GetComponentInChildren<Rigidbody2D>();
    private Animator anim => GetComponentInChildren<Animator>();
    private Entity_VFX fx => GetComponent<Entity_VFX>();
    private Entity_DropManager dropManager => GetComponent<Entity_DropManager>();

    [Header("Open Details")]
    [SerializeField] private Vector2 knockback;
    [SerializeField] private bool canDropItems = true;

    [Header("Save")]
    [SerializeField] private string chestId;

    public bool TakeDamage(float damage, Transform damageDealer)
    {
        if (canDropItems == false)
            return false;

        canDropItems = false;
        dropManager?.DropItems();
        fx.PlayOnDamageVfx();
        anim.SetBool("chestOpen", true);
        rb.linearVelocity = knockback;
        rb.angularVelocity = Random.Range(-200, 200);

        return true;
    }

    public void SaveData(ref GameData data)
    {
        if (string.IsNullOrEmpty(chestId)) return;

        data.openedChests[chestId] = !canDropItems;
    }

    public void LoadData(GameData data)
    {
        if (string.IsNullOrEmpty(chestId)) return;

        if (data.openedChests.TryGetValue(chestId, out bool isOpened) && isOpened)
        {
            canDropItems = false;
            anim.SetBool("chestOpen", true);
        }
    }
}
