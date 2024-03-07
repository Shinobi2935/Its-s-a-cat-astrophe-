using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStats : CharacterStats
{
   public override void TakeDamage (int damage)
	{
		// Subtract the armor value - Make sure damage doesn't go below 0.

		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		// Subtract damage from health
		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");
        Debug.Log(damageAudio);
        characterAudio.clip = damageAudio;
        characterAudio.Play();
        
		// If we hit 0. Die.

		if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
	}
}
