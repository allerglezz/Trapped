using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
Creado por: Beatriz Aller
Fecha de creación: 09/06/2024
Descripción: Script que gestiona el menú de inicio.
En desuso. Sustituido por MenuInicio
*/

public class MenuInicioManager : MonoBehaviour
{
    [Header("Cargar Escenas")]
    [SerializeField] private SceneField _juegoprincipal;

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(_juegoprincipal);
    }
}
