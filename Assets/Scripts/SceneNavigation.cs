using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    [SerializeField] private bool loadNextScene;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    //void Update()
    //{

    //}
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats ps = ps = col.transform.parent.GetComponent<PlayerStats>();
            if(InventoryManager.Instance.GetItem("Key") != null)
            { 
                ps.SavePlayer();
                ps.SetGameStarted(true);
                InventoryManager.Instance.Remove(InventoryManager.Instance.GetItem("Key"));
                if (loadNextScene) { SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 ); }
                else { SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex - 1 ); }
            }
        }
    }
}
