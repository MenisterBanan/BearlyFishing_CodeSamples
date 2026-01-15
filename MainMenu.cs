using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingsUI;
    [SerializeField] GameObject MainMenuButtonsUI;
    [SerializeField] GameObject CreditsUI;


    public void SettingsButton()
    {
        MainMenuButtonsUI.SetActive(false);
        SettingsUI.SetActive(true);
    }
    
    public void CloseSettingsButton()
    {
        SettingsUI.SetActive(false);
        MainMenuButtonsUI.SetActive(true);
    }
    public void CreditsButton()
    {
        MainMenuButtonsUI.SetActive(false);
        CreditsUI.SetActive(true);
    }
    public void CloseCreditsButton()
    {
        CreditsUI.SetActive(false);
        MainMenuButtonsUI.SetActive(true);
    }


    public void PlayGameButton()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

}
