using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //*************************************************************************
    //*************************************************************************
    // Atributes
    //*************************************************************************
    //*************************************************************************
    
    // Singleton
    private static GameManager instance = null;
    public static GameManager Instance {get{ return instance ;}}

    // Other classes
    public LevelController levelController;
    public Timer timer;

    // Attributes
    private bool isPlaying;
    private float timeBonus = 3f;
    private float timePenalty = 1f;
    private PlayerDataMessaging gameData;
    private bool tutorialAlreadyShown = false;

    //*************************************************************************
    //*************************************************************************

    //*************************************************************************
    //*************************************************************************
    // Unity Methods
    //*************************************************************************
    //*************************************************************************

    void Awake()
    {
        gameData = new PlayerDataMessaging();
        CreateSingleton();
        isPlaying = false;
    }

    void Update()
    {
        if (isPlaying && timer.GetRemainingTime() <= 0)
        {
            GameOver();
        }
    }

    //*************************************************************************
    //*************************************************************************
    // Class methods
    //*************************************************************************
    //*************************************************************************

    private void CreateSingleton()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ClickOnApple()
    {
        // TODO: apple animation
        // TODO: time bonus animation
        if(isPlaying){
            gameData.ApplesCollected++;
            levelController.NextLevel();
            timer.AddSeconds(timeBonus);
        }
        
    }

    public void ClickOnFruits()
    {
        // TODO: time penalty animation
        if(isPlaying){
            timer.AddSeconds(-timePenalty);
        }
    }

    //*************************************************************************
    //*************************************************************************

    public void PlayGame()
    {
        gameData = new PlayerDataMessaging();
        isPlaying = true;
        Time.timeScale = 1;
        timer.RestartTimer();
        levelController.StartGame();
        PowerUpsManager.Instance.UpdatePowerUpsOnPlayGame();
        if(!tutorialAlreadyShown){
            PauseGame();
            tutorialAlreadyShown = true;
        }
    }

    public void PauseGame()
    {
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPlaying = true;
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        isPlaying = false;
        PowerUpsManager.Instance.ClearPowerUpsGridLayout();
        Time.timeScale = 1;
        LevelController.Instance.level = 0;
    }

	public void GameOver()
	{
		// PRUEBAS
		//gameData.ApplesCollected = 100;
		//gameData.GamesPlayed = 100;
		//gameData.MaxLevel = 321;
		//gameData.TimeSpent = 600;
		//PlayerDataManager.Instance.PlayerData.PowerUpsCollection.DisableColor = 1;

        PowerUpsManager.Instance.ClearPowerUpsGridLayout();
        isPlaying = false;
        UiManager.Instance.OnGameOver();
		SaveSystem.SaveGame(gameData);
	}
}
