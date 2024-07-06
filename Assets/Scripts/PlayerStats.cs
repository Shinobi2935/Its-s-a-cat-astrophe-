using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : CharacterStats
{
    public float moveSpeed = 10.0f;
    public float maxSprint = 15.0f;
    public float maxTimeSprint = 100.0f;
    public float currentTimeSprint {get; private set;}
    private static bool gameStarted = false;
    private MenuScript menuScript;
    private int maxUpdatedHealth = 500;
    private int maxUpdatedStamina = 500;
    private Animator playerAnimator;
    //private Animator sidekickAnimator;

    public override void Awake()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (gameStarted && data != null)
        {
            Debug.Log("Empezo PlayerStats");
            currentHealth = data.health;
            currentTimeSprint = data.stamina;
            maxHealth = data.maxHealth;
            maxTimeSprint = data.maxStamina;

            damage.baseValue = data.DamageValue;
            if (data.DamageModdifier != null)
            {
                for (int i = 0; i < data.DamageModdifier.Length; i++)
                {
                    damage.AddModifier(data.DamageModdifier[i]);
                }
            }

            armor.baseValue = data.ArmorValue;
            if (data.ArmorModdifier != null)
            {
                for (int i = 0; i < data.ArmorModdifier.Length; i++)
                {
                    armor.AddModifier(data.ArmorModdifier[i]);
                }
            }
        }
        else
        {
            SetMaxHealth();
            SetMaxTimeSprint();
            SavePlayer();
        }
        playerAnimator = GetComponent<Animator>();
        characterAudio = GetComponent<AudioSource>();
        //sidekickAnimator = transform.GetChild(4).GetChild(0).GetComponent<Animator>();
        //sidekickAnimator = transform.Find("conejo").GetComponent<Animator>();
        //Debug.Log(transform.GetChild(4).GetChild(0).GetComponent<Animator>());
        //Debug.Log(sidekickAnimator);

        menuScript = FindObjectOfType<MenuScript>();
        menuScript.SetMaxHealth(maxHealth);
        menuScript.SetMaxStamina((int)maxTimeSprint);
        menuScript.SetMaxHealthBar(maxUpdatedHealth);
        menuScript.SetMaxStaminaBar(maxUpdatedStamina);
        menuScript.SetHealthBar(maxHealth);
        menuScript.SetStaminaBar((int)maxTimeSprint);
    }
    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        menuScript.SetHealth(currentHealth);
        menuScript.SetStamina((int)currentTimeSprint);
    }

    public void SetMaxTimeSprint()
    {
        currentTimeSprint = maxTimeSprint;
    }

    public void LossTimeSprint(float loss)
    {
        currentTimeSprint -= loss;
    }

    public void RecoverTimeSprint(float recover)
    {
        currentTimeSprint += recover;
    }

    public override IEnumerator Die()
    {
        Debug.Log(transform.name + " died.");
        playerAnimator.SetTrigger("muerte");
        //sidekickAnimator.SetTrigger("muerte");
        
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameManager>().EndGame();
        maxHealth = 100;
        currentHealth = 100;
        maxTimeSprint = 100.0f;

        damage.baseValue = 10;
        damage.modifiers = new List<int>();
        armor.baseValue = 0;
        armor.modifiers = new List<int>();

        SaveSystem.SavePlayer(this);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    public void SetGameStarted(bool isStarted)
    {
        gameStarted = isStarted;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.health;
        currentTimeSprint = data.stamina;
    }
}
