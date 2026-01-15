using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIActions : MonoBehaviour
{

    [SerializeField] public GameObject InventoryUI;
    [SerializeField] public GameObject ShopUI;
    [SerializeField] public GameObject SettingsUI;
    [SerializeField] GameObject SellAllFishUI;
    [SerializeField] GameObject CaughtFishUI;
    [SerializeField] GameObject GoFishingButtonUI;
    [SerializeField] public GameObject PausedGameUI;

    public static UIActions instance;

    bool isHome = true;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InventoryUI.SetActive(false);
        ShopUI.SetActive(false);
        SettingsUI.SetActive(false);
        //SellAllFishUI.SetActive(false);
        CaughtFishUI.SetActive(false);
        GoFishingButtonUI.SetActive(true);
        PausedGameUI.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseOrResumeGame();
        }

    }

    public void HomeButton()
    {
        InventoryUI.SetActive(false);
        ShopUI.SetActive(false);
        SettingsUI.SetActive(false);
        isHome = true;
        if (!GoFishingButtonUI.activeInHierarchy)
        {
            GoFishingButtonUI.SetActive(true);
        }

    }
    public void InventoryButton()
    {
        if (InventoryUI.activeInHierarchy)
        {
            InventoryUI.SetActive(false);
        }
        else
        {
            InventoryUI.SetActive(true);
        }

        ShopUI.SetActive(false);
        SettingsUI.SetActive(false);
    }

    public void ShopButton()
    {
        if (ShopUI.activeInHierarchy && isHome)
        {
            ShopUI.SetActive(false);
        }
        else if (isHome)
        {
            ShopUI.SetActive(true);
        }
        InventoryUI.SetActive(false);
        SettingsUI.SetActive(false);

        if (!GoFishingButtonUI.activeInHierarchy && isHome)
        {
            GoFishingButtonUI.SetActive(true);
        }


    }

    public void SettingsButton()
    {
        if (SettingsUI.activeInHierarchy)
        {
            SettingsUI.SetActive(false);
        }
        else
        {
            SettingsUI.SetActive(true);
        }
        ShopUI.SetActive(false);
        InventoryUI.SetActive(false);
    }

    public void CloseSettingsButton()
    {
        SettingsUI.SetActive(false);
    }

    public void GoFishingButton()
    {
        InventoryUI.SetActive(false);
        ShopUI.SetActive(false);
        SettingsUI.SetActive(false);
        GoFishingButtonUI.SetActive(false);
        isHome = false;

    }

    public void SellAllFishButton()
    {
        SellAllFishUI.SetActive(true);
    }

    public void SellAllFishYesButton()
    {
        SellAllFishUI.SetActive(false);

        // implement sell thingy
    }

    public void SellAllFishNoButton()
    {
        SellAllFishUI.SetActive(false);
    }
    public void CaughtFishOkButton()
    {
        CaughtFishUI.SetActive(false);
    }

    public void ClosePauseGameUI()
    {
        PausedGameUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void PauseGameButton()
    {
        if (PausedGameUI.activeInHierarchy)
        {
            PausedGameUI.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            PausedGameUI.SetActive(true);
            Time.timeScale = 0f;
        }

    }


    void PauseOrResumeGame()
    {

        if (Input.GetKey(KeyCode.Escape) && !PausedGameUI.activeInHierarchy)
        {
            PausedGameUI.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PausedGameUI.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }



}
