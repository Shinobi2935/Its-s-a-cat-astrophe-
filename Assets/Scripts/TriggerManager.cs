using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    private Animator playerAnimator;
    public AudioSource pickUpAudio;
    
    void Awake()
    {
        playerAnimator = transform.parent.GetComponent<Animator>();
        //pickUpAudio = GameObject.FindObjectOfType<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)

    {
        
       
        if (collision.tag == "CollectableItems")
        {
            playerAnimator.SetTrigger("Take");
            Debug.Log("se recogio el objeto");
            if (InventoryManager.Instance.globalItemCounter < 20)
            {
                ItemPickUp obj = collision.GetComponentInParent<ItemPickUp>();
                CollectableItem Item = obj.Item;
                obj.Pickup();
                pickUpAudio.Play();
            }
            else
            {
                collision.GetComponentInParent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CollectableItems")
        {
            collision.gameObject.GetComponentInParent<BoxCollider2D>().isTrigger = true;
        }
    }
} 
