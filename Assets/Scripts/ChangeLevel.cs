using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeLevel : MonoBehaviour
{
    private Scene scene;
    public Volume normalVisionVolume;
    private Vignette vign;

    private void Start()
    {
        normalVisionVolume.profile.TryGet<Vignette>(out vign); // Grab our vignette override
    }

    private void Update()
    {
        if (vign.intensity.value != 1 && GameManager.GetPlayerCollided() == true)
        {
            vign.intensity.value += GameManager.GetFadeTime();
        }

        if (vign.intensity.value == 1 && GameManager.GetPlayerCollided() == true)
        {
            GameManager.SetPlayerCollided(false);
            ChangeScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.SetPlayerCollided(true); // We collided with the player
            GameManager.SetPlayerMovementEnabled(false); // Disable player movement
        }
    }

    private void ChangeScene()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "LevelOne")
        {
            SceneManager.LoadScene("LevelTwo");
            GameManager.SetLevel(1);
        }
        else if (scene.name == "LevelTwo")
        {
            SceneManager.LoadScene("LevelThree");
            GameManager.SetLevel(2);
        }
        else if (scene.name == "LevelThree")
        {
            SceneManager.LoadScene("MainMenu");
            GameManager.SetLevel(0);
        }
    }
}
