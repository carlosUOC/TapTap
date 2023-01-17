using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour {
    
    // Singleton
    private static UiManager instance = null;
    public static UiManager Instance {
        get {
            return instance;
        }
    }

    // Variables used to animate the apples counter
    private int currentCollectedApples;
    private int minCollectedApples;
    private int currentTotalApples;
    private int maxTotalApples;
    private int scoreIncreaseRate = 1;

    // Canvas
    [Header("Canvas", order = 1)]
    public GameObject canvasHome;
    public GameObject canvasGame;

    // Pages
    [Header("Pages", order = 1)]
    public GameObject pageHome;
    public GameObject pageShop;
    public GameObject pageSettings;

    [Header("Settings page", order = 1)]
    public GameObject panelCredits;
    public GameObject iconMusicOn;
    public GameObject iconMusicOff;

    [Header("Game page", order = 1)]
    public GameObject popupPause;
    public TextMeshProUGUI textCurrentLevelInGame;
    public TextMeshProUGUI textRemainingTimeInGame;
    public GameObject popupGameOver;

    [Header("Pause Popup", order = 1)]
    public TextMeshProUGUI textCurrentLevelInPause;
    public TextMeshProUGUI textRemainingTimeInPause;
    public GameObject popupPauseExitGame;

    [Header("GameOver Popup", order = 1)]
    public TextMeshProUGUI textApplesCollectedInGame;
    public TextMeshProUGUI textTotalApplesCollected;    

    [Header("Exit Popup", order = 1)]
    public GameObject panelExitPopup;

    [Header("Private helpers", order = 1)]
    private GameObject currentPanel;


    // Unity Methods

    private void Awake() {
        if (!instance)
        {
            instance = this;
            currentPanel = pageHome;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        MusicController.controller.LoadMusicState();
        SaveSystem.LoadGame();

        // Set icon music according to saved state
        iconMusicOn.SetActive(MusicController.controller.IsPlaying());
        iconMusicOff.SetActive(!MusicController.controller.IsPlaying());
    }

    void Update() {
        // On back event
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentPanel == pageHome)
            {
                // Open the ExitPopup if it's not visible or hide it otherwise
                panelExitPopup.gameObject.SetActive(!panelExitPopup.gameObject.activeSelf);
            }
            else if (currentPanel == panelCredits)
            {
                OnChangeCurrentPanel(ref pageSettings);
            }
            else
            {
                OnBackToMainMenu();
            }
        }

        // Update timer state
        if (!GameManager.Instance.timer.IsPaused())
        {
            textRemainingTimeInGame.text = GameManager.Instance.timer.GetRemainingTimeAsString();
        }

        // Animate collected apples on GameOver
        // if (popupGameOver.activeSelf == true)
        // {
        //     AnimateCollectedApples();
        // }
    }

    // Class methods
    private void OnChangeCurrentPanel(ref GameObject panel) {
        panel.SetActive(true);
        currentPanel.SetActive(false);
        currentPanel = panel;
    }

    public void ChangeCurrentPage(string page) {
        switch (page)
        {
            case "shop": OnChangeCurrentPanel(ref pageShop); break;
            case "home": OnChangeCurrentPanel(ref pageHome); break;
            case "settings": OnChangeCurrentPanel(ref pageSettings); break;
            default: break;
        }
    }

    public void OnButtonPlayClicked() {
        canvasHome.SetActive(false);
        canvasGame.SetActive(true);
        GameManager.Instance.PlayGame();
    }

    public void OnButtonMusicClicked() {
        if (MusicController.controller.IsPlaying())
        {
            iconMusicOn.SetActive(false);
            iconMusicOff.SetActive(true);
            MusicController.controller.StopMusic();
        }
        else
        {
            iconMusicOn.SetActive(true);
            iconMusicOff.SetActive(false);
            MusicController.controller.PlayMusic();
        }
    }

    public void OnButtonPauseClicked() {
        GameManager.Instance.PauseGame();
        textCurrentLevelInPause.text = textCurrentLevelInGame.text;
        textRemainingTimeInPause.text = textRemainingTimeInGame.text;
        popupPause.SetActive(true);
    }

    public void OnButtonResumeClicked() {
        popupPause.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void OnButtonHomeFromPause() {
        popupPauseExitGame.SetActive(true);
    }

    public void OnButtonPlayAgainClicked() {
        popupGameOver.SetActive(false);
        GameManager.Instance.PlayGame();
    }

    public void OnButtonHomeFromGameOver() {
        popupGameOver.SetActive(false);
        canvasGame.SetActive(false);
        canvasHome.SetActive(true);
    }

    public void OnButtonShopFromGameOver() {
        popupGameOver.SetActive(false);
        canvasGame.SetActive(false);
        canvasHome.SetActive(true);
        ChangeCurrentPage("shop");
    }

    public void OnButtonAcceptExitGameClicked() {
        popupPauseExitGame.SetActive(false);
        popupPause.SetActive(false);
        canvasGame.SetActive(false);
        canvasHome.SetActive(true);
        GameManager.Instance.StopGame();
    }

    public void OnButtonCancelExitGameClicked() {
        popupPauseExitGame.SetActive(false);
    }

    public void OnButtonCreditsClicked() {
        OnChangeCurrentPanel(ref panelCredits);
    }

    public void OnBackToMainMenu() {
        OnChangeCurrentPanel(ref pageHome);
    }

    public void OnBackToSettingsMenu() {
        OnChangeCurrentPanel(ref pageSettings);
    }

    public void OnCancelExit() {
        panelExitPopup.SetActive(false);
    }

    public void OnCloseApplication() {
        Application.Quit();
    }

    public void OnGameOver() {
        // Store the collected apples in the run to animate the sum to the total
        currentCollectedApples = int.Parse(textCurrentLevelInGame.text) - 1;
        minCollectedApples = 0;
        currentTotalApples = PlayerDataManager.Instance.PlayerData.ApplesCollected;
        maxTotalApples = currentTotalApples + currentCollectedApples;
        textApplesCollectedInGame.text = currentCollectedApples.ToString();
        // textTotalApplesCollected.text = currentTotalApples.ToString();

        // Temporary until the animation is done, the good line is above
        textTotalApplesCollected.text = maxTotalApples.ToString();

        popupGameOver.SetActive(true);
    }

    private void AnimateCollectedApples() {
        if (currentTotalApples < maxTotalApples)
        {
            int scoreIncrement = (int)Time.deltaTime * scoreIncreaseRate;
            currentTotalApples += scoreIncrement;
            currentCollectedApples -= scoreIncrement;

            if (currentTotalApples > maxTotalApples)
                currentTotalApples = maxTotalApples;

            if (currentCollectedApples < minCollectedApples)
                currentCollectedApples = minCollectedApples;
        }
    }
}