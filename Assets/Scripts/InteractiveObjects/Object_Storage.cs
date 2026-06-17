using UnityEngine;

public class Object_Storage : Object_NPC, IInteractable
{
    [SerializeField] private DialogueLineSO firstDialogueLine;

    private Animator anim;
    private Inventory_Player inventory;
    private Inventory_Storage storage;


    protected override void Awake()
    {
        base.Awake();
        storage = GetComponent<Inventory_Storage>();
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isStorage", true);
    }

    public void Interact()
    {
        ui.storageUI.SetupStorageUI(inventory, storage);
        ui.OpenDialogueUI(firstDialogueLine);

        //ui.OpenStorageUI(true);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        inventory = player.GetComponent<Inventory_Player>();
        storage.SetInventory(inventory);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        ui.HideAllTooltips();
        ui.OpenStorageUI(false);
    }
}
