using System.Text;
using TMPro;
using UnityEngine;

public class UI_ItemToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemInfo;

    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemActions;
    [SerializeField] private Transform inventoryInfo;

    public void ShowToolTip(bool show, RectTransform targetRect, Inventory_Item itemToShow, bool buyPrice = false, bool showActions = false, string actionText = "")
    {
        base.ShowToolTip(show, targetRect);

        inventoryInfo.gameObject.SetActive(!showActions);

        int price = buyPrice ? itemToShow.buyPrice : Mathf.FloorToInt(itemToShow.sellPrice);
        int totalPrice = price * itemToShow.stackSize;

        string fullStackPrice = ($"Preço:{price}g.");
        string singleStackPrice = ($"Preço:{price}g.");

        itemPrice.text = itemToShow.stackSize > 1 ? fullStackPrice : singleStackPrice;
        if (showActions)
            itemActions.text = actionText;
        itemActions.gameObject.SetActive(showActions);
        itemType.text = GetItemNameByType(itemToShow.itemData.itemType);
        itemInfo.text = GetItemInfo(itemToShow);

        string color = GetColorByRarity(itemToShow.itemData.itemRarityType);
        itemName.text = GetColoredText(color, itemToShow.itemData.itemName);
    }

    public string GetItemInfo(Inventory_Item item)
    {

        if (item.itemData.itemType == ItemType.Material)
            return "Usado para trocar com mercadores.";

        if (item.itemData.itemType == ItemType.Consumable)
            return item.itemData.itemEffect.effectDescription;

        StringBuilder sb = new StringBuilder();

        sb.AppendLine("");

        foreach (var mod in item.modifiers)
        {
            string modType = GetStatNameByType(mod.statType);
            string modValue = IsPercentageStat(mod.statType) ? mod.value.ToString() + "%" : mod.value.ToString();
            sb.AppendLine("+ " + modValue + " " + modType);
        }

        if (item.itemEffect != null)
        {
            sb.AppendLine("");
            sb.AppendLine("Efeito único:");
            sb.AppendLine(item.itemEffect.effectDescription);
        }

        return sb.ToString();
    }

    private string GetStatNameByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return "Vida Máxima";
            case StatType.HealthRegen: return "Regeneração de Vida";
            case StatType.Armor: return "Armadura";
            case StatType.Evasion: return "Evasão";

            case StatType.Strength: return "Força";
            case StatType.Agility: return "Agilidade";
            case StatType.Intelligence: return "Inteligência";
            case StatType.Vitality: return "Vitalidade";

            case StatType.AttackSpeed: return "Velocidade de Ataque";
            case StatType.Damage: return "Dano";
            case StatType.CritChance: return "Chance de Crítico";
            case StatType.CritPower: return "Dano Crítico";
            case StatType.ArmorReduction: return "Redução de Armadura";
            default: return "Atributo Desconhecido";
        }
    }

    private bool IsPercentageStat(StatType type)
    {
        switch (type)
        {
            case StatType.CritChance:
            case StatType.CritPower:
            case StatType.ArmorReduction:
            case StatType.AttackSpeed:
            case StatType.Evasion:
                return true;
            default:
                return false;
        }
    }

    private string GetItemNameByType(ItemType type)
    {
        switch (type)
        {
            case ItemType.Material: return "Material";
            case ItemType.Weapon: return "Arma";
            case ItemType.Armor: return "Armadura";
            case ItemType.Trinket: return "Acessório";
            case ItemType.Consumable: return "Consumível";
            default: return "Desconhecido";
        }
    }

    private string GetColorByRarity(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return "white";
            case ItemRarity.Uncommon: return "green";
            case ItemRarity.Rare: return "blue";
            case ItemRarity.Epic: return "purple";
            case ItemRarity.Legendary: return "orange";
            default: return "white";
        }
    }
}