using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class AudioNode
{
    public AudioClip clip;
    [Range(0.1f, 1)] public float volume;
}
 
public class MusicController : MonoBehaviour
{
    public static MusicController controller = null;
    private AudioSource audioSource;
    private AudioListener audioListener;

    [SerializeField]
    private AudioNode initialMusic;
    [SerializeField]
    private AudioNode gameMusic;

    [Range(0.1f, 1)] public float generalVolume = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        if (controller == null)
            controller = this;
        else if (controller != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioListener = GetComponent<AudioListener>();
    }

    // Check if music is enabled in current settings
    public void LoadMusicState()
	{
        // Set initial music for home screen
    	SetInitialMusic();

    	// Check if the PlayerPrefs has a cached setting for the “music” key.
    	// initialize music component if not already done
    	if (!PlayerPrefs.HasKey("music"))
	   	{
	   		// music enabled
            PlayMusic();
	   	}
	   	else
	   	{
	   		// check if the music was on or off 
		    if (PlayerPrefs.GetInt("music") == 0)
		    {
		    	// mute music if it had to be off based on players configuration
				StopMusic();
		    }
            else
            {
                PlayMusic();
            }
	   	}
	}

    public void SetInitialMusic() => SetMusic(initialMusic);
    public void SetGameMusic() => SetMusic(gameMusic);
    public bool isMuted() => audioSource.mute;
    public bool isPlaying() => audioSource.isPlaying;

    public void PlayMusic()
    {
        audioSource.Play();
        PlayerPrefs.SetInt("music", 1);
    }

    public void StopMusic()
    {
        audioSource.Stop();
        PlayerPrefs.SetInt("music", 0);
    }
    public void SetVolume(float intensity) => generalVolume = intensity;
    public bool IsPlaying() => audioSource.isPlaying;
    public void Pause() => audioSource.Pause();
    public void UnPause() => audioSource.UnPause();
    public void SetMusic(AudioNode newClip)
    {
        if(newClip.clip != audioSource.clip)
        {
            audioSource.volume = newClip.volume;
            audioSource.clip = newClip.clip;
        }
    }
}
