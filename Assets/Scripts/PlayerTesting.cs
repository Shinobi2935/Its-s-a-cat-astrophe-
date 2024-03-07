using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTesting : MonoBehaviour
{
    public static PlayerTesting Instance;

    public int health;
    public int exp;

    public Text healthText;
    public Text expText;

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseHealth(int value)
    {
        health += value;
        healthText.text = $"HP:{health}";
    }
    public void IncreaseExp(int value)
    {
        exp += value;
        expText.text = $"EXP:{exp}";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
