/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    //Instanciate all the audio files used
    public AudioSource bgMusic;
    public AudioSource uiButtonClickSound;
    public AudioSource correctSound;
    public AudioSource wrongSound;
    public AudioSource starGraplingSound;
    // Create an instance of SounManagerScript to be able to call it outside of this script
    public static SoundManagerScript instance;

    void Awake()
    {
        MakeSingleton();
        DontDestroyOnLoad(this.gameObject); //Make sure that the AudioManager doesn't get destroyed
    }

    // Set values to the integers acting as booleans if it is the first time starting the app 
    private void Start() 
    {
        if (!PlayerPrefs.HasKey("FirstTimeSoundCheck"))
        {
            PlayerPrefs.SetInt("MusicOnOff", 1);
            PlayerPrefs.SetInt("SoundOnOff", 1);
            PlayerPrefs.SetInt("FirstTimeSoundCheck", 0);
        }
        TurnMusicOnOff();
        TurnSoundOnOff();
    }

    // Disable/Enable the music depending on the Player Preferences
    public void TurnMusicOnOff()
    {
        if(GetMusic() == 1)
        {
            bgMusic.enabled = true;
        }
        else
        {
            bgMusic.enabled = false;
        }
    }
    // Disable/Enable the sounds depending on the Player Preferences
    public void TurnSoundOnOff()
    {
        if(GetSound() == 1)
        {
            uiButtonClickSound.enabled = true;
            correctSound.enabled = true;
            wrongSound.enabled = true;
            starGraplingSound.enabled = true;
        }
        else
        {
            uiButtonClickSound.enabled = false;
            correctSound.enabled = false;
            wrongSound.enabled = false;
            starGraplingSound.enabled = false;
        }
    }

    //Ensure that there's only one AudioManager in the scene
    void MakeSingleton()
    { 
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    
    public void SetMusic(int isOn) // Change Player Prefs
    {
        PlayerPrefs.SetInt("MusicOnOff", isOn);
    }
    public int GetMusic() // Get Player Prefs
    {
        return PlayerPrefs.GetInt("MusicOnOff");
    }
    public void SetSound(int isOn) // Change Player Prefs
    {
        PlayerPrefs.SetInt("SoundOnOff", isOn);
    }
    public int GetSound() // Get Player Prefs
    {
        return PlayerPrefs.GetInt("SoundOnOff");
    }
}*/
