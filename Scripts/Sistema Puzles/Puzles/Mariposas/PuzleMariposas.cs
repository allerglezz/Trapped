using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 17/05/2024
Descripción: Script que controla el puzle de las mariposas.
*/

public class PuzleMariposas : MonoBehaviour
{
    public GameObject Mariposa;
    public GameObject Hueco;
    //Listas de mariposas y holders
    public List<Transform> HuecoLista;
    public List<Transform> MariposasLista;
    //rango de error al colocar las mariposas
    public float SoltarDistancia = 0.1f;
    public bool Bloqueado;

    private Vector3 PosicionInicial;
    [SerializeField] private Camera mainCamera;
    private Rigidbody spiritRigidbody;
    private bool moviendo;
    private Vector3 offset;
    private Plane movementPlane;
    //animacion al completar el puzle
    public PlayableDirector animacionCompletado;
    public Animator camaraAnim;

    void Start()
    {
        PosicionInicial = Mariposa.transform.position;
        spiritRigidbody = Mariposa.GetComponent<Rigidbody>();

        // Configurar el Rigidbody para que sea cinem�tico
        spiritRigidbody.isKinematic = true;
        moviendo = false;
    }

    void OnMouseDown()
    {
        if (!Bloqueado)
        {
            moviendo = true;

            // Establecer un plano paralelo al eje x-y en la posici�n actual del cubo
            movementPlane = new Plane(Vector3.forward, Mariposa.transform.position);

            // Calcular el desplazamiento inicial entre el rat�n y el cubo
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Vector3 nuevaZ = Mariposa.transform.position;
            nuevaZ.z -= (float)0.1;
            Mariposa.transform.position = nuevaZ;

            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                offset = Mariposa.transform.position - hitPoint;
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
                Mariposa.transform.position = hitPoint + offset;
            }
        }
    }

    void OnMouseUp()
    {
        Soltar();
        comprobarSolucion();
    }

    public void Soltar()
    {
        moviendo = false;

        float Distancia = Vector3.Distance(Mariposa.transform.position, Hueco.transform.position);
        if (Distancia < SoltarDistancia)
        {
            Bloqueado = true;
            Mariposa.transform.position = Hueco.transform.position;
        }
        else
        {
            Mariposa.transform.position = PosicionInicial;
        }
    }

    public void comprobarSolucion()
    {
        bool completado = true;
        for(int i = 0; i < MariposasLista.Count; i++)
        {
            if (MariposasLista[i].position != HuecoLista[i].position)
            {
                completado = false;
                break;
            }
        }
        if (completado)
        {
            StartCoroutine(hacerCinematica());
        }
    }

    public IEnumerator hacerCinematica()
    {
        animacionCompletado.Play();
        camaraAnim.enabled = true;
        yield return new WaitForSeconds((float) animacionCompletado.duration);
        camaraAnim.enabled = false;
        PuzleController.Instance.PuzleCompletado("Mariposas");
        SceneManager.LoadScene("MainScene");
    }
}