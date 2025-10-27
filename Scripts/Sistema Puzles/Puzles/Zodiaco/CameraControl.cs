/*
Creado por: Adrián de la Serna
Fixeado por:
Fecha de creación: 27/05/2024
Descripción: Script que controla el control de la cámara dentro de la escena "Zodiaco" 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    public float horizontalGiro; // Variable que controla el giro de la cámara

    public Vector3 velocidadRotacion = new Vector3(0, 100, 0); // Variable que controla la velocidad de rotación de la cámara

    public CharacterController CameraController; // Variable que controla el CharacterController de la cámara

    void Start()
    {
        CameraController = GetComponent<CharacterController>();
    }

    //Por cada frame, se comprueba la rotación de la cámara y se actualiza
    void Update()
    {

        horizontalGiro = Input.GetAxis("Horizontal");

        transform.Rotate(velocidadRotacion * horizontalGiro * Time.deltaTime); //Rotación de la cámara
        
        //Si se pulsa la tecla E, se sale de la escena
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Salir a Main Scene");
            Salir();
        }
    }
    
    void Salir()
    {
        SceneManager.LoadScene("MainScene");
    }
}
