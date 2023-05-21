using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 0.5f;

    public Rigidbody2D rb;
    public GameObject sc;
    public SceneChanger sceneChanger;
    public Animator animator;

    // public RandomEncounterController encounter;

    public PauseMenu pause;
    Vector2 movement;
    
    void Start()
    {
        sc = GameObject.FindWithTag("SceneChanger");
        sceneChanger = sc.GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneChanger.inBattle)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        } else if(!pause.isPaused)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

            if (movement.y > 0.01) {
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
            } else if (movement.y < -0.01) {
                animator.SetBool("Down", true);
                animator.SetBool("Up", false);
                animator.SetBool("Right", false);
                animator.SetBool("Left", false);
            } else if (movement.x > 0.01) {
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
            }
            else if (movement.x < -0.01) {
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
                animator.SetBool("Up", false);
                animator.SetBool("Down", false);
            }

            if (movement.magnitude > 1)
                animator.SetFloat("Speed", 1);
            else
                animator.SetFloat("Speed", movement.magnitude);
            }
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
