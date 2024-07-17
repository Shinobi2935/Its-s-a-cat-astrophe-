using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]

public class SpiderWeeb : MonoBehaviour
{
    protected CharacterStats stats;
    private float maxS;
    private float moveS;

    public virtual void Start ()
	{
		stats = GetComponent<CharacterStats>();
	}

    // Update is called once per frame
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            maxS = ps.maxSprint;
            moveS = ps.moveSpeed;


        }
        
    }

    public virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            ps.maxSprint = maxS;
            moveS = ps.moveSpeed;
        }
        
    }
}
