using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Lucía Moreno
Fixeado por: Beatriz Aller
Fecha de creación: 17/05/2024
Descripción: Script que controla el movimiento de las gemas
*/

public class ControlarMovimiento : MonoBehaviour
{
    public GameObject disco;
    public GameObject gema;
    public GameObject controlador;
    
    //place holders para la gema
    public GameObject gemaIzq;
    public GameObject gemaDer;

    public float SoltarDistancia = 0.1f; //Distancia a la que se debe soltar la gema para que se quede bloqueada
    public bool bloqueado;
    public bool huecoOcupado; //Booleano que activa los place holders 

    public int peso = 1; //Peso inicial de la gema

    private bool enMovimiento; //Booleano que indica si la gema se está moviendo
    private Vector3 posInicial;
    private Vector3 posRatonInicial; 
    private Vector3 posGemaInicial; 
    [SerializeField] private Camera mainCamera;
    private Plane movementPlane; 
    private Vector3 offset;

    void Start()
    {
        posInicial = transform.position;
        enMovimiento = false;
    }

    //Al hacer clic con el raton, se activa el movimiento de la gema
    void OnMouseDown()
    {
        //Si no esta bloqueado, se puede mover
        if (!bloqueado)
        {
            enMovimiento = true;
            posGemaInicial = transform.position;

            // Establecer un plano paralelo al eje x-y en la posicion actual del cubo
            movementPlane = new Plane(Vector3.forward, disco.transform.position);

            // Calcular el desplazamiento inicial entre el raton y el cubo
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                offset = gema.transform.position - hitPoint;
            }
        }
    }

    //Al arrastrar el raton, se mueve la gema
    void OnMouseDrag()
    {
        //Si esta en movimiento y no esta bloqueado, se puede mover
        if (enMovimiento && !bloqueado)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            // Realizar un raycast hacia el plano paralelo al eje x-y
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                gema.transform.position = hitPoint + offset;
            }
        }
    }

    //Al soltar el raton, se suelta la gema
    void OnMouseUp()
    {
        Soltar();
    }

    //Método que comprueba si la posicion es valida 

    public void Soltar()
    {
        enMovimiento = false;

        //Calcula si se ha soltado el objeto a una distancia v�lida del plato de la balanza.
        float distancia = Vector3.Distance(transform.position, disco.transform.position);

        //Si la distancia es v�lida, el objeto se queda bloqueado.
        if (distancia < SoltarDistancia)
        {
            bloqueado = true;
            //Comprueba si hay un hueco libre en el disco
            huecoOcupado = disco.GetComponent<ComprobarPlaceHolder>().ComprobarHuecoLibre();
            //Si hay un hueco libre, se activa el place holder izquierdo y se desactiva la gema
            if(!huecoOcupado)
            {
                gema.SetActive(false);
                gemaIzq.SetActive(true);
                disco.GetComponent<ComprobarPlaceHolder>().huecoOcupado = true;

            }
            //Si no hay un hueco libre, se activa el place holder derecho y se desactiva la gema
            else
            {
                gema.SetActive(false);
                gemaDer.SetActive(true);
            }
            controlador.GetComponent<ControlPuzle>().numGemasEnDisco++;
            controlador.GetComponent<ControlPuzle>().SumarPesos(peso);
            
        }
        else
        {
            //Si la distancia no es v�lida, el objeto vuelve a su posici�n inicial.
            transform.position = posInicial;
        }
    }

    //Si el resultado no es correcto, se reiniza el puzle
    public void ReiniciarPuzle()
    {
        gemaIzq.SetActive(false); //Desactiva el place holder izquierdo
        gemaDer.SetActive(false); //Desactiva el place holder derecho
        gema.transform.position = posInicial; //Reinicia la posicion de la gema
        bloqueado = false; //Desbloquea la gema
        gema.SetActive(true); //Activa la gema
        disco.GetComponent<ComprobarPlaceHolder>().huecoOcupado = false; //Desactiva el hueco ocupado
    }
}
