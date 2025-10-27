using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 10/06/2024
Descripción: Script que gestiona los sonidos del juego.
*/

//Enumerator con la lista de sonidos a poder reproducir

public enum TiposSonido
{
    RECOGEROBJETOS,
    RECOGERCOLECCIONABLES,
    CAJONABIERTO,
    CAJONCERRADO,
    SWITCHER,
    PALANCA,
    COMPLETARPUZLE,
    DESBLOQUEAROBJ,
    DESBLOQUEARZONA,
    BOTONESMENU,
    vozMariposas,
    CAMPANAS,
    BALANZA,
    BOTONESGRIMORIO,
    COFRE,
    PORTON,
    PUERTACRISTALabrir,
    PUERTACRISTALcerrar,
    LLAVEPANEL,
    noMARIPOSAS,
    VELAS,
    fichaEspiritu,
    fichaFuego,
    fichaAgua,
    fichaTierra,
    fichaAire
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] sonidosLista;
    public static AudioManager instance;
    //AudioSource para reproducir los sonidos
    public AudioSource audioSource;
    //acceso al audioSource de la musica del juego
    public AudioSource GameMusic;

    private bool audioWaiting = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(audioSource);
        }
    }

    public static void SonarSonido(TiposSonido sound, float volumen = 1)
    {
        if (instance.audioSource.isPlaying)
        {
            instance.StartCoroutine(instance.WaitForAudioToFinish(sound, volumen));
        }
        else
        {
            AudioClip clip = instance.sonidosLista[(int)sound];
            instance.audioSource.PlayOneShot(instance.sonidosLista[(int)sound], volumen);
        }
    }

    private IEnumerator WaitForAudioToFinish(TiposSonido sound, float volumen = 1)
    {
        if (!audioWaiting)
        {
            // Espera hasta que termine el audio actual
        audioWaiting = true;
        yield return new WaitWhile(() => audioSource.isPlaying);
        audioWaiting = false;

        // Reproduce el nuevo audio
        AudioClip clip = instance.sonidosLista[(int)sound];
        instance.audioSource.PlayOneShot(instance.sonidosLista[(int)sound], volumen);
        }
    }
}