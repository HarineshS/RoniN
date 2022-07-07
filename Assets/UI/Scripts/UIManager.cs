using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject controls;
    bool isPaused;

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            OnClickPauseButton();
        }
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenControls()
    {
        controls.SetActive(true);
    }

     public void CloseControls()
    {
        Time.timeScale = 1;
        controls.SetActive(false);
    }

    public void OnClickExitButton()
    {
        Debug.Log("You gave up!!");
        Application.Quit();
    }
    public void OnClickSettingsButton()
    {

    }

    void OnClickPauseButton()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnClickResumeButton()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

}
