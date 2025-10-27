using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 12/06/2024
Descripción: Script que controla la interacción con las placas de las brujas.
*/

public class placasBrujas : BaseInteractuable
{
    public GameObject canvas;
    public PlayerMovement player;

    public override void OnInteract()
    {
        base.OnInteract();
        player.setPause();
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvas.SetActive(true);
    }

    public void activarInteraccion()
    {
        IsInteractable = true;
    }

    public void disableCanvas()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        player.setPause();
        canvas.SetActive(false);
    }
}
