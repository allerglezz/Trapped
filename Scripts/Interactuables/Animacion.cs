using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 18/04/2024
Descripción: Script que gestiona la animación de un objeto interactuable.
*/

public class Animacion : BaseInteractuable
{
    private Animator anim;
    private BoxCollider collider;
    private bool abierto = false;
    [SerializeField] private bool necesita_llave;
    [SerializeField] int llave_requerida;

    [SerializeField] private Animator objDentro;
    [SerializeField] private string parametroDiario = "MOVIENDOSE"; // Nombre del par�metro en el Animator del diario
    [SerializeField] private string parametroCajon = "ABIERTA"; // Nombre del par�metro en el Animator del caj�n

    public string mensajeError;

    public string puzleNecsario;

    public void Start()
    {
        anim = GetComponent<Animator>();
        collider = GetComponent<BoxCollider>();
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (necesita_llave != false && !InventarioManager.Instance.HasItem(llave_requerida))
        {
            tooltipMessage = "Necesitas una llave";
        }
        else if(puzleNecsario != "" && !PuzleController.Instance.PuzleEstado(puzleNecsario).completado)
        {
            tooltipMessage = mensajeError;
        }
        else if (necesita_llave == false || InventarioManager.Instance.HasItem(llave_requerida))
        {
            abierto = !abierto; // Cambia el estado primero
            anim.SetBool(parametroCajon, abierto); // Actvar segun el nuevo estado

            if(objDentro != null)
            {
                objDentro.SetBool(parametroDiario, abierto);
                collider.enabled = false;
            }
        }
    }
}