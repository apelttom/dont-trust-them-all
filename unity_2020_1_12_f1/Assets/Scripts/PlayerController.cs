using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rigidBody;
    private Vector2 movementVector;
    private Animator animator;

    void Start(){
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movementVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            movementVector = Vector2.up * moveSpeed;
        
        if (Input.GetKey(KeyCode.S))
            movementVector = Vector2.down * moveSpeed;
        
        if (Input.GetKey(KeyCode.A))
            movementVector = Vector2.left * moveSpeed;

        if (Input.GetKey(KeyCode.D))
            movementVector = Vector2.right * moveSpeed;
    }

    void FixedUpdate(){
        if(movementVector != Vector2.zero) {
            animator.SetBool("isRunning", true);
            animator.SetFloat("inputX", movementVector.x);
            animator.SetFloat("inputY", movementVector.y);
        }
        else {
            animator.SetBool("isRunning", false);
        }
        rigidBody.MovePosition(rigidBody.position + movementVector * Time.fixedDeltaTime);
    }
}
