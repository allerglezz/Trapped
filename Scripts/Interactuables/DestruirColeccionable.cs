using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 05/05/2024
Descripción: Script que destruye un coleccionable y lo añade al inventario tras interactuar con el
*/

public class DestruirColeccionable : BaseInteractuable
{
    [SerializeField] ActivarEnColeccionables inventario;
    //se sobrescribe onInteract de baseInteractuable para destruir el objeto
    public bool isLoading = false;
    public override void OnInteract()
    {
        base.OnInteract();
        ColeccionableManager.Instance.Add(coleccionable);
        inventario.visible(coleccionable);
        Destroy(gameObject);
        if(!isLoading)
        {
            AudioManager.SonarSonido(TiposSonido.RECOGERCOLECCIONABLES);
        }
    }
}
