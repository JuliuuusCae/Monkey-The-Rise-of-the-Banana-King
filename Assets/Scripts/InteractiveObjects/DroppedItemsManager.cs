using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroppedItemsManager : MonoBehaviour, ISaveable
{
    public static DroppedItemsManager instance;

    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private ItemList_DataSO itemDataBase;

    private readonly List<Object_itemPickup> activeDrops = new List<Object_itemPickup>();

    private void Awake()
    {
        instance = this;
    }

    public void RegisterDrop(Object_itemPickup drop)
    {
        activeDrops.Add(drop);
    }

    public void UnregisterDrop(Object_itemPickup drop)
    {
        activeDrops.Remove(drop);
    }

    public void SaveData(ref GameData data)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        data.droppedItems.RemoveAll(d => d.sceneName == currentScene);
        activeDrops.RemoveAll(d => d == null);

        foreach (var drop in activeDrops)
        {
            data.droppedItems.Add(new DroppedItemSaveData(
                drop.GetItemData().saveId,
                drop.transform.position,
                currentScene
            ));
        }
    }

    public void LoadData(GameData data)
    {
        if (data.droppedItems == null)
            return;

        string currentScene = SceneManager.GetActiveScene().name;

        foreach (var savedDrop in data.droppedItems)
        {
            if (savedDrop.sceneName != currentScene)
                continue;

            Item_DataSO itemData = itemDataBase.GetItemData(savedDrop.saveId);

            if (itemData == null)
                continue;

            GameObject newItem = Instantiate(itemDropPrefab, savedDrop.position, Quaternion.identity);
            Object_itemPickup pickup = newItem.GetComponent<Object_itemPickup>();
            pickup.SetupItemStatic(itemData);
            activeDrops.Add(pickup);
        }
    }
}
