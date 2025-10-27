using System;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Script que controla las interacciones del jugador con los objetos del escenario.
*/

public class ControladorDeInteracciones : MonoBehaviour
{
    //Los space header son para que se seperan en el inspectoer de unity
    [Space, Header("Data")]
    [SerializeField] private InteraccionInputData interactionInputData = null;
    [SerializeField] private InteraccionData interactionData = null;

    [Space, Header("UI")]
    [SerializeField] private InteraccionIUPanel uiPanel;

    [Space, Header("Ray Settings")]
    [SerializeField] private float rayDistance = 0f;
    [SerializeField] private float raySphereRadius = 0f;
    [SerializeField] private LayerMask interactableLayer = ~0;
    [SerializeField] private LayerMask obstacleLayer = ~0;

    [Space, Header("Contorno")]
    [SerializeField] private Material outlineMaterial;
    private BaseInteractuable ultimoitem;

    private Camera m_cam;

    private bool m_interacting;
     
    //guardamos la camara
    void Awake()
    {
        m_cam = FindObjectOfType<Camera>();
    }

    //en cada frame miramos si hay un interactuable en el ray y si se puede realizar la interaccion
    void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }
        
    void CheckForInteractable()
    {
        if (m_cam == null)
        {
            m_cam = FindObjectOfType<Camera>();
        }
        Ray _ray = new Ray(m_cam.transform.position, m_cam.transform.forward);
        RaycastHit _hitInfo;

        //mira si el rayo golpeo algo
        bool _hitSomething = Physics.SphereCast(_ray, raySphereRadius, out _hitInfo, rayDistance, interactableLayer | obstacleLayer);

        //si el rayo golpeo
        if (_hitSomething)
        {
            //obtenemos la base de interaccion del objeto golpeado
            BaseInteractuable _interactable = _hitInfo.transform.GetComponent<BaseInteractuable>();
            if (_interactable != null && _hitInfo.collider.gameObject.layer == 6 && _interactable.IsInteractable == true)
            {
                //si no hay datos previos de un interactuable
                if (interactionData.IsEmpty())
                {
                    //se guarda el interactuable
                    interactionData.Interactable = _interactable;
                    //se coloca el mensaje
                    uiPanel.SetTooltip(_interactable.TooltipMessage);
                }
                else
                {
                    //en caso de que el interactuable guardado sea el mismo al golpeado
                    if (!interactionData.IsSameInteractable(_interactable))
                    {
                        interactionData.Interactable = _interactable;
                        uiPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                }
                AplicarContorno(_interactable);
            }
            else
            {
                QuitarContorno();
                //reseteamos la interfaz
                uiPanel.ResetUI();
                //resetemos los datos de interaccion
                interactionData.ResetData();
            }
        }
        else
        {
            QuitarContorno();
            //reseteamos la interfaz
            uiPanel.ResetUI();
            //resetemos los datos de interaccion
            interactionData.ResetData();
        }

        //cambia el color del rayo si miramos un objeto
        DrawDebugSphereRay(_ray, raySphereRadius, rayDistance, _hitSomething ? Color.green : Color.red);
    }

    void AplicarContorno(BaseInteractuable _interacting)
    {
        if (ultimoitem != null && ultimoitem != _interacting)
        {
            QuitarContorno();
        }
        //si tiene hijos
        if (_interacting.transform.childCount > 0)
        {
            foreach (Renderer hijo in _interacting.GetComponentsInChildren<Renderer>())
            {
                Material[] materials = hijo.materials;
                if (materials[materials.Length-1].name != "outliner_mat (Instance)")
                {
                    Array.Resize(ref materials, materials.Length + 1); // Ampliar el arreglo para incluir el material de contorno
                    materials[materials.Length - 1] = outlineMaterial; // A�adir el material de contorno
                    hijo.materials = materials;
                }
            }
        }
       // no tiene hijos
        else
        {
            Material[] materials = _interacting.GetComponent<Renderer>().materials;
            if (materials[materials.Length-1].name != "outliner_mat (Instance)")
            {
                String nombre = materials[materials.Length-1].name;
                Array.Resize(ref materials, materials.Length + 1); // Ampliar el arreglo para incluir el material de contorno
                materials[materials.Length - 1] = outlineMaterial; // A�adir el material de contorno
            }
            _interacting.GetComponent<Renderer>().materials = materials;
        }
        ultimoitem = _interacting;
    }

    void QuitarContorno()//BaseInteractuable _interacting
    {
        if (ultimoitem != null && ultimoitem.transform.childCount == 0)
        {
            Material[] materials = ultimoitem.GetComponent<Renderer>().materials;
            Array.Resize(ref materials, materials.Length - 1); // Vuelve al material original
            ultimoitem.GetComponent<Renderer>().materials = materials;
            ultimoitem = null;
        }
        else if (ultimoitem != null)
        {
            foreach (Renderer hijo in ultimoitem.GetComponentsInChildren<Renderer>())
            {
                Material[] materials = hijo.materials;
                Array.Resize(ref materials, materials.Length - 1); // Vuelve al material original
                hijo.GetComponent<Renderer>().materials = materials;
            }
            ultimoitem = null;
        }
    }
    void DrawDebugSphereRay(Ray ray, float radius, float distance, Color color)
    {
        // L�nea principal del rayo
        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        // Visualizaci�n de los bordes de la esfera inicial y final
        Vector3 end = ray.origin + ray.direction * distance;

        // Dibuja c�rculos para el inicio y el final del SphereCast
        DrawWireCircle(end, radius, ray.direction, color);
    }

    void DrawWireCircle(Vector3 position, float radius, Vector3 direction, Color color)
    {
        int segments = 16;
        float angle = 360f / segments;

        Vector3[] points = new Vector3[segments + 1];
        Vector3 axis1 = Vector3.Cross(direction, Vector3.up).normalized * radius;
        Vector3 axis2 = Vector3.Cross(direction, axis1).normalized * radius;

        for (int i = 0; i <= segments; i++)
        {
            float rad = Mathf.Deg2Rad * angle * i;
            points[i] = position + Mathf.Cos(rad) * axis1 + Mathf.Sin(rad) * axis2;
        }

        for (int i = 0; i < segments; i++)
        {
            Debug.DrawLine(points[i], points[i + 1], color);
        }
    }

    void CheckForInteractableInput()
    {
        //si no hay datos para interactuar
        if (interactionData.IsEmpty())
        {
            return;
        }
        //si esta clickado
        if (interactionInputData.InteractedClicked)
        {
            m_interacting = true;
        }
        //si se libera el click
        if (interactionInputData.InteractedReleased)
        {
            m_interacting = false;
        }
        //si esta clickado
        if (m_interacting)
        {
            //si no es interactuable
            if (!interactionData.Interactable.IsInteractable)
            {
                return;
            }
            //en caso contrario
            else
            {
                //se realiza la interaccion
                interactionData.Interact();
                m_interacting = false;
            }
        }
    }
}