using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SidekickFollow : MonoBehaviour
{
    public GameObject target;
    public float speed;

    public float distance;
    Animator animator;
    Vector2 movement = Vector2.zero;
    private PlayerControls playerControls = null;
    private Rigidbody2D playerRigidbody;
    private PlayerInput playerInput;
    private bool isInteracting = false;
    private GameManager gamemanager = null;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();
        gamemanager = FindObjectOfType<GameManager>();
        playerControls.Player.Move.performed += OnMovePerformed;
        playerControls.Player.Move.canceled += OnMoveCanceled;
        playerControls.Player.Interact.started += OnInteractStarted;
    }



    // Update is called once per frame
    void Update()
    {
        if( !gamemanager.IsPaused())
        {
            if( target != null )
                    {
                        distance = Vector2.Distance(transform.position, target.transform.position); 
                        Vector2 direction = target.transform.position;
                        direction.Normalize();
                    }

                    if (distance > 1.3)
                    {
                        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);


                    }
        }
        
    }
    public bool GetIsInteracting() { return isInteracting; }
    public void SetIsInteracting(bool inter) { isInteracting = inter; }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        if (!gamemanager.IsPaused())
        {
            animator.SetBool("IsMove", true);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
        if (!gamemanager.IsPaused())
        {
            animator.SetBool("IsMove", false);
        }
    }


    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        isInteracting = (isInteracting) ? false : true;


    }

    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }




}