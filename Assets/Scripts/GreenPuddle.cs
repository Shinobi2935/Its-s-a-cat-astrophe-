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
        if (col.CompareTag("Player"))
        {
            StartCoroutine(Slip(col));
        }
    }

    protected virtual IEnumerator Slip(Collider2D player)
    {
        normal.x = ran.Next(-10,10);
        normal.y = ran.Next(-10,10);
        normal = normal.normalized * multiplicador;
        Rigidbody2D col_Rigidbody = player.transform.parent.GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(0.1f);
        col_Rigidbody.AddForce(normal);
    }
}
