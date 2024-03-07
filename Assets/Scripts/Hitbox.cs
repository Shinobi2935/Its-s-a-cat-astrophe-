using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Hitbox : MonoBehaviour
{
    public CharacterStats stats;
    private Animator enemyAnimator;
    
    // Start is called before the first frame update
    void Awake()
    {
        stats = transform.parent.GetComponent<CharacterStats>();
        enemyAnimator = transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && !transform.parent.CompareTag("Player"))
        {
            HitForce(col);

            PlayerStats ps = col.transform.parent.GetComponent<PlayerStats>();
            ps.TakeDamage(stats.damage.GetValue());
        }
        if((col.CompareTag("Enemy") || col.CompareTag("EnemyBullet")) && !transform.parent.CompareTag("Enemy"))
        {
            HitForce(col);

            CharacterStats es = col.GetComponent<CharacterStats>();
            Animator pa = col.transform.GetComponent<Animator>();
            es.TakeDamage(stats.damage.GetValue());
            pa.SetTrigger("Damage");
        }
        if((col.CompareTag("Boss") || col.CompareTag("EnemyBullet")) && !transform.parent.CompareTag("Enemy"))
        {
            HitForce(col);

            CharacterStats es = col.GetComponent<CharacterStats>();
            Animator pa = col.transform.GetComponent<Animator>();
            es.TakeDamage(stats.damage.GetValue());
            pa.SetTrigger("Damage");
        }
    }

    void HitForce(Collider2D col)
    {
        if(col.CompareTag("Player") && !transform.parent.CompareTag("Player"))
        {
            Rigidbody2D col_Rigidbody = col.transform.parent.GetComponent<Rigidbody2D>();
            Vector3 parentPosition = transform.parent.transform.position;
            Vector2 direction = (Vector2) (col.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockBack = direction * stats.knockBackForce.GetValue();
            col_Rigidbody.AddForce(knockBack);
        }
        if(col.CompareTag("Enemy") && !transform.parent.CompareTag("Enemy"))
        {
            Rigidbody2D col_Rigidbody = col.GetComponent<Rigidbody2D>();
            Vector3 parentPosition = transform.position;
            Vector2 direction = (Vector2) (col.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockBack = direction * stats.knockBackForce.GetValue();
            col_Rigidbody.AddForce(knockBack);
        }
    }
}
