using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterStats : MonoBehaviour
{
	public static CharacterStats Instance;
    [SerializeField] public AudioClip damageAudio;
    [SerializeField] public AudioClip deathAudio;
	public int maxHealth = 100;
    public int currentHealth;

    public Stat damage;
    public Stat armor;
	public Stat knockBackForce;
    protected Animator enemyAnimator;
    [SerializeField] protected AudioSource characterAudio;

    List<CollectableItem> indexNum = new List<CollectableItem>();

    public virtual void Awake()
    {
        SetMaxHealth();
        enemyAnimator = GetComponent<Animator>();
        characterAudio = GetComponent<AudioSource>();
    }
    
    public virtual void Start ()
	{
		Instance = this;
    }
    //private void Update()
    //{
    //    Keyboard kb = InputSystem.GetDevice<Keyboard>();
    //    if (kb.tKey.wasPressedThisFrame)
    //    {
    //        TakeDamage(10);
    //    }
    //}

    // Damage the character
    public virtual void TakeDamage (int damage)
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
            StartCoroutine(Die());
        }
	}

	// Heal the character.
	public void Heal (int amount)
	{
		currentHealth += amount;
        Debug.Log(transform.name + " cures " + amount + " damage.");
        currentHealth = (currentHealth >= maxHealth) ? maxHealth : currentHealth;
        Debug.Log(transform.name + " current health: " + currentHealth + ".");
    }
    public void StatUpgrade(int upgrade, Stat.StatsType stat)
    {
        switch(stat)
        {
            case Stat.StatsType.Armor:
                armor.AddModifier(upgrade);
                //Debug.Log(transform.name + " armor " + armor.GetValue() + " value.");
                break;
            case Stat.StatsType.Damage:
                damage.AddModifier(upgrade);
                //Debug.Log(transform.name + " damage " + damage.GetValue() + " value.");
                break;
        }
        FindObjectOfType<ViewPlayerStats>().UpdateStatsText();
    }

    public bool UpgradeStat(int upgradeValue, int objsRequired, CollectableItem.ItemType itemType, Stat.StatsType stat)
    {
        bool isStatUpgraded = false;
        int id = 0;
        List<CollectableItem> inventoryItems = FindObjectOfType<InventoryManager>().GetItems();
        foreach (CollectableItem item in inventoryItems)
        {
            if (item.itemType == itemType)
            {
                indexNum.Add(item);
            }
            id++;
        }
        Debug.Log(inventoryItems);
        if (objsRequired <= indexNum.Count)
        {
            Debug.Log("objsRequired " + objsRequired + " lest that totalitems " + indexNum.Count);
            foreach (CollectableItem item in indexNum)
            {
                Debug.Log("Element " + item + " Item");
            }
            int totalItemCount = inventoryItems.Count;
            int itemsPased = 0;
            for (int i = indexNum.Count - 1; i >= 0; i--)
            {
                if (itemsPased >= objsRequired)
                {
                    continue;
                }
                //Debug.Log("Is deleting " + i + " " + indexNum[i] + "Item");
                InventoryManager.Instance.Remove(indexNum[i]);

                itemsPased++;
            }

            StatUpgrade(upgradeValue, stat);
            InventoryManager.Instance.ListItems();
            isStatUpgraded = true;
        } else
        {
            Debug.Log(indexNum.Count + " No son suficientes");
        }
        indexNum.Clear();
        return isStatUpgraded;
    }

    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }

    public virtual IEnumerator Die()
    {
        Debug.Log(transform.name + " died.");
        enemyAnimator.SetTrigger("muerte");
        characterAudio.clip = deathAudio;
        characterAudio.Play();
        yield return new WaitForSeconds(1);
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }
}