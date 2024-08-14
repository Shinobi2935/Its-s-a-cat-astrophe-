using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NavegadorDialogos : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private string[] dialogo;

    private int index;

    private void Start()
    {
        index = 0;
        dialogText.text = dialogo[0];
    }

    public void NextDialogue()
    {
        if (index < (dialogo.Length - 1)) { index++; }
        dialogText.text = dialogo[index];
    }

    public void PreviousDialogue()
    {
        if(index > 0) { index--; }
        dialogText.text = dialogo[index];
    }

    public void AssignDialogue(string[] d)
    {
        dialogo = d;
    }

    public void ResetIndex()
    {
        index = 0;
        dialogText.text = dialogo[0];
    }
}
