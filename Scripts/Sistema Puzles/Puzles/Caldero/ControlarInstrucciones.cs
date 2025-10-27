/*
Creado por: Lucía Moreno
Fixeado por: 
Fecha de creación: 13/05/2024
Descripción: Script que controla el canvas de las intrucciones (pista para poder completar el puzle)
*/
using UnityEngine;
using UnityEngine.UI;

public class ControlarInstrucciones : MonoBehaviour
{
    public GameObject canvas; 

    private bool verInstrucciones; //Variable que controla si se ven las instrucciones o no

    

    void Update()
    {
        //Al hacer clic con el raton, se comprueba si esta encima del objeto de instrucciones.
        if (Input.GetMouseButtonDown(0))
        {
            //Se emite un rayo desde la camara hasta la posicion del raton.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Si el rayo impacta en el collider del objeto con las instrucciones, se invoca a la funcion para abrir las instrucciones. 
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                //Al llamar a la funcion AbrirInstrucciones() desde Update(), se puede volver a ver las instrucciones siempre que el jugador quiera.
                AbrirInstrucciones();
            }
        }
    }

    //Método que activa el canvas de las instrucciones
    public void AbrirInstrucciones(){

        //Se establece que las instrucciones sean visibles y se activa el canvas.
        verInstrucciones = true;
        canvas.SetActive(verInstrucciones);
    }
}
