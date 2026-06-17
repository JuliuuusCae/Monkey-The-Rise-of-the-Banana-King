using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private Player player;
    private Inventory_Player inventory;
    private UI_SkillSlot[] skillSlots;

    [SerializeField] private RectTransform healthRect;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Quick Item Slots")]
    [SerializeField] private Transform quickItemOptionsParent;
    private UI_QuickItemSlotOption[] quickItemOptions;
    private UI_QuickItemSlot[] quickItemSlots;

    private void Start()
    {
        quickItemSlots = GetComponentsInChildren<UI_QuickItemSlot>();

        player = FindFirstObjectByType<Player>();
        player.health.OnHealthUpdate += UpdateHealthBar;

        inventory = player.inventory;
        inventory.OnInventoryChange += UpdateQuickSlotsUI;
        inventory.OnQuickSlotUsed += PlayQuickSlotFeedback;

        quickItemOptionsParent.gameObject.SetActive(false);
        UpdateQuickSlotsUI();
    }

    public void PlayQuickSlotFeedback(int slotNumber, Inventory_Item item)
    {
        quickItemSlots[slotNumber].UpdateQuickSlotUI(item);
        quickItemSlots[slotNumber].SimulateButtonFeedback();
    }

    public void UpdateQuickSlotsUI()
    {
        Inventory_Item[] quickItems = inventory.quickItems;

        int count = Mathf.Min(quickItems.Length, quickItemSlots.Length);
        for (int i = 0; i < count; i++)
            quickItemSlots[i].UpdateQuickSlotUI(quickItems[i]);
    }

    public void OpenQuickItemOptions(UI_QuickItemSlot quickItemSlot, RectTransform targetRect)
    {
        if(quickItemOptions == null)
            quickItemOptions = quickItemOptionsParent.GetComponentsInChildren<UI_QuickItemSlotOption>(true);

        List<Inventory_Item> consumables = inventory.itemList.FindAll(item => item.itemData.itemType == ItemType.Consumable);

        if (consumables.Count == 0)
            return;

        for (int i = 0; i < quickItemOptions.Length; i++)
        {
            if (i < consumables.Count)
            {
                quickItemOptions[i].gameObject.SetActive(true);
                quickItemOptions[i].SetupOption(quickItemSlot, consumables[i]);
            }
            else
                quickItemOptions[i].gameObject.SetActive(false);
        }

        Vector3 fixedPos = quickItemOptionsParent.position;
        quickItemOptionsParent.position = new Vector3(targetRect.position.x, fixedPos.y, fixedPos.z);
        quickItemOptionsParent.gameObject.SetActive(true);
    }

    public void HideQuickItemOptions() => quickItemOptionsParent.gameObject.SetActive(false);

    public bool IsQuickItemOptionsOpen() => quickItemOptionsParent.gameObject.activeSelf;

    public UI_SkillSlot GetSkillSlot(SkillType skillType)
    {
        if(skillSlots == null)
            skillSlots = GetComponentsInChildren<UI_SkillSlot>(true);

        foreach (var slot in skillSlots)
        {
            if (slot.skillType == skillType)
            {
                slot.gameObject.SetActive(true);
                return slot;
            }
        }

        return null;
    }

    private void UpdateHealthBar()
    {
        float currentHealth = Mathf.RoundToInt(player.health.GetCurrentHealth());
        float maxHealth = player.stats.GetMaxHealth();
        float sizeDiffrnece = Mathf.Abs(maxHealth - healthRect.sizeDelta.x);

        if(sizeDiffrnece > .1f)
            healthRect.sizeDelta = new Vector2(maxHealth, healthRect.sizeDelta.y);

        healthText.text = currentHealth + "/" + maxHealth;
        healthSlider.value = player.health.GetHealthPercent();
    }

}
