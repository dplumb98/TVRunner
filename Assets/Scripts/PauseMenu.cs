using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static bool _IsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    private new AudioSource audio;
    public Slider volumeSlider;
    public string sceneToLoad;

    private void Start()
    {
        audio = GameObject.FindObjectOfType<AudioSource>(); // Get the Audio Source in our scene
        volumeSlider.value = GameManager.GetGameVolume(); // Place our slider where it needs to be in the new level
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_IsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        audio.Play();
        pauseMenuUI.SetActive(false); // Disable our UI
        settingsMenuUI.SetActive(false); // Disable the settings menu in case Escape was pressed from there
        Time.timeScale = 1; // Set time to normal
        _IsPaused = false;
        Cursor.visible = false; // Make our cursor not visible
    }

    private void Pause()
    {
        audio.Pause();
        pauseMenuUI.SetActive(true); // Enable our UI
        Time.timeScale = 0; // Freeze time
        _IsPaused = true;
        Cursor.visible = true; // Make our cursor visible
    }

    public void Settings()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void LeaveSettings()
    {
        pauseMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitToDesktop()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void ChangeVolume()
    {
        audio.volume = volumeSlider.value;
        GameManager.SetGameVolume(volumeSlider.value);
    }

    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void Windowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    #region Properties
    public static bool GetIsPaused()
    {
        return _IsPaused;
    }

    public static void SetIsPaused(bool isPaused)
    {
        _IsPaused = isPaused;
    }
    #endregion
}
