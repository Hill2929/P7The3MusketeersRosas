using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public float changeTime = 3.0f;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    private GameManager gameManager;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                direction = -direction;
                timer = changeTime;
            }
        }
    }
    void FixedUpdate()
    {
        if (gameManager.isGameActive)
        {
            Vector2 position = rigidbody2D.position;
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            rigidbody2D.MovePosition(position);
        }
    }
}
