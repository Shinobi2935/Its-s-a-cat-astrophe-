using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<GameManager>().PlayGame();
    }
}
