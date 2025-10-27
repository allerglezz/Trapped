using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 15/05/2024
Descripción: Script que activa el canvas de la polaroid.
*/

public class Polaroid : MonoBehaviour
{
    public GameObject canvas;
    public GameObject boton;

    public void OnMouseDown()
    {
        canvas.SetActive(true);
        boton.SetActive(true);
    }
}
