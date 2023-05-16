using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float horizontal;
    private GameManager gameManager;
    public bool isOnGround = true;
    public bool gameOver = false; 
    private Rigidbody2D playerRb;
    public float gravityModifier;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && gameManager.isGameActive)
        {
            horizontal = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                isOnGround = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !gameOver && gameManager.isGameActive)
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Enemy") && !gameOver && gameManager.isGameActive)
        {
            gameOver = true;
            gameManager.GameOver();
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Win") && !gameOver && gameManager.isGameActive)
        {
            Debug.Log("You Win!");
            gameOver = true;
            gameManager.YouWin();
        }
    }
}
