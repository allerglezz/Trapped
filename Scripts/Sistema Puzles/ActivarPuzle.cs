using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 24/05/2024
Descripción: Script que controla la activación de los puzles.
*/

public class ActivarPuzle : BaseInteractuable
{
    public string Escena;
    private Animator anim;
    private BoxCollider collider;
    private SceneLoadTrigger escena;
    private bool abierto = false;
    [SerializeField] private bool necesita_llave;
    [SerializeField] int llave_requerida;
    public List<int> objetosNecesarios = new List<int>();
    public string mensajeError;
    public TiposSonido audioError;

    void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
        escena = GetComponent<SceneLoadTrigger>();
    }

    public override void OnInteract()
    {
        Debug.Log("interactuado");
        base.OnInteract();
        StartCoroutine(EsperarAnimacion());
    }

    IEnumerator EsperarAnimacion()
    {
        Debug.Log("escena");
        //si tiene un animador
        if (anim != null)
        {
            //si el puzle requiere una llave
            if (necesita_llave && InventarioManager.Instance.HasItem(llave_requerida) && PuzleController.Instance.PuzleAccesible(Escena))
            {
                anim.SetTrigger("Abierta");
                yield return new WaitForSeconds(1.5f); // Ajusta este tiempo seg�n la duraci�n de tu animaci�n
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //SceneManager.LoadScene(Escena);
                escena.IniciarPuzle();
            }
            //si el puzle ya esta completado
            else if (PuzleController.Instance.PuzleEstado(Escena).completado)
            {
                tooltipMessage = "Puzle ya completado";
            }
            //si el puzle necesita la llave
            else
            {
                tooltipMessage = "Llave requerida";
            }
        }
        //si necesita una serie de items para el puzle
        else if (objetosNecesarios.Count > 0)
        {
            bool recogidos = true;
            foreach (int objeto in objetosNecesarios)
            {
                if (!InventarioManager.Instance.HasItem(objeto))
                {
                    tooltipMessage = mensajeError;
                    recogidos = false;
                }
            }
            if (recogidos && PuzleController.Instance.PuzleAccesible(Escena))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //SceneManager.LoadScene(Escena);
                escena.IniciarPuzle();
            }
            //linea de voz de error si no tienes los objetos
            else if (!recogidos)
            {
                AudioManager.SonarSonido(audioError);
            }
            else if (PuzleController.Instance.PuzleEstado(Escena).completado)
            {
                tooltipMessage = "Puzle ya completado";
            }
        }
        else
        {
            if (PuzleController.Instance.PuzleAccesible(Escena))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                //SceneManager.LoadScene(Escena);
                escena.IniciarPuzle();
            }
            else if (PuzleController.Instance.PuzleEstado(Escena).completado)
            {
                tooltipMessage = "Puzle ya completado";
            }
            else
            {
                tooltipMessage = mensajeError;
            }
        }
    }

    public void LoadSave()
    {
        
    }
}
