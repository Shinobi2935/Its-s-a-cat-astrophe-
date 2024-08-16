using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameWin;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject playerStats;
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider Health;
    [SerializeField] private Slider StaminaBar;
    [SerializeField] private Slider Stamina;

    private void Awake()
    {
		playerStats.SetActive(true);
    }

    public void DisplayGameOverScreen()
    {
        gameOver.SetActive(true);
        playerStats.SetActive(false);
        GameObject obj = GameObject.FindGameObjectWithTag("MainCamera");;
    }
    public void DisplayWinScreen()
    {
        gameWin.SetActive(true);
        FindObjectOfType<GameManager>().WinGame();
    }
    public void Restart()
    {
        FindObjectOfType<GameManager>().Restart();
    }
    public void MainMenu()
    {
        FindObjectOfType<GameManager>().MainMenu();
    }
    public void Quit()
    {
        FindObjectOfType<GameManager>().Quit();
    }
    public void SetMaxHealth(int health)
    {
        Health.maxValue = health;
        Health.value = health;
    }
    public void SetHealth(int health)
    {
        Health.value = health;
    }
    public void SetMaxStamina(int stamina)
    {
        Stamina.maxValue = stamina;
        Stamina.value = stamina;
    }
    public void SetStamina(int stamina)
    {
        Stamina.value = stamina;
    }

    public void SetMaxHealthBar(int width)
    {
        HealthBar.maxValue = width;
        HealthBar.value = width;
    }
    public void SetHealthBar(int width)
    {
        HealthBar.value = width;
    }
    public void SetMaxStaminaBar(int width)
    {
        StaminaBar.maxValue = width;
        StaminaBar.value = width;
    }
    public void SetStaminaBar(int width)
    {
        StaminaBar.value = width;
    }
    public void PlayGame()
    {
        FindObjectOfType<GameManager>().PlayGame();
    }
    public void PlayIntro()
    {
        FindObjectOfType<GameManager>().PlayIntro();
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(FindObjectOfType<GameManager>().PauseUnpauseGame());
    }
    public bool GameOverScreenIsActive()
    {
        return gameOver ? gameOver.activeSelf : false;
    }

    public bool WinGameScreenIsActive()
    {
        return gameWin ? gameWin.activeSelf : false;
    }
    public void ShowInventory()
    {
        Debug.Log("Entro al inventario");
        inventoryMenu.SetActive(FindObjectOfType<GameManager>().showUnshowInventory());
        //inventoryMenu.SetActive(true);
        InventoryManager.Instance.ListItems();
    }
}
