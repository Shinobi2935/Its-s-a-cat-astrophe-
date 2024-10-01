using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public float stamina;
    public int maxHealth;
    public float maxStamina;
    public int DamageValue;
    public int[] DamageModdifier;
    public int ArmorValue;
    public int[] ArmorModdifier;

    public PlayerData()
    {
        health = 100;
        stamina = 100;

        maxHealth = 100;
        maxStamina = 100;
        
        DamageValue = 10;
        DamageModdifier = null;

        ArmorValue = 0;
        ArmorModdifier = null;
    }

    public PlayerData(PlayerStats playerStats)
    {
        health = playerStats.currentHealth;
        stamina = playerStats.currentTimeSprint;
        
        maxHealth = playerStats.maxHealth;
        maxStamina = playerStats.maxTimeSprint;

        DamageValue = playerStats.damage.baseValue;
        
        if(playerStats.damage.modifiers != null) {
            DamageModdifier = new int[playerStats.damage.modifiers.Count];
            for (int i = 0; i < playerStats.damage.modifiers.Count; i++)
                DamageModdifier[i] = playerStats.damage.modifiers[i];
        }

        ArmorValue = playerStats.armor.baseValue;
        if(playerStats.armor.modifiers != null) {
            ArmorModdifier = new int[playerStats.armor.modifiers.Count];
            for (int i = 0; i < playerStats.armor.modifiers.Count; i++)
                ArmorModdifier[i] = playerStats.armor.modifiers[i];
        }
    }
}
