using UnityEngine;
using UnityEngine.UI;

/*
Creado por: Beatriz Aller
Fecha de creación: 20/05/2024
Descripción: Script que controla el puzle de las mariposas.
*/

public class Panel : MonoBehaviour
{
    [SerializeField] public Button palanca; // Asigna el bot�n desde el inspector
    [SerializeField] public Renderer luzRender; // Asigna el renderer del objeto 3D cuyo material quieres cambiar
    [SerializeField] public Material encendido; // Asigna el nuevo material desde el inspector
    [SerializeField] public Material apagado; // Asigna el nuevo material desde el inspector
    private Animator anim; // Asigna el Animator del objeto que deseas animar
    [SerializeField] public bool luzEncendida;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnMouseDown()
    {
        Debug.Log("activado");
        if (luzRender != null && encendido != null && apagado != null)
        {
            luzEncendida = !luzEncendida;
            anim.SetBool("Bajar", luzEncendida);

            if (luzEncendida)
            {
                luzRender.material = apagado;
            }
            else
            {
                luzRender.material = encendido;
            }
        }
    }
}
