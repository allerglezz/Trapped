using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Interfaz base para los objetos interactuables.
*/

    public interface IInteractuable
    {
        bool IsInteractable { get; set;}

        string TooltipMessage { get; }

        void OnInteract();
    }

