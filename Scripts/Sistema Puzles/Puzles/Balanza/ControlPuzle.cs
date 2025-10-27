using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Creado por: Lucía Moreno
Fixeado por: Beatriz Aller
Fecha de creación: 17/05/2024
Descripción: Script que controla la logica del puzle
*/
public class ControlPuzle : MonoBehaviour
{
    public Destruir obsidiana;
    private int pesoTotal = 0; //Peso total de las gemas en el disco
    public Animator animator;
    public int pesoCorrecto = 6; //Peso correcto para completar el puzle
    public int numGemasEnDisco; //Numero de gemas en el disco
    private ControlarMovimiento[] gemas; //Array para llamar a todas las gemas para reiniciarlas

    public void Start()
    {
        gemas = FindObjectsOfType<ControlarMovimiento>();
    }

    //Metodo para sumar el peso de las gemas en el disco
    public void SumarPesos(int peso)
    {  
        pesoTotal += peso; //Suma el peso de la gema al peso total
        Debug.Log("Peso total en el disco: " + pesoTotal);
        Debug.Log("gemas en disco = " + numGemasEnDisco);
        //Si hay dos gemas en el disco, se comprueba si la solucion es correcta
        if (numGemasEnDisco==2)
        {
            DecidirAnimacion(); //Decide que animacion se debe reproducir en base al peso
            StartCoroutine(ComprobarSolucion(pesoTotal)); //Comprueba si la solucion es correcta
            
        }
    }

    //Metodo para pasarle el peso a la variable del animator para que decida que animacion reproducir
        public void DecidirAnimacion()
    {
        animator.SetInteger("PesoTotal", pesoTotal); //Pasa el peso total al animator
    }

    //Metodo para comprobar si la solucion es correcta
    public IEnumerator ComprobarSolucion(int pesoTotal)
    {
        Debug.Log("se comprueba");
        yield return new WaitForSeconds(3f); //Espera 3 segundos para que se reproduzca la animacion

        //Si el peso total es correcto, se completa el puzle
        if (pesoTotal == pesoCorrecto)
        {
            Debug.Log("Puzle completo");
            obsidiana.OnInteract();
            PuzleController.Instance.PuzleCompletado("Balanza"); //Se completa el puzle
            SceneManager.LoadScene("MainScene"); //cargar la escena correcta
        }
        else
        {
            Debug.Log("No es correcto");
            Reiniciar();

        }
    }

    //Metodo para reiniciar el puzle
    public void Reiniciar()
    {
        pesoTotal = 0; //Reinicia el peso total
        numGemasEnDisco = 0; //Reinicia el numero de gemas en el disco
        animator.SetInteger("PesoTotal", pesoTotal); //Reinicia el animator
        //Reinicia cada una de las gemas
        foreach (var gema in gemas)
        {
            gema.ReiniciarPuzle(); //Reinicia la gema
        }
    }
}
