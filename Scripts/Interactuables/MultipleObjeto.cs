using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 12/05/2024
Descripción: Script que busca tener que coger un grupo de objetos para poder tener el item en el inventario
*/

public class MultipleObjeto : BaseInteractuable
{
    [SerializeField] activarEnInv inventario;
    public bool isLoading = false;
    public int contador = 0;
    public MultipleObjeto[] otrosItems;
    public TiposSonido recogidoPrimerItem;

    public override void OnInteract()
    {
        contador += 1;
        foreach (var item in otrosItems)
        {
            item.sumarContador();
        }
        if(contador==1){AudioManager.SonarSonido(recogidoPrimerItem);}
        if (contador == otrosItems.Count()+1){ destruir();} else {Destroy(gameObject);}
    }

    public void sumarContador()
    {
        contador += 1;
    }

    public void destruir()
    {
        base.OnInteract();
        InventarioManager.Instance.Add(item);
        SaveManager.Instance.itemRecogido(item);
        inventario.visible();
        Destroy(gameObject);
        if(!isLoading)
        {
            AudioManager.SonarSonido(TiposSonido.RECOGEROBJETOS);
        }
    }

    public void loading()
    {
        isLoading = true;
        if (SaveManager.Instance.ObtenerSaveData().itemsObtenibles.Contains(item.id.ToString()))
        {
            foreach (var item in otrosItems)
            {
                Destroy(item.gameObject);
            }
            inventario.visible();
            Destroy(gameObject);
            InventarioManager.Instance.Add(item);
        }
    }
}