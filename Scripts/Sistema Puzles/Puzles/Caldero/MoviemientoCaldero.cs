/*
Creado por: Lucía Moreno
Fixeado por: 
Fecha de creación: 13/05/2024
Descripción: Script que controla el movimiento de los frascos en el puzle
*/
using UnityEngine;

public class MovimientoCaldero : MonoBehaviour
{
    public GameObject frasco; 
    public GameObject caldero; //Objeto que representa el lugar donde se sueltan los frascos
    public float SoltarDistancia;
    
    private Plane movementPlane;
    private Vector3 offset;
    private bool enMovimiento;
    private Vector3 posInicial;
    private Vector3 posFrascoInicial;
    [SerializeField] private Camera mainCamera;

    void Start()
    {
        posInicial = transform.position;
        enMovimiento = false;
    }

    //Al hacer clic con el raton, se activa el movimiento del frasco
    void OnMouseDown()
    {
            enMovimiento = true;
            posFrascoInicial = transform.position;

            // Establecer un plano paralelo al eje x-y en la posicion actual del cubo
            movementPlane = new Plane(Vector3.forward, caldero.transform.position);

            // Calcular el desplazamiento inicial mediante un rayo entre el raton y el cubo
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                offset = frasco.transform.position - hitPoint;
            }
        
    }

    //Al arrastrar el raton, el frasco se mueve
    void OnMouseDrag()
    {
        // Si el frasco esta en movimiento, se actualiza su posicion
        if (enMovimiento)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float distance;

            // Realizar un raycast hacia el plano paralelo al eje x-y
            if (movementPlane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                frasco.transform.position = hitPoint + offset;
            }
        }

    }

    //Al soltar el raton, se activa el método de soltar
    void OnMouseUp()
    {
        Soltar();
    }

    //Metodo que calcula si el frasco se ha soltado en la posición correcta
    public void Soltar()
    {
        //Finaliza el movimiento
        enMovimiento = false;

        //Calcula la distancia entre el punto objetivo y el objeto arrastrado, si es menor de la distancia de soltado, se acepta como valido y se llama a la funcion que calcula el orden.
        float distancia = Vector3.Distance(transform.position, caldero.transform.position);
        if (distancia < SoltarDistancia)
        {
            //Llama al controlador y agrega el objeto a la lista con los ingredientes utilizados.
            StartCoroutine(caldero.GetComponent<Caldero>().ComprobarOrden(frasco));
            
            //Devuelve el objeto a su posicion inicial
            transform.position = posInicial;
        }
        else
        {
            //Devuelve el objeto a su posicion inicial.
            transform.position = posInicial;
        }
    }
}
