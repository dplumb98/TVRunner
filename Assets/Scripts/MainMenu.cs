using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public GameObject tutorialMenuUI;
    public GameObject highscoresUI;
    public Slider volumeSlider;
    public string sceneToLoad;
    public Volume normalVisionVolume;
    private new AudioSource audio;

    public Text levelOneText;
    public Text levelTwoText;
    public Text levelThreeText;

    private void Start()
    {
        // Set up player prefs
        GameManager.SetGameVolume(PlayerPrefs.GetFloat("GameVolume"));
        GameManager.SetLevelOneTimer(PlayerPrefs.GetFloat("LevelOneTimer"));
        GameManager.SetLevelTwoTimer(PlayerPrefs.GetFloat("LevelTwoTimer"));
        GameManager.SetLevelThreeTimer(PlayerPrefs.GetFloat("LevelThreeTimer"));

        // Change the High Score texts to match the high scores
        levelOneText.text = "Level One - " + PlayerPrefs.GetFloat("LevelOneTimer").ToString("00.00") + " seconds.";
        levelTwoText.text = "Level Two - " + PlayerPrefs.GetFloat("LevelTwoTimer").ToString("00.00") + " seconds.";
        levelThreeText.text = "Level Three - " + PlayerPrefs.GetFloat("LevelThreeTimer").ToString("00.00") + " seconds.";

        audio = GameObject.FindObjectOfType<AudioSource>(); // Grab our Audio Source
        volumeSlider.value = GameManager.GetGameVolume(); // Place our slider where it needs to be in the new level
        audio.volume = GameManager.GetGameVolume();
        if (audio.volume == 0)
        {
            audio.volume = 1;
            volumeSlider.value = audio.volume;
        }
        Cursor.visible = true; // Set our cursor to be visible (After leaving LevelThree)
    }

    public void Settings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void Tutorial()
    {
        mainMenuUI.SetActive(false);
        tutorialMenuUI.SetActive(true);
    }

    public void HighScores()
    {
        mainMenuUI.SetActive(false);
        highscoresUI.SetActive(true);
    }

    public void LeaveTutorial()
    {
        mainMenuUI.SetActive(true);
        tutorialMenuUI.SetActive(false);
    }

    public void LeaveSettings()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }

    public void LeaveHighScores()
    {
        mainMenuUI.SetActive(true);
        highscoresUI.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneToLoad);
        GameManager.SetLevel(1);
    }

    public void ChangeVolume()
    {
        audio.volume = volumeSlider.value;
        GameManager.SetGameVolume(volumeSlider.value);
    }

    public void ExitToDesktop()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void Fullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    public void Windowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void CheckForKeys()
    {
        if (!PlayerPrefs.HasKey("LevelOneTimer"))
        {
            PlayerPrefs.SetFloat("LevelOneTimer", 500);
        }
        if (!PlayerPrefs.HasKey("LevelTwoTimer"))
        {
            PlayerPrefs.SetFloat("LevelTwoTimer", 500);
        }
        if (!PlayerPrefs.HasKey("LevelThreeTimer"))
        {
            PlayerPrefs.SetFloat("LevelThreeTimer", 500);
        }
    }
}
