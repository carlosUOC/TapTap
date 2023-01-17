using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class PlayerDataManager
{
    private readonly static PlayerDataManager instance = new PlayerDataManager();
    private PlayerDataMessaging playerData;

    private PlayerDataManager(){
    	playerData = new PlayerDataMessaging();
    }

    public static PlayerDataManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void SetLoadedPlayerData(PlayerDataMessaging gameData){
        playerData.MaxLevel = gameData.MaxLevel;
        playerData.GamesPlayed = gameData.GamesPlayed;
        playerData.ApplesCollected = gameData.ApplesCollected;
        playerData.TimeSpent = gameData.TimeSpent;
        playerData.PowerUpsCollection = gameData.PowerUpsCollection;
    }

    public PlayerDataMessaging UpdatePlayerData(PlayerDataMessaging gameData){
        if(gameData != null){
            if(gameData.MaxLevel > playerData.MaxLevel){
                playerData.MaxLevel = gameData.MaxLevel;
            }
            playerData.GamesPlayed = playerData.GamesPlayed + 1;
            playerData.ApplesCollected = playerData.ApplesCollected + gameData.ApplesCollected;
            playerData.TimeSpent = playerData.TimeSpent + gameData.TimeSpent;
        }
            
        return playerData;
    }

    public PlayerDataMessaging PlayerData
    {
        get{ return playerData;}
        set{ playerData = value;}
    }

}
