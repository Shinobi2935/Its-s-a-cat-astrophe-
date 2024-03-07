using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPlayerStats : MonoBehaviour
{
    public Text damage;
    public Text armor;

    public GameObject player;
    //public Text knockBackForce;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        UpdateStatsText();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateStatsText();
    }

    public void UpdateStatsText()
    {
        damage.text = player.GetComponent<PlayerStats>().damage.GetValue().ToString();
        //damage.text = FindObjectOfType<CharacterStats>().damage.GetValue().ToString();
        Debug.Log("damage " + damage.text);
        armor.text = player.GetComponent<PlayerStats>().armor.GetValue().ToString();
        //armor.text = FindObjectOfType<CharacterStats>().armor.GetValue().ToString();
        Debug.Log("armor " + armor.text);
        //knockBackForce.text = FindObjectOfType<CharacterStats>().knockBackForce.GetValue().ToString();
    }
}
