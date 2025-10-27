using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SaveDataLoader : MonoBehaviour
{
    private void Awake()
    {
        // Suscribirse al evento de carga de escenas
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar errores
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Cargar el archivo de guardado
        SaveData saveData = SaveManager.Instance.ObtenerSaveData();

        if (saveData == null)
        {
            Debug.LogWarning("No se pudo cargar el archivo de guardado.");
            return;
        }

        // Llamar a los métodos modulares para cargar los datos
        CargarObjetosRecogidos(saveData);
        CargarColeccionables(saveData);
        CargarObjetosColocados(saveData);

        // Delegar la carga de puzles a PuzleController
        if (PuzleController.Instance != null)
        {
            PuzleController.Instance.LoadData(saveData);
        }
        else
        {
            Debug.LogWarning("PuzleController no está inicializado en la escena.");
        }
    }

    private void CargarObjetosRecogidos(SaveData saveData)
    {
        // Buscar todos los objetos interactuables en la escena
        ItemControler[] interactuables = FindObjectsOfType<ItemControler>();
        MultipleObjeto[] multipleObjetos = FindObjectsOfType<MultipleObjeto>();
        string id = "";
        foreach (ItemControler interactuable in interactuables)
        {
            // Obtener el ID del objeto interactuable
            id = interactuable.Item.id.ToString();

            // Verificar si el ID está en el archivo de guardado
            if (saveData.itemsObtenibles.Contains(id))
            {
                // Desactivar o destruir el objeto según corresponda
                Destruir destruir = interactuable.GetComponent<Destruir>();
                if (destruir != null)
                {
                    destruir.isLoading = true; // Evitar efectos visuales/sonoros
                    destruir.OnInteract();
                }
                else
                {
                    interactuable.gameObject.SetActive(false);
                }
            }
        }

        // Cargar el estado de los objetos MultipleObjeto
        foreach (MultipleObjeto multipleObjeto in multipleObjetos)
        {
            if (id == multipleObjeto.item.id.ToString()) continue;
            id = multipleObjeto.item.id.ToString();
            if (saveData.itemsObtenibles.Contains(id))
            {
                multipleObjeto.loading();
            }
            // Llamar al método loading para manejar el estado del objeto

        }
    }

    private void CargarColeccionables(SaveData saveData)
    {
        // Obtener la lista de IDs de coleccionables guardados
        List<string> coleccionablesGuardados = saveData.itemsColeccionables;

        if (coleccionablesGuardados == null || coleccionablesGuardados.Count == 0)
        {
            Debug.LogWarning("No se encontraron coleccionables en el archivo de guardado.");
            return;
        }

        // Buscar todos los botones de coleccionables en la escena
        DestruirColeccionable[] botones = FindObjectsOfType<DestruirColeccionable>();

        foreach (DestruirColeccionable boton in botones)
        {
            if (boton != null && boton.coleccionable != null)
            {
                // Verificar si el coleccionable asociado al botón está en la lista de guardado
                string id = boton.coleccionable.id.ToString();
                if (coleccionablesGuardados.Contains(id))
                {
                    // Marcar el botón como cargado y ejecutar su interacción
                    boton.isLoading = true;
                    boton.OnInteract();

                    // Agregar el coleccionable al ColeccionableManager
                    ColeccionableManager.Instance.Coleccionables[boton.coleccionable.id] = boton.coleccionable;
                }
            }
        }
    }

    private void CargarObjetosColocados(SaveData saveData)
    {
        // Obtener la lista de objetos colocados desde SaveData
        List<PlacedItemsRecord> objetosColocados = saveData.itemsColocados;

        if (objetosColocados == null || objetosColocados.Count == 0)
        {
            Debug.LogWarning("No se encontraron objetos colocados en el archivo de guardado.");
            return;
        }

        // Buscar todos los objetos de tipo ObjetosBrujas en la escena
        ObjetosBrujas[] objetosBrujas = FindObjectsOfType<ObjetosBrujas>();

        bool objeto15Colocado = false;

        foreach (PlacedItemsRecord record in objetosColocados)
        {
            foreach (ObjetosBrujas objetoBruja in objetosBrujas)
            {
                // Verificar si el objeto coincide con el registro en el guardado
                if (objetoBruja.itemId.ToString() == record.itemId && objetoBruja.gameObject.name == record.holderId)
                {
                    Debug.Log($"Cargando objeto colocado: ID '{record.itemId}' en '{record.holderId}'");

                    // Activar el objeto colocado
                    objetoBruja.objeto.SetActive(true);

                    // Verificar si el objeto con ID 15 ha sido colocado
                    if (objetoBruja.itemId == 15)
                    {
                        objeto15Colocado = true;
                    }
                    break;
                }
            }
        }

        // Si el objeto con ID 15 ha sido colocado, activar las animaciones de las puertas
        if (objeto15Colocado)
        {
            foreach (ObjetosBrujas objetoBruja in objetosBrujas)
            {
                if (objetoBruja.itemId != 15) continue;
                objetoBruja.PuertaAnimator[0].enabled = true;
                objetoBruja.PuertaAnimator[1].enabled = true;
                objetoBruja.PuertaAnimator[0].Play("PuertaOuijaR", 0, 1f);
                objetoBruja.PuertaAnimator[1].Play("PuertaOuijaL", 0, 1f);
            }
            Debug.Log("El objeto con ID 15 ha sido colocado. Activando las puertas.");
        }
    }
}