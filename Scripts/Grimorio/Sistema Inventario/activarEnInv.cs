using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
Creado por: Beatriz Aller
Fecha de creación: 23/04/2024
Descripción: Script que activa el objeto en el inventario.
*/

public class activarEnInv : MonoBehaviour
{
    [SerializeField] Button boton;
    [SerializeField] Animacion cajon;
    [SerializeField] ActivarPuzle puzle;
    [SerializeField] List<Animacion> objAbrir;

    [SerializeField] MultipleObjeto multiple;

    public void visible()
    {
        if (boton == null)
        {
            return;
        }
        TextMeshProUGUI itemName = boton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>(); // Verifica que el nombre es correcto
        Image itemIcon = boton.transform.Find("ItemIcon").GetComponent<Image>(); // Verifica que el nombre es correcto
        Image interrogationImage = boton.transform.Find("Interrogacion").GetComponent<Image>(); // Verifica que el nombre es correcto

        if (interrogationImage != null)
            interrogationImage.enabled = false;
        if (itemIcon != null)
            itemName.enabled = true;
        //Para desbloquear el cajon que lo contiene
        if (cajon != null)
        {
            cajon.tooltipMessage = "Abrir";
            Collider cajonCollider = cajon.GetComponent<Collider>();
            cajonCollider.enabled = true;
        }
        else if (puzle != null)
        {
            puzle.tooltipMessage = "Puzle Panel";
        }
        boton.interactable = true;
        if(objAbrir != null)
        {
            foreach(Animacion obj in objAbrir)
            {
                obj.tooltipMessage = "Abrir";
            }
        }
    }
}