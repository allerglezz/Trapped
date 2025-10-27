using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 15/05/2024
Descripción: Script que controla el puzle del bonsai.
*/

public class Bonsai : MonoBehaviour
{
    public GameObject Frasco;
    public GameObject Hueco;
    public float distanciaMover;
    public float rotacionFrasco;
    public Vector3 rotacionOriginal;
    //listas de frascos y holders
    public List<Transform> HuecoLista;
    public List<Transform> FrascosLista;
    //rango de error al colocar el frasco en el hueco
    public float SoltarDistancia = 0.1f;
    public bool Bloqueado;

    private Vector3 PosicionInicial;
    [SerializeField] private Camera mainCamera;
    private Rigidbody spiritRigidbody;
    private bool moviendo;
    private Vector3 offset;
    private Plane movementPlane;
    public PlayableDirector cinematica;
    public Destruir item;

    void Start()
    {
        PosicionInicial = Frasco.transform.position;
        spiritRigidbody = Frasco.GetComponent<Rigidbody>();

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
            movementPlane = new Plane(Vector3.left, Frasco.transform.position);

            // Calcular el desplazamiento inicial entre el rat�n y el cubo
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            //Debido a un problema del modelo 3D en blender, se tuvo que crear una variable llamada distanciaMover.
            //Esto se debe a que poner nuevaX.x = -33.49f llevaba el objeto a x = -68.65f por algun motivo, cosa que solo ocurre con estos modelos
            Vector3 nuevaX = Frasco.transform.position;
            nuevaX.x -= distanciaMover;
            Frasco.transform.position = nuevaX;
            
            if (rotacionFrasco != 0)
            {
                Quaternion currentRotation = transform.rotation;
                Vector3 currentEulerAngles = currentRotation.eulerAngles;
                currentEulerAngles.x = rotacionFrasco;
                currentEulerAngles.y = -85.963f;
                currentEulerAngles.z = 358.358f;
                Quaternion newRotation = Quaternion.Euler(currentEulerAngles);
                transform.rotation = newRotation;
            }
            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                offset = Frasco.transform.position - hitPoint;
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
                Frasco.transform.position = hitPoint + offset;
            }
        }
    }

    void OnMouseUp()
    {
        Soltar();
        StartCoroutine(comprobarSolucion());
    }

    public void Soltar()
    {
        moviendo = false;

        float Distancia = Vector3.Distance(Frasco.transform.position, Hueco.transform.position);
        if (Distancia < SoltarDistancia)
        {
            Bloqueado = true;
            Frasco.transform.position = Hueco.transform.position;
        }
        else
        {
            Frasco.transform.position = PosicionInicial;
            //ver comentario linea 51
            if(rotacionFrasco != 0)
            {
                Quaternion currentRotation = transform.rotation;
                Vector3 currentEulerAngles = currentRotation.eulerAngles;
                currentEulerAngles = rotacionOriginal;
                Quaternion newRotation = Quaternion.Euler(currentEulerAngles);
                transform.rotation = newRotation;
            }
        }
    }

    public IEnumerator comprobarSolucion()
    {
        bool completado = true;
        for (int i = 0; i < FrascosLista.Count; i++)
        {
            if (FrascosLista[i].position != HuecoLista[i].position)
            {
                completado = false;
                break;
            }
        }
        if (completado)
        {
            yield return StartCoroutine(hacerAnimacion());
            PuzleController.Instance.PuzleCompletado("Bonsai");
            SceneManager.LoadScene("MainScene");
        }
    }

    public IEnumerator hacerAnimacion()
    {
        cinematica.Play();
        yield return new WaitForSeconds((float)cinematica.duration);
        item.OnInteract();
    }
}