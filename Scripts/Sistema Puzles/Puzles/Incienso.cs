using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 13/06/2024
Descripción: Script que controla la interaccion y aparicion del objeto incienso
*/

public class Incienso : BaseInteractuable
{
    //objetos necesarios para que aparezca el incienso
    public List<int> objetoNecesarios = new List<int>();
    private Destruir destruir = null;
    public GameObject incienso;
    public Destruir cuenco;

    //al interactuar se comprueba si se tiene los items necsarios, en caso afirmativo el incienso aparece
    public override void OnInteract()
    {
        destruir = GetComponent<Destruir>();
        if (destruir != null )
        {
            destruir.OnInteract();
        }
        bool activar = true;
        Debug.Log("incienso apareciendo");
        foreach (int i in objetoNecesarios)
        {
            if (!InventarioManager.Instance.HasItem(i))
            {
                activar = false;
                break;
            }
        }
        if (activar)
        {
            cuenco.IsInteractable = true;
            incienso.SetActive(true);
        }
    }

    //sistema de carga
    public void loading()
    {
        bool activar = true;
        foreach (int i in objetoNecesarios)
        {
            if (!InventarioManager.Instance.HasItem(i))
            {
                activar = false;
                break;
            }
        }
        if (activar)
        {
            cuenco.isLoading = true;
            cuenco.OnInteract();
        }
    }
}
