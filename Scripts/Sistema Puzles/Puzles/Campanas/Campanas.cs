/*
Creado por: Adrián de la Serna
Fixeado por: Beatriz Aller
Fecha de creación: 22/05/2024
Descripción: Script que controla el comportamiento de las campanas del puzle de las campanas
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Playables;
using Cinemachine;

public class Campanas : BaseInteractuable // Hereda de BaseInteractuable
{
    bool EnTrigger = false;

    [Space, Header("Numero de las campanas")]
    // La creamos vacia para declarar en el inspector
    public int numCampana; //numero de la campana

    int contador = 0; //contador de las campanas

    [Space, Header("partes de la campana")]
    public Animator animatorCampana; // Animator de la campana

    // Obtener el componente BoxCollider de la campana
    public BoxCollider boxCollider;

    [Space, Header("Animator de las puertas")]
    public List<Animator> animatorPuertas = new List<Animator>(); // Lista de los animators de las puertas

    public GameObject player; 
    public PlayableDirector cinematica; 
    public CinemachineVirtualCamera camaraCinematica; // Camara de la cinematica
    public Animator camaraAnimator; // Animator de la camara


    public override void OnInteract()
    {
        base.OnInteract();

        Debug.Log("Interact con " + numCampana); //compruebo con que objeto interacciona

        animatorCampana.SetTrigger("TocarCampana"); //activa la animacion de la campana
        //si el puzle esta ya completado se notifica al jugador y no se hace nada
        if(PuzleController.Instance.PuzleEstado("Campanas").completado)
        {
            tooltipMessage = "Puzle ya completado";
            return;
        //si el puzle no es accesible no se hace nada
        } else if (!PuzleController.Instance.PuzleAccesible("Campanas"))
        {
            return;
        }
        //if (EnTrigger == true)
        //{
        Debug.Log("Interact con " + numCampana); //compruebo con que objeto interacciona
        //Debug.Log(contador);                   //compruebo a que numero se encuentra el contador  
        //Debug.Log(ordenCampanas[contador]);    //compruebo que valor deberia ser el siguiente en pulsar

        //action.Invoke();   //para invocar el sonido de la campana /particulas
    
        //animatorPalo.SetTrigger("TocarCampana");
        //si toca varias veces la misma campana no se reinicia el contador
        if(contador == numCampana)
        {
            return;
        }
        //Comprueba que la camapana que se toca es la correcta del orden establezido
        if (contador + 1 == numCampana)
        {
            Debug.Log("Correcto");
            contador += 1;
            if (contador == 4) { win(); }

        }
        else { contador = 0; }

        Debug.Log("Contador es " + contador);

        // Encontrar todos los objetos que tienen el script PuzleCampanas
        Campanas[] todosLosObjetos = FindObjectsOfType<Campanas>();

        // Cambiar la variable en cada uno de ellos
        foreach (Campanas campana in todosLosObjetos)
        {
            campana.contador = this.contador;
        }
    }

    //Cuando el jugador ha completado el orden correcto de las campanas
    private void win()
    {
        Debug.Log("Has ganado");

        // Encontrar todos los objetos que tienen el script PuzleCampanas
        Campanas[] todosLosObjetos = FindObjectsOfType<Campanas>();

        // Cambiar la variable en cada uno de ellos
        foreach (Campanas objeto in todosLosObjetos)
        {
            if (boxCollider != null)
            {
                // Desactivar el MeshCollider
                boxCollider.enabled = false;
            }
        }

        Debug.Log("completado");
        //Llamar al metodo de puzle completado
        PuzleController.Instance.PuzleCompletado("Campanas");
        //Llamar a la corrutina de la cinematica
        StartCoroutine(hacerCinematica());
        
    }
    public IEnumerator hacerCinematica()
    {
        player.GetComponent<PlayerMovement>().setPause(); //Pausar el movimiento del jugador
        camaraCinematica.gameObject.SetActive(true); //Activar la camara de la cinematica
        yield return new WaitForSeconds(2f); //Esperar 2 segundos
        camaraAnimator.enabled = true; //Activar el animator de la camara
        cinematica.Play(); //Reproducir la cinematica
        yield return new WaitForSeconds((float)cinematica.duration); //Esperar a que termine la cinematica
        camaraCinematica.gameObject.SetActive(false); //Desactivar la camara de la cinematica
        yield return new WaitForSeconds(2f); //Esperar 2 segundos
        player.GetComponent<PlayerMovement>().setPause(); //Reanudar el movimiento del jugador
    }
}