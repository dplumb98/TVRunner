using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableRespawnPoint : MonoBehaviour
{
    private SpriteRenderer[] objects; // Array of SpriteRenderer to store all the SpriteRenderers in our children
    private Color respawnColor0 = new Color(0.4470588f, 0.8666667f, 0.9686275f, 1);
    private Color respawnColor1 = new Color(0.9686275f, 0.4980392f, 0, 1);
    private Color respawnColor2 = new Color(0.09411765f, 0.227451f, 0.2156863f, 1);
    private Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        objects = GetComponentsInChildren<SpriteRenderer>(); // Grab all of the SpriteRenderers in our children
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check to see if we collided with the Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Loop over our array of SpriteRenderers
            for (int i = 0; i < objects.Length; i++)
            {
                if (scene.name == "LevelOne")
                {
                    objects[i].color = respawnColor0;
                }
                else if (scene.name == "LevelTwo")
                {
                    objects[i].color = respawnColor1;
                }
                else if (scene.name == "LevelThree")
                {
                    objects[i].color = respawnColor2;
                }
            }
        }
    }
}
