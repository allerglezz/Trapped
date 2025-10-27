using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 05/05/2024
Descripción: Script que gestiona la colección de coleccionables y su persistencia.
*/

public class ColeccionableManager : MonoBehaviour
{
    public static ColeccionableManager Instance;

    public List<Coleccionable> Coleccionables = new List<Coleccionable>();

    public Transform ColeccionableContent;

    public GameObject InventoryColeccionable;

    public List<DestruirColeccionable> Botones = new List<DestruirColeccionable>();

    private void Awake()
    {
        Botones = new List<DestruirColeccionable>(FindObjectsOfType<DestruirColeccionable>());
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Add(Coleccionable coleccionable)
    {
        if (coleccionable != null)
        {
            Debug.Log(coleccionable.id);
            //necesario para que no salte excepcion al c# no permitir introducir elementos en posiciones concretas en la lista, en lugar de en orden
            Coleccionables[coleccionable.id] = coleccionable;
            SaveManager.Instance.itemRecogido(coleccionable);
        }
    }

    //Metodo para el sistema de de cargado
    public void LoadData()
    {
        // Obtener la lista de IDs de coleccionables guardados desde el SaveManager
        List<string> coleccionablesGuardados = SaveManager.Instance.ObtenerItemsColeccionables();

        if (coleccionablesGuardados == null || coleccionablesGuardados.Count == 0)
        {
            Debug.LogWarning("No se encontraron coleccionables en el archivo de guardado.");
            return;
        }

        // Actualizar la lista de botones
        Botones = new List<DestruirColeccionable>(FindObjectsOfType<DestruirColeccionable>());

        foreach (DestruirColeccionable boton in Botones)
        {
            if (boton != null && boton.coleccionable != null)
            {
                // Verificar si el coleccionable asociado al botón está en la lista de guardado
                string id = boton.coleccionable.id.ToString();
                if (coleccionablesGuardados.Contains(id))
                {
                    // Marcar el botón como cargado y ejecutar su interacción
                    boton.isLoading = true;
                    boton.OnInteract();

                    // Agregar el coleccionable a la lista de coleccionables
                    Coleccionables[boton.coleccionable.id] = boton.coleccionable;
                }
            }
        }
    }

    //Metodo para el sistema de de guardado
    public void SaveData()
    {
        /* data.objColeccionables = new List<Coleccionable>(Coleccionables); */
    }
}