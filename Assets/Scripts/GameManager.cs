using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public static bool gameIsPaused = false;
    public static bool gameIsInventory = false;
    private void Awake()
    {
    }

    public bool PauseUnpauseGame()
    {
        if (!gameIsPaused)
        {
            gameIsPaused = true;
            Time.timeScale = 0;
        } else
        {
            gameIsPaused = false;
            Time.timeScale = 1;
        }
        return gameIsPaused;
    }
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            FindObjectOfType<MenuScript>().DisplayGameOverScreen();
            PauseUnpauseGame();
            Debug.Log("Game Over");
        }
    }
    public void WinGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            FindObjectOfType<MenuScript>().DisplayWinScreen();
            PauseUnpauseGame();
            Debug.Log("Game Won");
            StartCoroutine(WinWating(10));

        }
    }
    public void Restart()
    {
        gameHasEnded = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            Time.timeScale = 1;
        }
        //SaveSystem.ErasePlayer();
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        SaveSystem.ErasePlayer();
        Application.Quit();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Lvl01WorldBuilding");
    }
    public void PlayIntro()
    {
        SaveSystem.ErasePlayer();
        SceneManager.LoadScene("Intro");
    }

    IEnumerator WinWating(int wait)
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        yield return new WaitForSecondsRealtime(wait);
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene("MainMenu");
    }

    public bool IsPaused()
    {
        return gameIsPaused;
    }

    public bool IsInventoryShown()
    {
        return gameIsInventory;
    }

    public bool showUnshowInventory()
    {
        gameIsInventory = !gameIsInventory;
        return gameIsInventory;
    }
}
