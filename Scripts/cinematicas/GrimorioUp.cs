using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

/*
Creado por: Beatriz Aller
Fecha de creación: 18/06/2024
Descripción: Script que controla la animación del grimorio
*/

public class GrimorioUp : BaseInteractuable
{
    public GameObject player;
    public PlayableDirector cinematica;
    public CinemachineVirtualCamera camaraCinematica;

    public override void OnInteract()
    {
        base.OnInteract();
        StartCoroutine(hacerCinematica());
    }

    public IEnumerator hacerCinematica()
    {
        player.GetComponent<PlayerMovement>().setPause();
        camaraCinematica.gameObject.SetActive(true);
        //espera transicion entre camaras
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Animator>().enabled = true;
        IsInteractable = false;
        cinematica.Play();
        yield return new WaitForSeconds((float)cinematica.duration);
        camaraCinematica.gameObject.SetActive(false);
        //espera transicion entre camaras
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerMovement>().setPause();
        InventarioManager.Instance.Add(item);
        Destroy(gameObject);
    }

    //Para cuando se estan cargando los datos guardados
    public void Loading()
    {
        Destroy(gameObject);
        InventarioManager.Instance.Add(item);
    }
}