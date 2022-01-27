using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour
{

    public float transitionTime;
    public Animator transition;
    public static MySceneManager instance;
    public Animator musicOnOffButtonAnimator;
    public bool buttonEnabled = true;

    public void NextSceneCall(string sceneName)
    {
        AudioManager.instance.Play("Button_Click");
        // Calls the scene given inside the button in the Unity project
        StartCoroutine(LoadNextScene(sceneName));
    }

    //Change scene using a dissolve transition, IEnumerator method used to sleep the program during the transition
    IEnumerator LoadNextScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(sceneName);
    }

    //Change music button state to give visual feedback to the user and play or pause the background theme
    public void MusicManager()
    {
        StartCoroutine (Wait());
    }
    IEnumerator Wait() //Check if a line clicked must be deleted
    {
        if (buttonEnabled == true)
        {
            AudioManager.instance.Pause("Main_Theme");
            //new YieldInstruction that waits for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
            musicOnOffButtonAnimator.SetTrigger("Disabled");
            buttonEnabled = false;
        }
        else
        {
            AudioManager.instance.Play("Main_Theme");
            //new YieldInstruction that waits for 0.5 seconds
            yield return new WaitForSeconds(0.5f);
            musicOnOffButtonAnimator.SetTrigger("Normal");
            buttonEnabled = true;
        }
    }

        public void ExitCall()
    {
        Application.Quit(); // Close app
    }
}
