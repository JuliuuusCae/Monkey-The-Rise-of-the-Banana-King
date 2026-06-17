using UnityEngine;
using UnityEngine.UI;

public class UI_ButtonSFX : MonoBehaviour
{
    private void Start()
    {
        foreach (var button in GetComponentsInChildren<Button>(true))
            button.onClick.AddListener(() => AudioManager.instance.PlayGlobalSFX("button_click"));
    }
}
