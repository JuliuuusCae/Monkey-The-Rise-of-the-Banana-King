using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class DroppedItemSaveData
{
    public string saveId;
    public Vector3 position;
    public string sceneName;

    public DroppedItemSaveData(string saveId, Vector3 position, string sceneName)
    {
        this.saveId = saveId;
        this.position = position;
        this.sceneName = sceneName;
    }
}

[Serializable]
public class GameData
{
    public int gold;

    public List<Inventory_Item> itemList;
    public SerializableDictionary<string, int> inventory; // itemSaveId -> stackSize
    public SerializableDictionary<string, int> storageItems;
    public SerializableDictionary<string, int> storageMaterials;

    public SerializableDictionary<string, ItemType> equipedItems; // itemSaveId -> slotType;

    public int skillPoints;
    public SerializableDictionary<string, bool> skillTreeUI; // skill name -> unlock status
    public SerializableDictionary<SkillType, SkillUpgradeType> skillUpgrades; // skill type -> upgrade type

    public SerializableDictionary<string, bool> unlockedCheckpoints; // checkpoint id -> unlocked status
    public SerializableDictionary<string, Vector3> inScenePortals; // scene name > portal position

    public string portalDestinationSceneName;
    public bool returningFromTown;

    public string lastScenePlayed;
    public Vector3 lastPlayerPosition;

    public List<DroppedItemSaveData> droppedItems;

    public SerializableDictionary<string, bool> openedChests;
    public SerializableDictionary<string, bool> killedEnemies;

    public GameData()
    {
        inventory = new SerializableDictionary<string, int>();
        storageItems = new SerializableDictionary<string, int>();
        storageMaterials = new SerializableDictionary<string, int>();

        equipedItems = new SerializableDictionary<string, ItemType>();

        skillTreeUI = new SerializableDictionary<string, bool>();
        skillUpgrades = new SerializableDictionary<SkillType, SkillUpgradeType>();

        unlockedCheckpoints = new SerializableDictionary<string, bool>();
        inScenePortals = new SerializableDictionary<string, Vector3>();

        droppedItems = new List<DroppedItemSaveData>();

        openedChests = new SerializableDictionary<string, bool>();
        killedEnemies = new SerializableDictionary<string, bool>();
    }
}
