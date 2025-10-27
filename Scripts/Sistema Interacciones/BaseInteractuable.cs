using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Script base para los objetos interactuables.
*/

public class BaseInteractuable : MonoBehaviour, IInteractuable
{
   public Item item;
   public Coleccionable coleccionable; 

   [SerializeField] private bool isInteractable = true;

   [SerializeField] public string tooltipMessage = "interact";

    // Implementación de la propiedad de la interfaz con get y set
   public bool IsInteractable
   {
      get => isInteractable;
      set => isInteractable = value;
   }

   public string TooltipMessage => tooltipMessage;

   public virtual void OnInteract()
   {
      Debug.Log("INTERACTED: " + gameObject.name);
   }
}
