using UnityEngine;
    using System.IO;
    using System.Collections.Generic;
    using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    string savePath;
    string tmpPath;

    public void Awake()
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
        }

        savePath = Path.Combine(Application.persistentDataPath, "game1.json");
        tmpPath = Path.Combine(Application.persistentDataPath, "game1.tmp");

        if (File.Exists(savePath))
        {
            CrearGuardadoTemporal();
            Debug.Log("Archivo de guardado temporal creado en: " + savePath);
        }
    }

    public void guardarPartida()
    {
        if (File.Exists(savePath))
        {
            Debug.Log("Guardando partida...");
            if (VerificarGuardadoExistente(tmpPath))
            {
                reemplazarPrincipal();
            }
            else
            {
                Debug.LogError("El guardado temporal no es válido. No se aplicará.");
            }
            Backup();
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath);
            Debug.Log("Directorio de guardado creado: " + Application.persistentDataPath);
        }
    }

    private void Backup()
    {
        if (File.Exists(savePath))
        {
            File.Copy(savePath, savePath + ".bak", true);
            Debug.Log("Backup creado en: " + savePath + ".bak");
        }
        else
        {
            Debug.LogError("No se encontró el archivo de guardado para crear una copia de seguridad.");
        }
    }

    private void reemplazarPrincipal()
    {
        if (File.Exists(tmpPath))
        {
            File.Copy(tmpPath, savePath, true);
            Debug.Log("Guardado aplicado desde el archivo temporal.");
        }
        else
        {
            Debug.LogError("No se encontró el archivo temporal para aplicar el guardado.");
        }
    }

    private bool VerificarGuardadoExistente(string path)
    {
        Debug.Log("Verificando existencia de guardado en: " + path);
        if (!File.Exists(path))
        {
            Debug.LogError("Error al crear el archivo de guardado.");
            return false;
        }
        var info = new FileInfo(path);
        if (info.Length == 0)
        {
            Debug.LogError("El archivo está vacío.");
            return false;
        }
        try
        {
            string json;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fs))
            {
                json = reader.ReadToEnd();
            }

            var copia = JsonUtility.FromJson<SaveData>(json);
            return copia != null;
        }
        catch
        {
            Debug.LogError("Error al leer el archivo de guardado.");
            return false;
        }
    }

    public void cargarPartida()
    {
        if (!File.Exists(tmpPath))
        {
            Debug.LogError("No se encontró el archivo de guardado temporal.");
            return;
        }

        string json;
        using (FileStream fs = new FileStream(tmpPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (StreamReader reader = new StreamReader(fs))
        {
            json = reader.ReadToEnd();
        }

        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        ItemControler[] items = FindObjectsOfType<ItemControler>();

        foreach (ItemControler itemControler in items)
        {
            if (itemControler.Item != null && saveData.itemsObtenibles.Contains(itemControler.Item.id.ToString()))
            {
                // Marcar el objeto como cargado para que no reproduzca sonido ni animaciones
                Destruir destruir = itemControler.GetComponent<Destruir>();
                if (destruir != null)
                {
                    destruir.isLoading = true;
                    destruir.OnInteract();
                }
                else
                {
                    itemControler.gameObject.SetActive(false); // Por si acaso no tiene script Destruir
                }
            }
        }
    }

    public bool HayGuardado()
    {
        return File.Exists(savePath);
    }

    public void CrearGuardadoTemporal()
    {
        if (HayGuardado())
        {
            File.Copy(savePath, tmpPath, true);
            Debug.Log("Guardado temporal realizado en: " + tmpPath);
        }
        else
        {
            Directory.CreateDirectory(Application.persistentDataPath);
            Debug.Log("Directorio de guardado creado: " + Application.persistentDataPath);
            if (!File.Exists(tmpPath))
            {
                using (FileStream fs = File.Create(tmpPath))
                {
                    // Puedes escribir un JSON vacío válido si lo deseas:
                    byte[] data = System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(new SaveData()));
                    fs.Write(data, 0, data.Length);
                }
                Debug.Log("Archivo temporal creado: " + tmpPath);
            }
        }
    }

    public void itemRecogido(Item item)
    {
        if (item != null)
        {
            SaveData saveData;
            if (!File.Exists(tmpPath))
            {
                CrearGuardadoTemporal();
            }

            string json = File.ReadAllText(tmpPath);
            saveData = JsonUtility.FromJson<SaveData>(json);

            if (saveData == null)
            {
                Debug.LogWarning("El archivo tmp está vacío o mal formado. Se creará uno nuevo.");
                saveData = new SaveData(); // Crear una instancia vacía para continuar
            }

            // Evita duplicados
            if (!saveData.itemsObtenibles.Contains(item.id.ToString()))
            {
                saveData.itemsObtenibles.Add(item.id.ToString());
                Debug.Log("Guardado: recogido item con ID " + item.id);
            }

            json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(tmpPath, json);
        }
        else
        {
            Debug.LogError("Item inválido o sin ID.");
        }
    }

    public void itemRecogido(Coleccionable coleccionable)
    {
        if (coleccionable != null)
        {
            SaveData saveData;
            if (!File.Exists(tmpPath))
            {
                CrearGuardadoTemporal();
            }

            string json = File.ReadAllText(tmpPath);
            saveData = JsonUtility.FromJson<SaveData>(json);

            if (saveData == null)
            {
                Debug.LogWarning("El archivo tmp está vacío o mal formado. Se creará uno nuevo.");
                saveData = new SaveData(); // Crear una instancia vacía para continuar
            }

            // Evita duplicados
            if (!saveData.itemsColeccionables.Contains(coleccionable.id.ToString()))
            {
                saveData.itemsColeccionables.Add(coleccionable.id.ToString());
                Debug.Log("Guardado: recogido item con ID " + coleccionable.id);
            }

            json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(tmpPath, json);
        }
        else
        {
            Debug.LogError("Item inválido o sin ID.");
        }
    }

    public void crearGuardado()
    {
        if (!HayGuardado())
        {
            SaveData saveData = new SaveData();
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(savePath, json);
        }
    }


    public SaveData ObtenerSaveData()
    {
        if (!HayGuardado())
        {
            Debug.LogWarning("No hay un archivo de guardado disponible.");
            return new SaveData();
        }

        string json = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        if (saveData == null)
        {
            Debug.LogWarning("El archivo de guardado está vacío o mal formado.");
            return new SaveData();
        }

        return saveData;
    }

    public List<string> ObtenerItemsColeccionables()
    {
        if (!HayGuardado())
        {
            Debug.LogWarning("No hay un archivo de guardado disponible.");
            return new List<string>();
        }

        string json = File.ReadAllText(savePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);

        if (saveData == null)
        {
            Debug.LogWarning("El archivo de guardado está vacío o mal formado.");
            return new List<string>();
        }

        return saveData.itemsColeccionables;
    }

    public void GuardarPuzleCompletado(string puzleId)
    {
        if (string.IsNullOrEmpty(puzleId))
        {
            Debug.LogError("El ID del puzle es inválido o está vacío.");
            return;
        }

        SaveData saveData;

        // Crear un guardado temporal si no existe
        if (!File.Exists(tmpPath))
        {
            CrearGuardadoTemporal();
        }

        // Leer el archivo de guardado temporal
        string json = File.ReadAllText(tmpPath);
        saveData = JsonUtility.FromJson<SaveData>(json);

        if (saveData == null)
        {
            Debug.LogWarning("El archivo tmp está vacío o mal formado. Se creará uno nuevo.");
            saveData = new SaveData(); // Crear una instancia vacía para continuar
        }

        // Evitar duplicados
        if (!saveData.puzlesCompletados.Contains(puzleId))
        {
            saveData.puzlesCompletados.Add(puzleId);
            Debug.Log($"Guardado: puzle completado con ID '{puzleId}'");
        }

        // Guardar los cambios en el archivo temporal
        json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(tmpPath, json);
    }

    public void GuardarObjetoColocado(int itemId, string holderId)
    {
        if (itemId <= 0 || string.IsNullOrEmpty(holderId))
        {
            Debug.LogError("El ID del objeto o del contenedor es inválido.");
            return;
        }

        SaveData saveData;

        // Crear un guardado temporal si no existe
        if (!File.Exists(tmpPath))
        {
            CrearGuardadoTemporal();
        }

        // Leer el archivo de guardado temporal
        string json = File.ReadAllText(tmpPath);
        saveData = JsonUtility.FromJson<SaveData>(json);

        if (saveData == null)
        {
            Debug.LogWarning("El archivo tmp está vacío o mal formado. Se creará uno nuevo.");
            saveData = new SaveData(); // Crear una instancia vacía para continuar
        }

        // Verificar si el objeto ya está registrado
        PlacedItemsRecord existingRecord = saveData.itemsColocados.Find(record => record.itemId == itemId.ToString() && record.holderId == holderId);
        if (existingRecord == null)
        {
            // Agregar un nuevo registro
            saveData.itemsColocados.Add(new PlacedItemsRecord
            {
                itemId = itemId.ToString(),
                holderId = holderId
            });
            Debug.Log($"Guardado: objeto con ID '{itemId}' colocado en '{holderId}'");
        }

        // Guardar los cambios en el archivo temporal
        json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(tmpPath, json);
    }
}

    [System.Serializable]
    public class SaveData
    {
        public List<string> itemsObtenibles = new List<string>();
        public List<string> itemsColeccionables = new List<string>();
        public List<string> puzlesCompletados = new List<string>();
        public List<PlacedItemsRecord> itemsColocados = new List<PlacedItemsRecord>();
    }

    [System.Serializable]
    public class PlacedItemsRecord
    {
        public string itemId;
        public string holderId;
    }