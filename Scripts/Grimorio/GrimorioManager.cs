using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 20/04/2024
Descripción: Script que gestiona el menú de pausa y la interacción con el Grimorio.
*/

public class GrimorioManager : MonoBehaviour
{
    //guardamos los gameobjects del menu desde unity
    [SerializeField] GameObject Menu;
    [SerializeField] GameObject Jugador;
    [SerializeField] GameObject Indicaciones;
    [SerializeField] GameObject Inventario;
    [SerializeField] GameObject Ajustes;
    [SerializeField] GameObject Coleccionables;
    private PlayerMovement movement;
    [SerializeField] public static bool Pausado = false;

    public AudioManager audioManganer;
    //se inician todos en false, se guarda playerMovement y se bloque la camara
    public void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        Indicaciones.SetActive(false);
        Menu.SetActive(false);
        Coleccionables.SetActive(false);
        Inventario.SetActive(false);
        Ajustes.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    //en cada frame
    public void Update()
    {
        //al pulsar tab
        if (Input.GetKeyDown(KeyCode.Tab) && InventarioManager.Instance.HasItem(18))
        {
            //si no esta pausado se pause y viceversa
            if (Pausado == false)
            {
                PausarJuego();
            }
            else
            {
                ContinuarJuego();
            }
            //en playerMovement se bloquea la camara
            movement.setPause();
        }
    }
    //pausamos el tiempo, se activa menu e indicaciones y se bloque el cursor
    public void PausarJuego()
    {
        Time.timeScale = 0f;
        Pausado = true;
        Menu.SetActive(true);
        Indicaciones.SetActive(true);
        Coleccionables.SetActive(false);
        Inventario.SetActive(false);
        Ajustes.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //se reanuda el tiempo, desactivamos todos los menus y desbloqueamos el cursor
    public void ContinuarJuego()
    {
        Time.timeScale = 1f;
        Pausado = false;
        Indicaciones.SetActive(false);
        Coleccionables.SetActive(false);
        Inventario.SetActive(false);
        Ajustes.SetActive(false);
        Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
