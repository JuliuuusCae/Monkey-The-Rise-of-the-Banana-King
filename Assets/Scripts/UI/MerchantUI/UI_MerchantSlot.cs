using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UI_MerchantSlot : UI_ItemSlot
{
    private Inventory_Merchant merchant;
    public enum MerchantSlotType { MerchantSlot, PlayerSlot }
    public MerchantSlotType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (itemInSlot == null)
            return;

        bool rightButton = eventData.button == PointerEventData.InputButton.Right;
        bool leftButton = eventData.button == PointerEventData.InputButton.Left;

        if (slotType == MerchantSlotType.PlayerSlot)
        {
            if (rightButton)
            {
                bool sellFullStack = Keyboard.current.leftCtrlKey.isPressed;
                merchant.TrySellItem(itemInSlot, sellFullStack);
            }
            else if (leftButton)
            {
                base.OnPointerDown(eventData);
            }
            
        }
        else if (slotType == MerchantSlotType.MerchantSlot)
        {
            if (leftButton)
                return; // Left click does nothing

            bool buyFullStack = Keyboard.current.leftCtrlKey.isPressed;
            merchant.TryBuyItem(itemInSlot, buyFullStack);
        }

        ui.itemToolTip.ShowToolTip(false, null);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(itemInSlot == null) return;

        if (slotType == MerchantSlotType.MerchantSlot)
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, true, true, "RMB: Comprar\nLCTRL + RMB: Comprar Pilha");
        else
            ui.itemToolTip.ShowToolTip(true, rect, itemInSlot, false, true, "LMB: Equipar/Usar\nRMB: Vender\nLCTRL + RMB: Vender Pilha");
    }

    public void SetupMerchantUI(Inventory_Merchant merchant) => this.merchant = merchant;
}