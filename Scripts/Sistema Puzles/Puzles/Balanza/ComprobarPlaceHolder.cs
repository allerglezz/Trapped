using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Lucía Moreno
Fixeado por:
Fecha de creación: 17/05/2024
Descripción: Script que comprueba si el hueco está ocupado o no
*/

public class ComprobarPlaceHolder : MonoBehaviour
{

    public bool huecoOcupado = false; //Variable que indica si el hueco está ocupado o no

    //Metodo para comprobar si el hueco está ocupado
    public bool ComprobarHuecoLibre()
    {
        //Si el hueco no está ocupado, devuelve false
        if (!huecoOcupado) {
            return false;
        }
        //Si el hueco está ocupado, devuelve true
        else
        {
            return true;
        }
    }
}
