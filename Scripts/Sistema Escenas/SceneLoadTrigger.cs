using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 09/06/2024
Descripción: Script que gestiona la carga y descarga de escenas
*/

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneField[] _escenasacargar;
    [SerializeField] private SceneField[] _escenadescargar;
    [SerializeField] private SceneField _escenaVacia; // Define aqu� el nombre de tu escena vac�a.

    public void IniciarPuzle()
    {
        CargarEscenas();
        //Debug.Log("Cargando las escenas");
        DescargarEscenas();
    }

    private void CargarEscenas()
    {
        SaveManager.Instance.guardarPartida();
        for (int i = 0; i < _escenasacargar.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _escenasacargar[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (!isSceneLoaded)
            {
                //Debug.Log("Cargando la escena: " + _escenasacargar[i].SceneName);
                SceneManager.LoadSceneAsync(_escenasacargar[i].SceneName, LoadSceneMode.Additive);
            }
        }
    }

    private void DescargarEscenas()
    {
           //Debug.Log("Cargando la escena vac�a antes de descargar la �ltima escena cargada.");
           SceneManager.sceneLoaded += OnEmptySceneLoaded;
           SceneManager.LoadSceneAsync(_escenaVacia, LoadSceneMode.Additive);
           UnloadScenes();
    }

    private void OnEmptySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == _escenaVacia)
        {
            SceneManager.sceneLoaded -= OnEmptySceneLoaded;
            UnloadScenes();
        }
    }

    private void UnloadScenes()
    {
        for (int i = 0; i < _escenadescargar.Length; i++)
        {
            bool isSceneLoaded = false;
            for (int j = 0; j < SceneManager.sceneCount; j++)
            {
                Scene loadedScene = SceneManager.GetSceneAt(j);
                if (loadedScene.name == _escenadescargar[i].SceneName)
                {
                    isSceneLoaded = true;
                    break;
                }
            }

            if (isSceneLoaded)
            {
                //Debug.Log("Descargando la escena: " + _escenadescargar[i].SceneName);
                StartCoroutine(UnloadScene(_escenadescargar[i].SceneName));
            }
            else
            {
                //Debug.Log("La escena no est� cargada: " + _escenadescargar[i].SceneName);
            }
        }
    }

    private IEnumerator UnloadScene(string sceneName)
    {
        AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
        if (asyncOperation == null)
        {
            //Debug.LogError("Error al intentar descargar la escena: " + sceneName);
            yield break;
        }

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        //Debug.Log("Escena descargada: " + sceneName);
    }
}