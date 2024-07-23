using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPuddle : MonoBehaviour
{
    [SerializeField] private float multiplicador;
    private Vector2 normal;
    private static System.Random ran = new System.Random();

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        normal.x = ran.Next(0,10);
        normal.y = ran.Next(0,10);
        normal = normal.normalized * multiplicador;

        if (col.CompareTag("Player"))
        {
            Rigidbody2D col_Rigidbody = col.transform.parent.GetComponent<Rigidbody2D>();
            col_Rigidbody.AddForce(normal);
        }
    }
}
