using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
Creado por: Beatriz Aller
Fecha de creación: 05/05/2024
Descripción: Script que activa un botón y su icono asociado en la interfaz de colección de coleccionables.
*/

public class ActivarEnColeccionables : MonoBehaviour
{
    [SerializeField] Button boton;
    [SerializeField] Animacion cajon;

    public void visible(Coleccionable coleccionable)
    {
        Image coleccionableIcon = boton.transform.Find("ColeccionableIcon").GetComponent<Image>(); // Verifica que el nombre es correcto
        //desbloque el cajon si el coleccionable estaba dentro, ya que estara bloqueado en ese caso
        if (cajon != null)
        {
            cajon.tooltipMessage = "Abrir";
            Collider cajonCollider = cajon.GetComponent<BoxCollider>();
            cajonCollider.enabled = true;
        }

        if (coleccionableIcon != null)
        {
            boton.interactable = true;
            coleccionableIcon.enabled = true;
        }
    }
}
