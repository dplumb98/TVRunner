using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM_PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public Vector2 jumpHeight = new Vector2(0, 100);
    private bool moveLeft = true;
    private int rndNum;
    private Rigidbody2D playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft == true)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        rndNum = Random.Range(0, 40);
        if (rndNum == 1 && playerRB.velocity.y == 0)
        {
            playerRB.AddForce(jumpHeight, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWall"))
        {
            if (moveLeft == true)
            {
                moveLeft = false;
            }
            else
            {
                moveLeft = true;
            }
        }
    }
}
