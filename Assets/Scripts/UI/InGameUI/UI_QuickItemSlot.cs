using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_QuickItemSlot : UI_ItemSlot
{
    private Button button;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private int slotNumber;

    protected override void Awake()
    {
        base.Awake();
        button = GetComponent<Button>();
    }

    public void SetupQuickSlotItem(Inventory_Item itemToPass)
    {
        inventory.SetQuickItemInSlot(slotNumber, itemToPass);
    }

    public void SimulateButtonFeedback()
    {
        EventSystem.current.SetSelectedGameObject(button.gameObject);
        ExecuteEvents.Execute(button.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }

    public void UpdateQuickSlotUI(Inventory_Item currentItemInSlot)
    {
        if (currentItemInSlot == null || currentItemInSlot.itemData == null)
        {
            itemIcon.sprite = defaultSprite;
            itemIcon.color = Color.white;
            itemStackSize.text = "";
            return;
        }

        Color color = Color.white; color.a = .9f;
        itemIcon.color = color;
        itemIcon.sprite = currentItemInSlot.itemData.itemIcon;
        itemStackSize.text = currentItemInSlot.stackSize.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (ui.inGameUI.IsQuickItemOptionsOpen())
        {
            ui.HideQuickItemOptionsUI();
            return;
        }

        ui.OpenQuickItemOptionsUI(this, rect);
    }

}
    