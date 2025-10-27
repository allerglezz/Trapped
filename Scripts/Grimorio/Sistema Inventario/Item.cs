using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 19/04/2024
Descripción: Script que define un item.
Estado: en desuso, tras varias modificaciones en el sistema de inventario.
*/

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string ItemName;
    public Sprite icon;
    internal string itemName;
}
