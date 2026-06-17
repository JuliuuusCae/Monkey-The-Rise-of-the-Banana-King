using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Gold Effect", fileName = "Item Effect Data - Gold")]
public class ItemEffect_Gold : ItemEffect_DataSO
{
    [SerializeField] private int goldAmount = 1;

    public override void ExecuteEffect()
    {
        Player player = FindFirstObjectByType<Player>();
        Inventory_Player inventory = player.GetComponent<Inventory_Player>();

        if (inventory != null)
            inventory.gold += goldAmount;
    }
}
