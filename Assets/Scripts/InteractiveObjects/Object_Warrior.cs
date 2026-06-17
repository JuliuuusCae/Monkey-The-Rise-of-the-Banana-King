using UnityEngine;

public class Object_Warrior : Object_NPC, IInteractable
{
    [SerializeField] private DialogueLineSO firstDialogueLine;

    public void Interact()
    {
        ui.OpenDialogueUI(firstDialogueLine);
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        ui.HideAllTooltips();
    }
}
