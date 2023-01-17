using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    [Tooltip("Initial time in seconds")]
    public int initialTime;

    [Tooltip("To show hours in timer?")]
    public bool hoursIncluded = false;

    [Tooltip("To show milliseconds in timer?")]
    public bool miliSecondsIncluded = false;

    [Tooltip("Timer time scale")]
    [Range(-10.0f, 10.0f)]
    public float timeScale = 1;

    [Tooltip("To enable count down or up")]
    public bool isCountdown = true;

    private string remainingTimeAsString;
    private string remainingHours;
    private string remainingMinutes;
    private string remainingSeconds;
    private string remainingMilliseconds;
    private float frameTimeWithTimeScale = 0f;
    private float remainingTime = 0f;
    private float timeScaleWhenPaused, initialTimeScale;
    private bool isPaused = false;

    void Awake()
    {
        // To set the initial time scale
        initialTimeScale = timeScale;
        // To init the variable that accumulates the time per frame with the initial time
        remainingTime = initialTime;

        UpdateTimer(initialTime);
    }

    void Update()
    {
        if (!isPaused)
        {
            // The next variable is the time per frame considering time sclae
            frameTimeWithTimeScale = Time.deltaTime * timeScale;
            // The next variable accumulates the time passed to show in the UI
            if (isCountdown)
                remainingTime -= frameTimeWithTimeScale;
            else
                remainingTime += frameTimeWithTimeScale;
            UpdateTimer(remainingTime);
        }
    }

    public void RestartTimer()
    {
        remainingTime = initialTime;
    }

    public void UpdateTimer(float timeInSeconds)
    {
        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        int milliseconds = 0;

        // To ensure that time is not negative
        if (timeInSeconds < 0)
        {
            timeInSeconds = 0;
        }

        seconds = (int)timeInSeconds % 60;
        remainingSeconds = seconds.ToString("00");

        if (hoursIncluded)
        {
            hours = (int)timeInSeconds / 3600;
            minutes = (int)(timeInSeconds - (hours * 3600)) / 60;

            // To create the string with 2 digits for the hours, for the minutes and seconds, separated by ":"
            remainingHours = hours.ToString("00");
            remainingMinutes = minutes.ToString("00");
            remainingTimeAsString = remainingHours + ":" + remainingMinutes + ":" + remainingSeconds;
        }
        else
        {
            // To calculate minutes and seconds
            minutes = (int)timeInSeconds / 60;

            // To create the string with 2 digits for the minutes and seconds, separated by ":"
            remainingMinutes = minutes.ToString("00");
            remainingTimeAsString = remainingMinutes + ":" + remainingSeconds;
        }

        if (miliSecondsIncluded)
        {
            milliseconds = (int)((timeInSeconds * 1000) % 1000);
            remainingTimeAsString += milliseconds.ToString("000");
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            timeScaleWhenPaused = timeScale;
            timeScale = 0;
        }
    }

    public void Continue()
    {
        if (isPaused)
        {
            isPaused = false;
            timeScale = timeScaleWhenPaused;
        }
    }

    public void Restart()
    {
        isPaused = false;
        timeScale = initialTimeScale;
        remainingTime = initialTime;
        UpdateTimer(remainingTime);
    }

    public void AddSeconds(float sec)
    {
        remainingTime += sec;
        UpdateTimer(remainingTime);
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }

    public string GetRemainingTimeAsString()
    {
        return remainingTimeAsString;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
