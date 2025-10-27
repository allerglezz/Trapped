using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 05/05/2024
Descripción: Script que define un coleccionable con un identificador y un icono asociado.
*/

[CreateAssetMenu(fileName = "Nuevo Coleccionable", menuName = "Coleccionable/Crear Nuevo Coleccionable")]
public class Coleccionable : ScriptableObject
{
    public int id;
    public Sprite icon;
}
