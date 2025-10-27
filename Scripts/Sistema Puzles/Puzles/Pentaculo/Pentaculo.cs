using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 10/05/2024
Descripción: Script que controla el puzle del pentáculo.
*/

public class Pentaculo : MonoBehaviour
{
    public GameObject Objeto;
    public GameObject Hueco;
    //rango de error para soltar la ficha
    public float SoltarDistancia = 0.1f;
    public bool Bloqueado;

    private Vector3 PosicionInicial;
    [SerializeField] private Camera mainCamera;
    private Rigidbody spiritRigidbody;
    private bool moviendo;
    private Vector3 offset;
    private Plane movementPlane;
    //Listas de las fichas de madera y sus holders
    public List<Transform> fichas;
    public List<Transform> holders;
    //Linea de voz a reproducir al cojer la ficha
    public TiposSonido fichaSonido;
    public Destruir diario;
    public PlayableDirector animacion;

    void Start()
    {
        PosicionInicial = Objeto.transform.position;
        spiritRigidbody = Objeto.GetComponent<Rigidbody>();

        // Configurar el Rigidbody para que sea cinem�tico
        spiritRigidbody.isKinematic = true;
        moviendo = false;
    }

    void OnMouseDown()
    {
        if (!Bloqueado)
        {
            AudioManager.SonarSonido(fichaSonido);
            moviendo = true;

            // Establecer un plano paralelo al eje x-y en la posici�n actual del cubo
            movementPlane = new Plane(Vector3.up, Objeto.transform.position);

            Vector3 nuevaZ = Objeto.transform.position;
            nuevaZ.y += (float)0.075;
            Objeto.transform.position = nuevaZ;

            // Calcular el desplazamiento inicial entre el rat�n y el cubo
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                offset = Objeto.transform.position - hitPoint;
            }
        }
    }

    void OnMouseDrag()
    {
        if (moviendo && !Bloqueado)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            // Realizar un raycast hacia el plano paralelo al eje x-y
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                Objeto.transform.position = hitPoint + offset;
            }
        }
    }

    void OnMouseUp()
    {
        Soltar();
    }

    public void Soltar()
    {
        moviendo = false;

        float Distancia = Vector3.Distance(Objeto.transform.position, Hueco.transform.position);
        if (Distancia < SoltarDistancia)
        {
            Bloqueado = true;
            Objeto.transform.position = Hueco.transform.position;
            StartCoroutine(comprobarSolucion());
        }
        else
        {
            Objeto.transform.position = PosicionInicial;
        }
    }

    public IEnumerator comprobarSolucion()
    {
        bool completado = true;
        for(int i = 0; i < fichas.Count; i++)
        {
            if (fichas[i].position != holders[i].position)
            {
                completado = false;
                break;
            }
        }
        if (completado)
        {
            animacion.Play();
            yield return new WaitForSeconds((float)animacion.duration);
            diario.OnInteract();
            PuzleController.Instance.PuzleCompletado("Pentaculo");
            SceneManager.LoadScene("MainScene");
        }
    }
}