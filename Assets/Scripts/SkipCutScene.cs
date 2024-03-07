using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SkipCutScene : MonoBehaviour
{
    [SerializeField] private PlayerControls playerControls = null;
    public GameObject toEnable;
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.started += SkipScene;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void SkipScene(InputAction.CallbackContext context)
    {
        Debug.Log("wnreo");
        toEnable.SetActive(true);
    }
}
