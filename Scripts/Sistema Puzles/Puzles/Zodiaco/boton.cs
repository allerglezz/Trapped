using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Creado por: Adrián de la Serna
Fixeado por:
Fecha de creación: 27/05/2024
Descripción: Script que controla la interaccion con los botones el puzle
*/

public class boton : MonoBehaviour
{
    [SerializeField] public Button palanca; // Asigna el bot�n desde el inspector
    [SerializeField] public Renderer botonRender; // Asigna el renderer del objeto 3D cuyo material quieres cambiar
    //private Animator anim;

    //asigno los materiales de los botones
    [SerializeField] public Material encendido; // Asigna el nuevo material desde el inspector
    [SerializeField] public Material apagado; // Asigna el nuevo material desde el inspector

    [SerializeField] public bool EstadoBoton = false;


    void Start()
    {
        palanca = GetComponent<Button>();
        botonRender = GetComponent<Renderer>();
        //anim = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        Debug.Log("activado" + gameObject);
        if (botonRender != null && encendido != null && apagado != null)
        {
            EstadoBoton = !EstadoBoton;
            if (!EstadoBoton)
            {
                botonRender.material = apagado;
                //anim.SetBool("Estado", false);
            }
            else
            {
                botonRender.material = encendido;
                //anim.SetBool("Estado", true);
            }
        }

        PuzleCaja.Instance.LlamarResultado();
    }
}
