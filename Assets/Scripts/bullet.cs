using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : Enemy
{
    public float speed = 10f;
    public float lifeTime = 3f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        if(col.CompareTag("Player")) { Destroy(gameObject); }
    }
}
