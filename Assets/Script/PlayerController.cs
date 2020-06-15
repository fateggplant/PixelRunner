using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    Animator playerAnimator;
    Rigidbody2D rb;

    void Start() {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        move(1);
    }

    int direction = 0;
    bool doubleJumping = false;
    public void move(int direction) {
        this.direction = direction;
    }

    public void jump() {
        if (doubleJumping) {
            return;
        }

        Vector2 velocity = rb.velocity;
        velocity.y = jumpForce;
        rb.velocity = velocity;

        int jumpState = playerAnimator.GetInteger("JumpState");
        if (jumpState == 0 || jumpState == 1) {
            playerAnimator.SetInteger("JumpState", jumpState + 1);
            if (jumpState == 1) {
                doubleJumping = true;
            }
        } else if (jumpState == 3) {
            playerAnimator.SetInteger("JumpState", 2);
            doubleJumping = true;
        }

    }

    float movement;
    // Update is called once per frame
    void Update()
    {
        int jumpState = playerAnimator.GetInteger("JumpState");
        if (rb.velocity.y < 0 && jumpState > 0) {
            playerAnimator.SetInteger("JumpState", 3);
        }

        if (Input.GetKeyDown("up") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) {
            jump();
        }        
    }

    void FixedUpdate() {
        movement = direction * moveSpeed * Time.deltaTime;
        playerAnimator.SetFloat("Speed", Mathf.Abs(movement));

        // Update position
        Vector3 position = transform.position;
        position.x += movement;
        transform.position = position;

        // Update scale
        if (direction != 0) {
            Vector3 scale = transform.localScale;
            scale.x = direction * Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        playerAnimator.SetInteger("JumpState", 0);
        doubleJumping = false;
    }

}
