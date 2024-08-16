using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeeb : MonoBehaviour
{
    [SerializeField] private int reduccion;
    private float maxS;
    private float moveS;

    // Update is called once per frame
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            maxS = ps.maxSprint;
            moveS = ps.moveSpeed;

            ps.maxSprint /= reduccion;
            ps.moveSpeed /= reduccion;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            ps.maxSprint = maxS;
            ps.moveSpeed = moveS;
        }
    }
}
