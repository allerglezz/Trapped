using UnityEngine;
using UnityEngine.Playables;

public class CinematicManager : MonoBehaviour
{
    public PlayableDirector timeline; // Referencia a la Timeline
    public Cinemachine.CinemachineVirtualCamera virtualCamera; // Referencia a la Virtual Camera

    void Start()
    {
        if (timeline == null)
        {
            Debug.Log("La referencia a la Timeline no ha sido asignada.");
        }
        else
        {
            timeline.stopped += OnPlayableDirectorStopped; // Añadir evento para cuando termine la Timeline
        }

        // Inicia la cinemática al cargar la escena
        //StartCinematic();
    }

    void StartCinematic()
    {
        if (timeline != null)
        {
            virtualCamera.gameObject.SetActive(true); // Asegúrate de que la Virtual Camera está activa
            timeline.Play();
        }
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == timeline)
        {
            // Detener la animación y desactivar la Virtual Camera al finalizar la Timeline
            virtualCamera.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (timeline != null)
        {
            timeline.stopped -= OnPlayableDirectorStopped; // Eliminar evento para evitar errores
        }
    }
}
