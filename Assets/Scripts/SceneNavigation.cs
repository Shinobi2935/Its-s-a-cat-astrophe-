using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    [SerializeField] private GameObject vSprite;
    [SerializeField] private GameObject animSprite;
    [SerializeField] private bool loadNextScene;
    private PlayerController playerController = null;
    private Animator chestAnimator;
    private PlayerStats ps = null;
    private bool hasKey;

    void Start()
    {
        hasKey = false;
    }

    void Update()
    {
        if(playerController != null && playerController.GetIsInteracting() && hasKey)
        { 
            GoToNextLevel();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerController = col.transform.parent.GetComponent<PlayerController>();
            ps = col.transform.parent.GetComponent<PlayerStats>();
            if(InventoryManager.Instance.GetItem("Key") != null) { 
                hasKey = true;
                vSprite.SetActive(true);
            }
            else {
                animSprite.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
            playerController.SetIsInteracting(false);
            vSprite.SetActive(false);
            animSprite.SetActive(false);
        }
    }

    private void GoToNextLevel()
    {
        ps.SavePlayer();
        ps.SetGameStarted(true);
        InventoryManager.Instance.Remove(InventoryManager.Instance.GetItem("Key"));
        if (loadNextScene) { SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 ); }
        else { SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex - 1 ); }
    }
}
