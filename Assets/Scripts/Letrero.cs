using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letrero : MonoBehaviour
{
	[SerializeField] private GameObject dialogBox;
	[SerializeField] private TMP_Text dialogText;
	[SerializeField] private string dialogo;
	[SerializeField] private PlayerController playerController = null;
	[SerializeField] public bool leido = false;
	[SerializeField] private SpriteRenderer vSprite;
	private bool dialogActive = false;
	private AudioSource readAudio;

    private void Start()
    {
		readAudio = this.GetComponent<AudioSource>();
		vSprite.enabled = false;
    }
    void Update()
	{
		if(playerController != null)
		{
			if (dialogActive) { dialogBox.SetActive(playerController.GetIsInteracting()); 
			if (playerController.GetIsInteracting()) { Debug.Log("Leeyo el texto"); leido = true; } }
			if (!dialogBox.active) { readAudio.Play(); }
			vSprite.enabled = dialogActive;
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
