using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Property Definitions/Global Variables
    private static bool _PlayerCollided = false;
    private static bool _LoadingIn = true;
    private static bool _PlayerMovementEnabled = true;
    private static float _FadeTime = 0.00625f;
    private static int _Level = 0;
    private static float _GameVolume = 1;
    private static float _LevelOneTimer;
    private static float _LevelTwoTimer;
    private static float _LevelThreeTimer;
    #endregion

    #region Color Objects
    private Camera mainCam;
    private GameObject player;
    private GameObject endLevel;
    private GameObject[] groundPieces;
    private GameObject[] enemies;
    private GameObject[] psychs;
    #endregion

    #region LevelOneArtRGBs
    private Color backgroundColor0 = new Color(0.7019608f, 0.5333334f, 0.9215686f, 1);
    private Color playerColor0 = new Color(0.4470588f, 0.8666667f, 0.9686275f, 1);
    private Color endLevelColor0 = new Color(1, 0.9882353f, 0.1921569f, 1);
    private Color groundColor0 = new Color(0.9686275f, 0.682353f, 0.972549f, 1);
    private Color enemiesColor0 = new Color(0.854902f, 0.2431373f, 0.3215686f, 1);
    private Color psychsColor0 = new Color(0.6627451f, 0.9921569f, 0.6745098f, 1);
    #endregion

    #region LevelTwoArtRGBs
    private Color backgroundColor1 = new Color(0.9568627f, 0.9529412f, 0.9333333f, 1);
    private Color playerColor1 = new Color(0.9686275f, 0.4980392f, 0, 1);
    private Color endLevelColor1 = new Color(0.9882353f, 0.7490196f, 0.2862745f, 1);
    private Color groundColor1 = new Color(0, 0.1882353f, 0.2862745f, 1);
    private Color enemiesColor1 = new Color(0.8392157f, 0.1568628f, 0.1568628f, 1);
    private Color psychsColor1 = new Color(0.4313726f, 0.5647059f, 0.4588235f, 1);
    #endregion

    #region LevelThreeArtRBGs
    private Color backgroundColor2 = new Color(0.9372549f, 0.8392157f, 0.6745098f, 1);
    private Color playerColor2 = new Color(0.09411765f, 0.227451f, 0.2156863f, 1);
    private Color endLevelColor2 = new Color(1, 0.9882353f, 0.1921569f, 1);
    private Color groundColor2 = new Color(0, 0.6509804f, 0.6509804f, 1);
    private Color enemiesColor2 = new Color(0.8588235f, 0.227451f, 0.2039216f, 1);
    private Color psychsColor2 = new Color(0.4784314f, 0.2313726f, 0.4117647f, 1);
    #endregion

    public Volume normalVisionVolume;
    private new AudioSource audio;
    private Vignette vign;
    private Scene scene;
    public Text timerText;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name != "MainMenu")
        {
            SetRGBs();

            audio = AudioSource.FindObjectOfType<AudioSource>(); // Grab the Audio Source in the scene
            _PlayerMovementEnabled = true; // Enable player movement when the scene begins
            Cursor.visible = false; // Make the cursor not visible
            normalVisionVolume.profile.TryGet<Vignette>(out vign);
            vign.intensity.value = 1;
            timer = 0; // Reset timer when level loads
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleTimer();
        FadeIn();
    }

    // Set up our prefs if the player quits the game
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("GameVolume", _GameVolume);
    }

    // Set up our prefs if the player quits the game or the scene ends (this is mainly for Exit to Menu)
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("GameVolume", _GameVolume);
    }

    private void FadeIn()
    {
        if (scene.name != "MainMenu")
        {
            if (vign.intensity.value != 0 && _PlayerCollided == false)
            {
                vign.intensity.value -= _FadeTime;
            }

            if (vign.intensity.value == 0)
            {
                _LoadingIn = false;
            }
        }
    }

    private void HandleTimer()
    {
        if (scene.name == "LevelOne")
        {
            if (_PlayerCollided == false)
            {
                timer += Time.deltaTime; // Count time passing
                timerText.text = timer.ToString("0.00"); // Pass it to our timer UI
            }
            else if (_PlayerCollided == true)
            {
                if (PlayerPrefs.GetFloat("LevelOneTimer") > timer || PlayerPrefs.GetFloat("LevelOneTimer") == 0)
                {
                    PlayerPrefs.SetFloat("LevelOneTimer", timer);
                }
            }
        }
        else if (scene.name == "LevelTwo")
        {
            if (_PlayerCollided == false)
            {
                timer += Time.deltaTime; // Count time passing
                timerText.text = timer.ToString("0.00"); // Pass it to our timer UI
            }
            else if (_PlayerCollided == true)
            {
                if (PlayerPrefs.GetFloat("LevelTwoTimer") > timer || PlayerPrefs.GetFloat("LevelTwoTimer") == 0)
                {
                    PlayerPrefs.SetFloat("LevelTwoTimer", timer);
                }
            }
        }
        else if (scene.name == "LevelThree")
        {
            if (_PlayerCollided == false)
            {
                timer += Time.deltaTime; // Count time passing
                timerText.text = timer.ToString("0.00"); // Pass it to our timer UI
            }
            else if (_PlayerCollided == true)
            {
                if (PlayerPrefs.GetFloat("LevelThreeTimer") > timer || PlayerPrefs.GetFloat("LevelThreeTimer") == 0)
                {
                    PlayerPrefs.SetFloat("LevelThreeTimer", timer);
                }
            }
        }
    } 

    private void SetRGBs()
    {
        // Fill our variables and arrays with our GameObjects
        mainCam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        endLevel = GameObject.FindGameObjectWithTag("EndLevel");
        groundPieces = GameObject.FindGameObjectsWithTag("Ground");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        psychs = GameObject.FindGameObjectsWithTag("Psychedelic");

        SpriteRenderer sr; // Create our SpriteRenderer object

        // Change colors
        if (scene.name == "LevelOne")
        {
            mainCam.backgroundColor = backgroundColor0;
        }
        else if (scene.name == "LevelTwo")
        {
            mainCam.backgroundColor = backgroundColor1;
        }
        else if (scene.name == "LevelThree")
        {
            mainCam.backgroundColor = backgroundColor2;
        }

        if (scene.name == "LevelOne")
        {
            sr = player.GetComponent<SpriteRenderer>();
            sr.color = playerColor0;
        }
        else if (scene.name == "LevelTwo")
        {
            sr = player.GetComponent<SpriteRenderer>();
            sr.color = playerColor1;
        }
        else if (scene.name == "LevelThree")
        {
            sr = player.GetComponent<SpriteRenderer>();
            sr.color = playerColor2;
        }

        if (scene.name == "LevelOne")
        {
            sr = endLevel.GetComponent<SpriteRenderer>();
            sr.color = endLevelColor0;
        }
        else if (scene.name == "LevelTwo")
        {
            sr = endLevel.GetComponent<SpriteRenderer>();
            sr.color = endLevelColor1;
        }
        else if (scene.name == "LevelThree")
        {
            sr = endLevel.GetComponent<SpriteRenderer>();
            sr.color = endLevelColor2;
        }

        for (int i = 0; i < groundPieces.Length; i++)
        {
            if (scene.name == "LevelOne")
            {
                sr = groundPieces[i].GetComponent<SpriteRenderer>();
                sr.color = groundColor0;
            }
            else if (scene.name == "LevelTwo")
            {
                sr = groundPieces[i].GetComponent<SpriteRenderer>();
                sr.color = groundColor1;
            }
            else if (scene.name == "LevelThree")
            {
                sr = groundPieces[i].GetComponent<SpriteRenderer>();
                sr.color = groundColor2;
            }
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (scene.name == "LevelOne")
            {
                sr = enemies[i].GetComponent<SpriteRenderer>();
                sr.color = enemiesColor0;
            }
            else if (scene.name == "LevelTwo")
            {
                sr = enemies[i].GetComponent<SpriteRenderer>();
                sr.color = enemiesColor1;
            }
            else if (scene.name == "LevelThree")
            {
                sr = enemies[i].GetComponent<SpriteRenderer>();
                sr.color = enemiesColor2;
            }
        }

        for (int i = 0; i < psychs.Length; i++)
        {
            if (scene.name == "LevelOne")
            {
                sr = psychs[i].GetComponent<SpriteRenderer>();
                sr.color = psychsColor0;
            }
            else if (scene.name == "LevelTwo")
            {
                sr = psychs[i].GetComponent<SpriteRenderer>();
                sr.color = psychsColor1;
            }
            else if (scene.name == "LevelThree")
            {
                sr = psychs[i].GetComponent<SpriteRenderer>();
                sr.color = psychsColor2;
            }
        }
    }

    #region Properties
    public static bool GetPlayerCollided()
    {
        return _PlayerCollided;
    }

    public static void SetPlayerCollided(bool collided)
    {
        _PlayerCollided = collided;
    }

    public static float GetFadeTime()
    {
        return _FadeTime;
    }

    public static int GetLevel()
    {
        return _Level;
    }

    public static void SetLevel(int level)
    {
        _Level = level;
    }

    public static bool GetLoadingIn()
    {
        return _LoadingIn;
    }

    public static void SetLoadingIn(bool loading)
    {
        _LoadingIn = loading;
    }

    public static bool GetPlayerMovementEnabled()
    {
        return _PlayerMovementEnabled;
    }

    public static void SetPlayerMovementEnabled(bool enabled)
    {
        _PlayerMovementEnabled = enabled;
    }

    public static float GetGameVolume()
    {
        return _GameVolume;
    }

    public static void SetGameVolume(float volume)
    {
        _GameVolume = volume;
    }

    public static float GetLevelOneTimer()
    {
        return _LevelOneTimer;
    }

    public static void SetLevelOneTimer(float timer)
    {
        _LevelOneTimer = timer;
    }

    public static float GetLevelTwoTimer()
    {
        return _LevelTwoTimer;
    }

    public static void SetLevelTwoTimer(float timer)
    {
        _LevelTwoTimer = timer;
    }

    public static float GetLevelThreeTimer()
    {
        return _LevelThreeTimer;
    }

    public static void SetLevelThreeTimer(float timer)
    {
        _LevelThreeTimer = timer;
    }
    #endregion
}
