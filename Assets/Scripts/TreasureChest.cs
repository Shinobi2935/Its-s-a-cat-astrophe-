using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject vSprite;
    [SerializeField] private GameObject animSprite;
    [SerializeField] private GameObject[] items;
    [SerializeField] private float DropRange = 1.0f;
    private PlayerController playerController = null;
    private AudioSource chestAudio;
    private Animator chestAnimator;

    private bool hasKey;

    public virtual void Start ()
	{
        chestAnimator = GetComponent<Animator>();
        chestAudio = GetComponent<AudioSource>();
        hasKey = false;
    }

    private void Update ()
    {
        if (playerController != null && playerController.GetIsInteracting() && hasKey)
        {
            chestAnimator.SetTrigger("Opening");
        }
    }

    public void SpawnItems()
    {
        chestAudio.Play();
        foreach (var item in items)
        {
            Instantiate(item, new Vector3(transform.position.x + Random.Range(-DropRange, DropRange), 
                    transform.position.y +  Random.Range(-DropRange, 0.0f), 0), Quaternion.identity);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerController = col.transform.parent.GetComponent<PlayerController>();
            
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
}
