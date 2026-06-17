using UnityEngine;
using UnityEngine.SceneManagement;

public class Object_Waypoint : MonoBehaviour
{

    [SerializeField] private string transferToScene;
    [Space]
    [SerializeField] private RespawnType waypointType;
    [SerializeField] private RespawnType conntedWaypoint;
    [SerializeField] private Transform respwanPoint;
    [SerializeField] private bool canBeTriggered = true;
    [Header("Key")]
    [SerializeField] private bool needsKey;
    [SerializeField] private Item_DataSO requiredKey;
    [SerializeField] private DialogueLineSO lockedDialogueLine;
    [Header("Boss Defeated Override")]
    [SerializeField] private string bossEnemyId;
    [SerializeField] private string sceneIfBossDefeated;

    public RespawnType GetWaypointType() => waypointType;

    public Vector3 GetPositionAndSetTriggerFalse()
    {
        canBeTriggered = false;
        return respwanPoint == null ? transform.position : respwanPoint.position;
    }

    private void OnValidate()
    {
        gameObject.name = "Object_Waypoint - " + waypointType.ToString() + " - " + transferToScene;

        if (waypointType == RespawnType.Enter)
            conntedWaypoint = RespawnType.Exit;

        if (waypointType == RespawnType.Exit)
            conntedWaypoint = RespawnType.Enter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeTriggered == false)
            return;

        if (needsKey)
        {
            Inventory_Base inventory = collision.GetComponent<Inventory_Base>();
            bool hasKey = inventory != null &&
                          inventory.itemList.Find(item => item.itemData == requiredKey) != null;

            if (!hasKey)
            {
                canBeTriggered = false;
                if (lockedDialogueLine != null)
                    UI.instance.OpenDialogueUI(lockedDialogueLine);
                return;
            }
        }

        string destination = GetDestinationScene();

        if (destination == sceneIfBossDefeated && !string.IsNullOrEmpty(sceneIfBossDefeated))
            GameManager.instance.showFinalScreen = true;

        GameManager.instance.ChangeScene(destination, conntedWaypoint);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canBeTriggered = true;
    }

    private string GetDestinationScene()
    {
        if (string.IsNullOrEmpty(bossEnemyId) || string.IsNullOrEmpty(sceneIfBossDefeated))
            return transferToScene;

        var data = SaveManager.instance.GetGameData();
        if (data.killedEnemies.TryGetValue(bossEnemyId, out bool isKilled) && isKilled)
            return sceneIfBossDefeated;

        return transferToScene;
    }
}
