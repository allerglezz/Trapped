using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

/*
Creado por: Beatriz Aller
Fecha de creación: 12/06/2024
Descripción: Script que controla la interacción con los objetos de las brujas. Y sus respectivas cinematicas
*/

public class ObjetosBrujas : BaseInteractuable
{
    public int itemId;
    public GameObject objeto;
    public GameObject player;
    public PlayableDirector cinematica;
    public CinemachineVirtualCamera camaraCinematica;
    public Animator[] PuertaAnimator;

    public override void OnInteract()
    {
        base.OnInteract();
        if (InventarioManager.Instance.HasItem(itemId) && !objeto.activeSelf)
        {
            objeto.SetActive(true);
            StartCoroutine(hacerCinematica());

            // Guardar el estado del objeto colocado
            SaveManager.Instance.GuardarObjetoColocado(itemId, gameObject.name);
        }
    }

    public IEnumerator hacerCinematica()
    {
        player.GetComponent<PlayerMovement>().setPause();
        camaraCinematica.gameObject.SetActive(true);
        //transicion entre camaras
        yield return new WaitForSeconds(2f);
        cinematica.Play();
        foreach (Animator anim in PuertaAnimator)
        {
            anim.enabled = true;
        }
        yield return new WaitForSeconds((float)cinematica.duration);
        player.GetComponent<PlayerMovement>().setPause();
    }

    public void activarInteraccion()
    {
        IsInteractable = true;
    }
    //sistema de carga
    public void LoadData()
    {
        /* foreach (var balda in data.brujasBaldas)
        {
            if (balda.id == itemId && balda.state)
            {
                objeto.SetActive(true);
            }
        } */
    }

    public void SaveData()
    {
        /* // Asegurarse de que la lista tenga el tamaño correcto
        if (data.brujasBaldas.Count != 4)
        {
            data.brujasBaldas.Add(new bladasBrujasData(itemId, objeto.activeSelf));
        }

        for (int i = 0; i < data.brujasBaldas.Count; i++)
        {
            var balda = data.brujasBaldas[i];

            // Comprobar si el número ya está en la lista
            if (balda.id == itemId)
            {
                data.brujasBaldas[i].state = objeto.activeSelf;
                break;
            }
        } */
    }
}
