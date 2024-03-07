using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letrero : MonoBehaviour
{
	public GameObject dialogBox;
	public TMP_Text dialogText;
	public string dialogo;
	private bool dialogActive;
	public PlayerController playerController = null;
	public bool leido = false;
	private AudioSource readAudio;

    private void Start()
    {
		readAudio = this.GetComponent<AudioSource>();
    }
    void Update()
	{
		if(playerController != null)
		{
			if (dialogActive) { dialogBox.SetActive(playerController.GetIsInteracting()); 
			if (playerController.GetIsInteracting()) { Debug.Log("Leeyo el texto"); leido = true; } }
			if (!dialogBox.active) { readAudio.Play(); }
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerController = collision.transform.parent.GetComponent<PlayerController>();
			Debug.Log("Entro");
			dialogText.text = dialogo;
			dialogActive = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerController.SetIsInteracting(false);
			Debug.Log("Salio");
			dialogActive = false;
			dialogBox.SetActive(false);
		}
	}
}
