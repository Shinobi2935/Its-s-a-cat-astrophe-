using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letrero : MonoBehaviour
{
	[SerializeField] private GameObject dialogBox;
	[SerializeField] private NavegadorDialogos nDialogos;
	[SerializeField] private string[] dialogo;
	[SerializeField] public bool leido = false;
	[SerializeField] private SpriteRenderer vSprite;
	private PlayerController playerController = null;
	private AudioSource readAudio;
	private Animator signAnimator;

    private void Start()
    {
		readAudio = this.GetComponent<AudioSource>();
		signAnimator = this.GetComponent<Animator>();
		vSprite.enabled = false;
		leido = false;
    }
    void Update()
	{
		if(playerController != null && vSprite.isVisible)
		{
			signAnimator.SetBool("isOpen", playerController.GetIsInteracting());
		}
	}

	public void OpenSign()
	{
		dialogBox.SetActive(true);
		//Time.timeScale = 0;
		leido = true;
		readAudio.Play();
	}
	public void CloseSign()
	{
		dialogBox.SetActive(false);
		//Time.timeScale = 1;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerController = collision.transform.parent.GetComponent<PlayerController>();
			nDialogos.AssignDialogue(dialogo);
			nDialogos.ResetIndex();
			vSprite.enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			playerController.SetIsInteracting(false);
			dialogBox.SetActive(false);
			vSprite.enabled = false;
		}
	}
}
