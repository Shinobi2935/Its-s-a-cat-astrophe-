using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignsRead : MonoBehaviour
{
    public Letrero[] letreros;
    private int letrerosLeidos;
    private bool todosLeídos = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        if (!todosLeídos)
        {
            for (int i = 0; i < letreros.Length; i++)
            {
                if (!letreros[i].leido)
                {
                    break;
                }
                letreros[i].leido = true;
                counter++;
            }
        }

        //Debug.Log("counter " + counter);
        if (counter == letreros.Length)
        {
            Debug.Log("Se desactivo");
            gameObject.SetActive(false);
        }
    }
}
