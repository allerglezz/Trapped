using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 23/05/2024
Descripción: Script que guarda la información de los puzles.
*/

[System.Serializable]
public class Puzle
{
    public string nombre;
    public bool completado;
    public Puzle(string _nombre, bool _completado)
    {
        nombre = _nombre;
        completado = _completado;
    }
}

