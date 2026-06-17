using UnityEngine;

[CreateAssetMenu(menuName = "RPG Setup/Item Data/Item Effect/Key Effect", fileName = "Item Effect Data - Key ")]
public class ItemEffect_Keys : ItemEffect_DataSO
{
    public override bool CanBeUsed(Player player) => false;
}
