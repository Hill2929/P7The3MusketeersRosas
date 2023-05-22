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
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && gameManager.isGameActive)
        {
            horizontal = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed);
            Vector2 move = new Vector2(horizontal, 0);

            if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(0.0f, 0.0f))
            {
                lookDirection.Set(move.x, 0);
                lookDirection.Normalize();
            }
            animator.SetFloat("Look X", lookDirection.x);
            animator.SetFloat("Speed", move.magnitude);
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * 10, ForceMode2D.Impulse);
                isOnGround = false;
                animator.SetTrigger("Move Y");
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
