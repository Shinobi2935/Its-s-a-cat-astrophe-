using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [SerializeField] private float DropRange = 1.0f;
    private AudioSource chestAudio;
    private Animator chestAnimator;

    public virtual void Start ()
	{
        chestAnimator = GetComponent<Animator>();
        chestAudio = GetComponent<AudioSource>();
    }

    public void SpawnItems()
    {
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
            chestAnimator.SetTrigger("Opening");
            chestAudio.Play();
        }
    }
}
