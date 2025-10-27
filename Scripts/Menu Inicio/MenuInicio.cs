using UnityEngine;
using UnityEngine.SceneManagement;

/*
Creado por: Lucia Moreno
Fecha de creación: 15/04/2024
Descripción: Script que controla el menú de inicio.
*/

public class MenuInicio : MonoBehaviour
{
    [Header("Cargar Escenas")]
    [SerializeField] private SceneField _juegoprincipal;


    public GameObject CargarPartidaCanvas;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void NuevaPartida()
    {
        SaveManager.Instance.crearGuardado();
        SaveManager.Instance.CrearGuardadoTemporal();
        SceneManager.LoadSceneAsync(_juegoprincipal);
    }

    public void CargarPartida()
    {
        if (SaveManager.Instance.HayGuardado())
        {
            SceneManager.LoadSceneAsync(_juegoprincipal);
        }
        else
        {
            CargarPartidaCanvas.SetActive(true);
        }
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}