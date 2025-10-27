using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 24/05/2024
Descripción: Script que controla la carga de las recompensas de los puzles
*/

public class CompletarPuzle : MonoBehaviour
{
    [SerializeField] public string puzle;
    public Destruir[] objetos;
    public Animator[] animacion;
    public GameObject[] objetosActivar;
    public GameObject[] objetosDesactivar;
    public BoxCollider[] boxCollider;
    public Animacion[] interactuableAnim;
    public GameObject[] indicaciones;

    // Nueva bandera para indicar si el puzle está siendo cargado desde el sistema de guardado
    private bool isLoading = false;

    public void SetLoadingState(bool loading)
    {
        isLoading = loading;

        if (isLoading)
        {
            // Configurar el estado final de las animaciones
            foreach (Animator obj in animacion)
            {
                if (obj != null)
                {
                    obj.SetTrigger("EnTrigger");
                    //obj.Play("EstadoFinal", 0, 1f);
                }
            }

            // Configurar los objetos activados/desactivados
            foreach (GameObject obj in objetosActivar)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            foreach (GameObject obj in objetosDesactivar)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }

            // Configurar los colliders
            foreach (BoxCollider parar in boxCollider)
            {
                if (parar != null)
                {
                    parar.isTrigger = true;
                }
            }

            // Configurar las indicaciones
            foreach (GameObject indicacion in indicaciones)
            {
                if (indicacion != null)
                {
                    indicacion.SetActive(true);
                }
            }
        }
    }

    public void activarPuzle()
    {
        if (isLoading)
        {
            Debug.Log($"El puzle '{puzle}' está siendo cargado desde el sistema de guardado. Configurando estado final.");
            return; // Evitar ejecutar la lógica normal si está en modo de carga
        }

        // Lógica normal para activar el puzle
        foreach (Destruir obj in objetos)
        {
            if (obj != null)
            {
                obj.isLoading = true;
                obj.OnInteract();
            }
        }
        foreach (Animator obj in animacion)
        {
            if (obj != null)
            {
                obj.SetTrigger("EnTrigger");
            }
        }
        foreach (GameObject obj in objetosDesactivar)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        foreach (GameObject obj in objetosActivar)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
        foreach (BoxCollider parar in boxCollider)
        {
            if (parar != null)
            {
                parar.isTrigger = true;
            }
        }
        foreach (Animacion anim in interactuableAnim)
        {
            if (anim != null)
            {
                anim.IsInteractable = true;
            }
        }
        foreach (GameObject indicacion in indicaciones)
        {
            if (indicacion != null)
            {
                indicacion.SetActive(true);
            }
        }
    }
}