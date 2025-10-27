using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Script que guarda la información de la interacción.
*/

[CreateAssetMenu(fileName = "Interacion Data", menuName = "SistemaDeInteracion/InteracionData")]
public class InteraccionData : ScriptableObject
{
    private BaseInteractuable m_interactable;

    //se guarda el valor
    public BaseInteractuable Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    //se hace la interaccion
    public void Interact()
    {
        m_interactable.OnInteract();
        ResetData();
    }

    //compara dos variables
    public bool IsSameInteractable(BaseInteractuable _newInteractable) => m_interactable == _newInteractable;
    public bool IsEmpty() => m_interactable == null;
    public void ResetData() => m_interactable = null;
}
