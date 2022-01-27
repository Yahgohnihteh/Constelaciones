/*using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Instanciate all settings panel buttons
    /*public Button musicOnButton;
    public Button musicOffButton;
    public Button soundOnButton;
    public Button soundOffButton;*/
/*
    public Animator transition;
    public float transitionTime;*/

    // Start is called before the first frame update
    /*    void Start()
        {
            // Give visual feedback to the users by enabling/disabling the sound and music buttons
            if (PlayerPrefs.GetInt("MusicOnOff") == 1)
            {
                musicOnButton.interactable = false;
                musicOffButton.interactable = true;
            }

            if (PlayerPrefs.GetInt("SoundOnOff") == 1)
            {
                soundOnButton.interactable = false;
                soundOffButton.interactable = true;
            }
        } */
/*
    public void LoadNextScene(int sceneIndex)
    {

        StartCoroutine(NextSceneCall(sceneIndex));
    }

    IEnumerator NextSceneCall(int sceneIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitCall()
    {
        Application.Quit(); // Close app
    }
}*/

    // Give instant audible feedback to users and change music Player Prefs
 /*   public void MusicOnOff(int musicId)
    {
        AudioManager.instance.SetMusic(musicId);
        AudioManager.instance.TurnMusicOnOff();
    }
    // Give instant audible feedback to users and change sound Player Prefs
    public void SoundOnOff(int soundId)
    {
        AudioManager.instance.SetSound(soundId);
        AudioManager.instance.TurnSoundOnOff();
    }
*/
