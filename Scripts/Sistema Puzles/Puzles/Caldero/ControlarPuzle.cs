/*
Creado por: Lucía Moreno
Fixeado por: 
Fecha de creación: 13/05/2024
Descripción: Script que controla la logica del puzle DESACARTADO
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlarPuzle : MonoBehaviour
{
    public List<GameObject> ordenCorrecto; //Lista de los ingredientes en el orden correcto

    private List<GameObject> ordenIngredientes = new List<GameObject>();

    private bool correcto;

    public Destruir veneno;

    public void AnadirIngrediente(GameObject ingrediente)
    {
        ordenIngredientes.Add(ingrediente);
        ComprobarOrden();
    }

    private void ComprobarOrden()
    {
        if (ordenIngredientes.Count != ordenCorrecto.Count)
        {
            return;

        }
        for (int i = 0; i < ordenIngredientes.Count; i++)
        {
            if (ordenIngredientes[i] != ordenCorrecto[i])
            {
                correcto = false;
                Debug.Log("Orden incorrecto");
            }
            if (ordenIngredientes[i] == ordenCorrecto[i])
            {
                correcto = true;
            }
        }

        if (correcto)
        {
            Debug.Log("puzle completado");
            veneno.OnInteract();
            PuzleController.Instance.PuzleCompletado("Caldero");
            SceneManager.LoadScene("MainScene");
        }
    }
}
