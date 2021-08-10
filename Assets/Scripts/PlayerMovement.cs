using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Vector2 jumpHeight = new Vector2(0, 1);
    private Rigidbody2D playerRB;

    private Vector2 respawnPoint = new Vector2(0, -0.09f);

    [Tooltip("How far the player can fall down on the y axis before being respawned.")]
    public float fallLimit = -20;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetPlayerMovementEnabled() == true)
        {
            Movement();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("RespawnPoint"))
        {
            respawnPoint = collision.transform.position;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            transform.position = respawnPoint;
        }
    }

    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.right * speed * moveX * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && playerRB.velocity.y == 0)
        {
            playerRB.AddForce(jumpHeight, ForceMode2D.Impulse);
        }

        if (transform.position.y < fallLimit)
        {
            transform.position = respawnPoint;
        }
    }
}
