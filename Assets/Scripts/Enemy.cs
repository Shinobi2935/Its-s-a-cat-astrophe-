using System.Collections;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : MonoBehaviour
{
    protected CharacterStats stats;

    public virtual void Start ()
	{
		stats = GetComponent<CharacterStats>();
	}
    
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
       /*if (collision.tag == "Enemy")
        {
            playerAnimator.SetTrigger("Damage");

        }*/
        if (col.CompareTag("Player"))
        {
            HitForce(col);

            /*PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            Animator pa = col.transform.parent.GetComponent<Animator>();
            Animator sa = col.transform.parent.transform.GetChild(4).GetChild(0).GetComponent<Animator>();
            ps.TakeDamage(stats.damage.GetValue());
            pa.SetTrigger("Damage");*/

            PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            Animator pa =  col.transform.parent.GetComponent<Animator>();
            ps.TakeDamage(stats.damage.GetValue());
            pa.SetTrigger("Damage");
        }
    }

    public void HitForce(Collider2D col)
    {
        //Debug.Log(col.transform.parent.GetComponent<Rigidbody2D>());
        Rigidbody2D col_Rigidbody = col.transform.parent.GetComponent<Rigidbody2D>();
        Vector3 parentPosition = transform.position;
        Vector2 direction = (Vector2) (col.gameObject.transform.position - parentPosition).normalized;
        Vector2 knockBack = direction * stats.knockBackForce.GetValue();
        col_Rigidbody.AddForce(knockBack);
    }
}
