/*
Creado por: Adrián de la Serna
Fixeado por: Beatriz Aller
Fecha de creación: 27/05/2024
Descripción: Script que controla el resultado del puzle de la caja del zodiaco
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Playables;

public class PuzleCaja : MonoBehaviour
{
    static public PuzleCaja Instance; 
    public Animator anim; //LLama a la animacion de la caja
    public List<boton> zodiaco = new List<boton>(); //Lista de botones del zodiaco que edita desde el inspector
    public List<bool> solucion = new List<bool>(); //Lista de la solucion del puzle que edita desde el inspector
    int contadorFallos = 0; //Contador de fallos
    int contador = 12; 
    public TextMeshProUGUI texto;
    public PlayableDirector cinematica;
    public float auidoRepeticion = 400f;
    public Destruir llaveCofre;
    private float timer = 0;
    private AudioSource audioSource;

    void Awake()
    {
        timer = auidoRepeticion;
        audioSource = GetComponent<AudioSource>();
        Instance = this;
    }
    private void Start()
    {
        texto.text = " ";
    }

    void Update()
    {
        timer -= Time.deltaTime; // Reduce el temporizador por el tiempo transcurrido en cada frame

        if (timer <= 0f)
        {
            audioSource.Play(); // Reproduce el sonido
            timer = auidoRepeticion; // Reinicia el temporizador
        }
    }

    //Método que donde el puzle es correcto
    public bool Correcto()
    {
        for (int i = 0; i < zodiaco.Count; i++)
        {

            if (zodiaco[i].EstadoBoton != solucion[i])
            {
                Debug.Log("resultado" + i + zodiaco[i].EstadoBoton);
                return false;
            }
        }
        return true;
    }
    
    //Método para llamar al método que comprueba el resultado
    public void LlamarResultado()
    {
        StartCoroutine(CompruebaResultado());
    }

    //Método que comprueba si el puzle está completo
    IEnumerator CompruebaResultado()
    {
        //Si el puzle no está completo, se muestra un mensaje
        if (!Correcto())
        {
            anim.SetBool("Correcto", false);
            Debug.Log("Correcto: false");
            Debug.Log("contador=" + contadorFallos);
            //Comprueba cuantos fallos lleva antes de mostrar el mensaje
            if (contadorFallos == contador){ //
                
                switch (Random.Range(1, 5)) // Genera un numero entre 1 y 4
                {
                    case 1:
                        texto.text = "Podria echar un ojo por la sala a ver si encuentro algo.";
                        break;
                    case 2:
                        texto.text = "Igual hay algo que no veo estando aqui.";
                        break; 
                    case 3:
                        texto.text = "Si no encuentro lo necesario aqui deberia darme una vuelta.";
                        break;
                    case 4:
                        texto.text = "Seguro que hay algo fuera que me estoy perdiendo.";
                        break;
                }
                contadorFallos = 0;
                //Espera 5 segundos y borra el mensaje
                yield return new WaitForSeconds(5f);
                texto.text = " ";
            }
            //Si el puzle no está completo, se suma un fallo
            contadorFallos++;
        }
        else
        {
            //Si el puzle está completo se reproduce la animación
            anim.SetBool("Correcto", true);
            Debug.Log("Correcto: true");

            yield return new WaitForSeconds(4); //Espera 4 segundos
            //Reproduce la cinematica final del zodiaco
            cinematica.Play();
            //Espera a que termine la cinematica
            yield return new WaitForSeconds((float) cinematica.duration);
            //Llama al método que comprueba si el puzle está completado
            llaveCofre.OnInteract();
            PuzleController.Instance.PuzleCompletado("Zodiaco");
            //Carga la escena principal
            SceneManager.LoadScene("MainScene");
        }
    }
}
