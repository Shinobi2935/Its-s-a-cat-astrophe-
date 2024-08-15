using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public AudioClip attackAudio;
    [SerializeField] public AudioClip pauseAudio;
    [SerializeField] public AudioClip walkAudio;
    [SerializeField] public AudioClip runAudio;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private PlayerControls playerControls = null;
    [SerializeField] private PlayerStats playerStats = null;
    [SerializeField] private Vector2 moveVector = Vector2.zero;
    [SerializeField] private bool isRunning = false;
    [SerializeField] private bool isRecovering = false;
    [SerializeField] private bool isInteracting = false;
    [SerializeField] private bool isInventoryShown = false;
    [SerializeField] private bool isPaused = false;
    [SerializeField] private GameManager gamemanager = null;

    // Start is called before the first frame update
    void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        playerAudio = GetComponent<AudioSource>();
        gamemanager = FindObjectOfType<GameManager>();
        if (gamemanager.IsPaused())
        {
            gamemanager.PauseUnpauseGame();
        }
        isPaused = false;
        playerControls = new PlayerControls();
        playerControls.Player.Move.performed += OnMovePerformed;
        playerControls.Player.Move.canceled += OnMoveCanceled;
        playerControls.Player.Fire.started += OnFireStarted;
        playerControls.Player.Interact.started += OnInteractStarted;
        playerControls.Player.Inventory.started += OnInventoryShowPerformed;
        playerControls.Player.Run.performed += OnRunPerformed;
        playerControls.Player.Run.canceled += OnRunCanceled;
        playerControls.Player.Pause.started += OnPause;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isPaused);
        //Debug.Log(playerAnimator);
    }
    // FixedUpdate is called per clock cicle
    void FixedUpdate()
    {
        if(isRunning && playerStats.currentTimeSprint > 0.0f && (!isRecovering))
        {
            playerRigidbody.velocity = moveVector *  playerStats.maxSprint;
            playerStats.LossTimeSprint(400 * Time.deltaTime);
            if(playerStats.currentTimeSprint <= 0.0f)
            {
                isRunning = false;
                isRecovering = true;
            }
        }
        else
        {
            playerRigidbody.velocity = moveVector * playerStats.moveSpeed;
            if(playerStats.currentTimeSprint < playerStats.maxTimeSprint)
            {
                playerStats.RecoverTimeSprint(10 * Time.deltaTime);
            }
            else
            {
                isRecovering = false;
            }
        }
    }
    public bool GetIsInteracting() { return isInteracting; }
    public void SetIsInteracting(bool inter) { isInteracting = inter; }
    //public bool GetIsInventoryShown() { return isInventoryShown; }
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
        if ( !gamemanager.IsPaused())
        {
            playerAnimator.SetBool("IsMove", true);
            playerAnimator.SetFloat("Horizontal", moveVector.x);
            playerAnimator.SetFloat("Vertical", moveVector.y);
            playerAudio.clip = walkAudio;
            playerAudio.Play();
        }
    }
    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        if (!isRunning && !isRecovering)
        {
            isRunning = true;
            playerAudio.clip = runAudio;
            playerAudio.Play();
        }
    }
    private void OnFireStarted(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        if ( !gamemanager.IsPaused())
        {
            playerAnimator.SetBool("IsMove", false);
            isRunning = false;
            playerAnimator.SetTrigger("Attack");
            playerAudio.clip = attackAudio;
            playerAudio.Play();
        }
    }
    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        isInteracting = (isInteracting)? false : true;
    }
    private void OnInventoryShowPerformed(InputAction.CallbackContext context)
    {
        //isInventoryShown = (isInventoryShown) ? false : true;

        FindObjectOfType<MenuScript>().ShowInventory();
        if (!isInventoryShown)
        {
            isInventoryShown = true;
        }
        else
        {
            isInventoryShown = false;
        }
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveVector = Vector2.zero;
        if ( !gamemanager.IsPaused())
        {
            playerAnimator.SetBool("IsMove", false);
        }
    }
    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        isRunning = false;
    }
    private void OnEnable()
    {
        playerControls.Player.Enable();
    }
    private void OnDisable()
    {
        playerControls.Player.Disable();
    }
    private void OnPause(InputAction.CallbackContext context)
    {
        isPaused = true;
        FindObjectOfType<MenuScript>().PauseGame();
        playerAudio.clip = pauseAudio;
        playerAudio.Play();
        Debug.Log("paused");
        //FindObjectOfType<MenuScript>().DisplayWinScreen();
    }

}
