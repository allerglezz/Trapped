using System.Collections.Generic;
using UnityEngine;


/*
Creado por: Beatriz Aller
Fecha de creación: 19/04/2024
Descripción: Script que gestiona el inventario y su persistencia.
*/

public class InventarioManager : MonoBehaviour
{
    public static InventarioManager Instance;

    public List<Item> Items = new List<Item>();

    public List<Destruir> Botones = new List<Destruir>();

    public Transform ItemContent;

    public GameObject InventoryItem;

    private void Awake()
    {
        Botones = new List<Destruir>(FindObjectsOfType<Destruir>());
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        Debug.Log("InventarioManager Awake: " + Instance);
    }

    private void Start()
    {
        //Debug.Log("InventarioManager Start: " + gameObject.GetInstanceID());
        if (SaveManager.Instance != null && SaveManager.Instance.HayGuardado())
        {
            SaveData saveData = SaveManager.Instance.ObtenerSaveData();
            LoadData();
        }
        else
        {
            Debug.LogError("Persistence Manager not found or no data available!");
        }
    }

    public void Add(Item item)
    {
        if (item != null)
        {
            if (!HasItem(item.id))
            {
                Items[item.id] = item;
                //Debug.Log("Item anadido: {item.id}");

            }
        }
    }

    public bool HasItem(int itemId)
    {
        foreach (Item item in Items)
        {
            if (item != null && item.id == itemId) //posible necesario comprobacion nulo
            {
                return true;
            }
        }
        return false;
    }

    public void LoadData()
    {
        // Obtener los datos de guardado desde el SaveManager
        SaveData saveData = SaveManager.Instance.ObtenerSaveData();

        if (saveData == null)
        {
            Debug.LogWarning("No se pudo cargar el archivo de guardado.");
            return;
        }

        // Cargar y procesar objetos de tipo Incienso
        List<Incienso> inciensos = new List<Incienso>(FindObjectsOfType<Incienso>());
        foreach (Incienso incienso in inciensos)
        {
            incienso.loading();
        }

        // Cargar y procesar botones (Destruir)
        Botones = new List<Destruir>(FindObjectsOfType<Destruir>());
        foreach (Destruir boton in Botones)
        {
            if (boton != null && boton.item != null && saveData.itemsObtenibles.Contains(boton.item.id.ToString()))
            {
                boton.isLoading = true;
                boton.OnInteract();
            }
        }

        // Cargar y procesar objetos de tipo MultipleObjeto
        List<MultipleObjeto> multiples = new List<MultipleObjeto>(FindObjectsOfType<MultipleObjeto>());
        foreach (MultipleObjeto multiple in multiples)
        {
            multiple.loading();
        }

        // Cargar y procesar objetos de tipo GrimorioUp
        if (saveData.itemsObtenibles.Contains("18"))
        {
            GrimorioUp[] grimorios = FindObjectsOfType<GrimorioUp>();
            if (grimorios.Length > 0)
            {
                grimorios[0].Loading();
            }
        }
    }
}