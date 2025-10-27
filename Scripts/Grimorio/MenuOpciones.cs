using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


/*
Creado por: Lucia Moreno
Fixeado y comentado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Script que gestiona las opciones del menú de pausa.
*/

public class MenuOpciones : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider barraBrillo;
    public PostProcessProfile brillo;
    public PostProcessLayer layer;
    AutoExposure exposicion;
    /* DataPersistanceManager guardado; */
    //public static AudioManager audioManager;

    public void Start()
    {
        /* guardado = FindObjectOfType<DataPersistanceManager>(); */
        //audioManager = FindObjectOfType<AudioManager>();
        brillo.TryGetSettings(out exposicion);
        barraBrillo.minValue = 0;
        barraBrillo.maxValue = 2;
        barraBrillo.value = 1;
        barraBrillo.onValueChanged.AddListener(AjustarBrillo);
        AjustarBrillo(barraBrillo.value); // Ajustar el brillo inicial
    }

    public void Volumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }

    public void AjustarBrillo(float valorBrillo)
    {
        RenderSettings.ambientIntensity = Mathf.Clamp(valorBrillo, 0, 2);
    }

    public void SalirJuego()
    {
        Debug.Log("C salio");
        Application.Quit();
    }

    public void Guardar()
    {
        Debug.Log("guardando");
        SaveManager.Instance.guardarPartida();
    }
}
