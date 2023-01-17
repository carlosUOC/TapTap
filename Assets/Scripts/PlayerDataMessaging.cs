using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerDataMessaging
{
    private int maxLevel;
    private float timeSpent;
    private int applesCollected;
    private int gamesPlayed;
    private PowerUpsCollection powerUpsCollection;

    public PlayerDataMessaging(){
        maxLevel = 0;
        timeSpent = 0;
        applesCollected = 0;
        gamesPlayed = 0;
        powerUpsCollection = new PowerUpsCollection();
    }

    public int MaxLevel
    {
        get{ return maxLevel;}
        set{ maxLevel = value;}
    }

    public int GamesPlayed
    {
        get{ return gamesPlayed;}
        set{ gamesPlayed = value;}
    }

    public int ApplesCollected
    {
        get{ return applesCollected;}
        set{ applesCollected = value;}
    }

    public float TimeSpent
    {
        get{ return timeSpent;}
        set{ timeSpent = value;}
    }

    public PowerUpsCollection PowerUpsCollection
    {
        get{ return powerUpsCollection;}
        set{ powerUpsCollection = value;}
    }

}
