using TMPro;
using UnityEngine;

public class UI_PlayerCoins : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    private Inventory_Player inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<Inventory_Player>();
        inventory.OnInventoryChange += UpdateCoinsUI;
    }

    private void OnEnable() => UpdateCoinsUI();

    public void UpdateCoinsUI()
    {
        if (inventory == null) return;
        coinsText.text = $"Total: {inventory.gold}g";
    }
}
