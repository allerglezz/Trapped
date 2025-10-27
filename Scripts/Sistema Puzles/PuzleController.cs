using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 24/05/2024
Descripción: Script que guarda la información de los puzles y gestiona su activacion y estados.
*/

public class PuzleController : MonoBehaviour
{
    static public PuzleController Instance;
    public List<Puzle> puzles = new List<Puzle>();
    bool cargadoPuzles = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            puzles.Add(new Puzle("Panel", false));
            puzles.Add(new Puzle("Campanas", false));
            puzles.Add(new Puzle("Mariposas", false));
            puzles.Add(new Puzle("Zodiaco", false));
            puzles.Add(new Puzle("Bonsai", false));
            puzles.Add(new Puzle("Pentaculo", false));
            puzles.Add(new Puzle("Balanza", false));
            puzles.Add(new Puzle("Caldero", false));
        }
    }

    //Metodo para marcar un puzle como completado
    public void PuzleCompletado(string puzleNombre)
    {
        SaveManager.Instance.GuardarPuzleCompletado(puzleNombre);
        SaveManager.Instance.guardarPartida();
        foreach (var puzle in puzles)
        {
            if (puzle.nombre == puzleNombre)
            {
                puzle.completado = true;
            }
        }
    }

    public Puzle PuzleEstado(string puzleNombre)
    {
        foreach (var puzle in puzles)
        {
            if (puzle.nombre == puzleNombre)
            {
                return puzle;
            }
        }
        return null;
    }

    //Metodo para comprobar si un puzle esta disponible
    public bool PuzleAccesible(string puzleNombre)
    {
        foreach (var puzle in puzles)
        {
            if (puzle.nombre == puzleNombre)
            {
                if (puzle.nombre == "Panel" || puzle.nombre == "Mariposas" || puzle.nombre == "Campanas" || puzle.nombre == "Zodiaco")
                {
                    Debug.Log(puzle.nombre + " " + puzle.completado);
                    return !puzle.completado;
                }
                else if (puzle.nombre == "Balanza")
                {
                    bool activar = true;
                    if(puzle.completado)
                    {
                        return false;
                    }
                    foreach (Puzle obj in puzles)
                    {
                        if (obj.nombre != "Balanza" && !obj.completado)
                        {
                            activar = false;
                        }
                    }
                    return activar;
                }
                else
                {
                    if (puzles[0].completado && puzles[1].completado && puzles[2].completado && puzles[3].completado)
                    {
                        return !puzle.completado;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        return false;
    }

    //Parte del sistema de carga de los puzles
    public void iniciarPuzles()
    {
        CompletarPuzle[] completarPuzle = FindObjectsOfType<CompletarPuzle>();
        foreach(var puzle in puzles)
        {
            if (puzle.completado) 
            {
                foreach (CompletarPuzle completar in completarPuzle)
                {
                    if (puzle.nombre == completar.puzle)
                    {
                        completar.activarPuzle();
                        break;
                    }
                }

            }
        }
        //texto contador 
    }

    //Sistema de carga de los puzles
    public void LoadData(SaveData saveData)
    {
        // Obtener la lista de puzles completados desde SaveData
        List<string> puzlesCompletados = saveData.puzlesCompletados;

        if (puzlesCompletados == null || puzlesCompletados.Count == 0)
        {
            Debug.LogWarning("No se encontraron puzles completados en el archivo de guardado.");
            return;
        }

        // Buscar todos los objetos de tipo CompletarPuzle en la escena
        CompletarPuzle[] completarPuzles = FindObjectsOfType<CompletarPuzle>();

        foreach (string puzleId in puzlesCompletados)
        {
            foreach (CompletarPuzle completarPuzle in completarPuzles)
            {
                if (completarPuzle.puzle == puzleId)
                {
                    Debug.Log($"Cargando estado completado para el puzle: {puzleId}");

                    // Activar el estado final del puzle
                    puzles.Find(p => p.nombre == puzleId).completado = true;
                    completarPuzle.SetLoadingState(true); // Configurar el estado de carga
                    break;
                }
            }
        }
    }
}