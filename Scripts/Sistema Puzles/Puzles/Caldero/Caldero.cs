using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Lucía Moreno
Fixeado por: Beatriz Aller
Fecha de creación: 13/05/2024
Descripción: Script que compueba el orden de los frascos en el puzle del caldero.
*/

public class Caldero : MonoBehaviour
{
    //Lista con el orden correcto de los ingredientes.
    public List<GameObject> ordenCorrecto;
    public Destruir veneno;
    //Posici�n actual del �ndice.
    private int indexActual = 0;

    public ParticleSystem particulasBien;
    public ParticleSystem particulasMal;
    public ParticleSystem particulasCompleto;
    public PlayableDirector animacionFinal;

    //Un canvas dice al jugador que ha cometido un error.
    public Canvas canvas;

    //Se agrega un ingrediente a la lista de ingredientes
    public IEnumerator ComprobarOrden(GameObject ingrediente)
    {
        //Se comprueba si el ingrediente a�adido es el que corresponde a la posici�n actual del �ndice.
        if (ingrediente == ordenCorrecto[indexActual])
        {
            // Si el ingrediente es correcto, el �ndice avanza a la siguiente posici�n.
            indexActual++;

            // Si la posici�n del �ndice es igual al n�mero de elementos que componen la lista, el jugador ha completado el puzle.
            if (indexActual >= ordenCorrecto.Count)
            {
                particulasCompleto.Play();
                yield return new WaitForSeconds(1);
                animacionFinal.Play();
                yield return new WaitForSeconds((float) animacionFinal.duration);
                Debug.Log("Puzle completado");
                veneno.OnInteract();
                PuzleController.Instance.PuzleCompletado("Caldero");
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                particulasBien.Play(); //si el ingrediente es correcto se activan las particulas de bien
            }
        }
        else
        {
            particulasMal.Play(); //si el ingrediente es incorrecto se activan las particulas de mal
            indexActual = 0; //Si el ingrediente es incorrecto, el indice se reinicia 
        }
    }
}
