using UnityEngine;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject finalScreen;

    private void Start()
    {
        transform.root.GetComponentInChildren<UI_Options>(true).LoadUpVolume();
        transform.root.GetComponentInChildren<UI_FadeScreen>().DoFadeIn();

        AudioManager.instance.StartBGM("playlist_mainMenu");

        if (GameManager.instance.showFinalScreen)
        {
            finalScreen.SetActive(true);
            GameManager.instance.showFinalScreen = false;
        }
    }

    public void PlayBTN()
    {
        AudioManager.instance.PlayGlobalSFX("button_click");
        GameManager.instance.ContinuePlay();
    }

    public void QuitGameBTN()
    {
        Application.Quit();
    }
}