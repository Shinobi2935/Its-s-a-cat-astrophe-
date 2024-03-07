using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    private void OnEnable()
    {
        FindObjectOfType<GameManager>().MainMenu();
    }
}
