using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Creado por: Lucia Moreno
Descripción: Script que controla la duración de la intro del juego.
*/

public class IntroJuego : MonoBehaviour
{

    public float DuracionVideo = 11f;
    void Start()
    {
        StartCoroutine(Intro());
        
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(DuracionVideo);
        SceneManager.LoadScene("MenuInicio");
    }
}
