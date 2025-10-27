using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemControler : MonoBehaviour
{
    public Item Item;

    public void Recoger()
    {
        InventarioManager.Instance.Add(Item);
        SaveManager.Instance.itemRecogido(Item);
        this.gameObject.SetActive(false);
    }
}
