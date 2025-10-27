using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 17/06/2024
Descripción: Script que controla la animación de la escena exterior
*/

public class animacionExterior : MonoBehaviour
{
    public PlayableDirector animacion;
    private SceneLoadTrigger escena;
    void Awake()
    {
        StartCoroutine(esperarAnimacion());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        escena = GetComponent<SceneLoadTrigger>();
    }

    public IEnumerator esperarAnimacion()
    {
        yield return new WaitForSeconds((float) animacion.duration);
        //yield return new WaitForSeconds(2);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        escena.IniciarPuzle();
    }
}
