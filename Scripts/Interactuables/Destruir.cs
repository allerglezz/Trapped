using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Script que destruye un objeto y lo añade al inventario al interactuar con él.
    Tiene varias configuraciones, como activar un objeto al destruirse, activar un grupo de objetos al destruirse...
*/

public class Destruir : BaseInteractuable
{
    [SerializeField] activarEnInv inventario;
    //se sobrescribe onInteract de baseInteractuable para destruir el objeto
    public TiposSonido vozRecogerGrupo;
    //grupo de objetos para desbloquear el item en el inventario
    public Destruir[] grupo;
    public int contadorGrupo;
    //variable para saber si el objeto se está cargando, para evitar el sonido de recoger objetos
    public bool isLoading = false;
    //otros objetos a destruir al interactuar con este
    public GameObject[] otherObjectsToDestroy;
    //animacion al recoger el objeto
    public PlayableDirector animacion;
    public GameObject cameraAnim;
    public Animator[] animators;
    public override void OnInteract()
    {
        if (grupo.Count() > 0 && !isLoading)
        {
            contadorGrupo--;
            foreach (Destruir destruir in grupo)
            {
                destruir.contadorGrupo--;
            }
            if (contadorGrupo == 0)
            {
                AudioManager.SonarSonido(vozRecogerGrupo);
            }
        }
        if (inventario == null)
        {
            if (item != null)
            {
                base.OnInteract();
                this.GetComponent<ItemControler>().Recoger();
            }
            Destroy(gameObject);
        }
        else
        {
            if (otherObjectsToDestroy.Count() > 0)
            {
                foreach (GameObject obj in otherObjectsToDestroy)
                {
                    obj.SetActive(false);
                }
            }
            Debug.Log("Mostrando");
            base.OnInteract();
            if(item.id != 17) this.GetComponent<ItemControler>().Recoger();
            inventario.visible();
            if (!isLoading)
            {
                AudioManager.SonarSonido(TiposSonido.RECOGEROBJETOS);
            }
            if (animacion != null && !isLoading)
            {
                StartCoroutine(hacerCinematica());
            }
            else if (animacion == null)
            {
                Destroy(gameObject);
            }
        }
    }

    public IEnumerator hacerCinematica()
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        movement.setPause();
        AudioManager.instance.GameMusic.Stop();
        foreach (Animator anim in animators)
        {
            anim.enabled = true;
        }
        animacion.Play();
        yield return new WaitForSeconds((float)animacion.duration - 1);
        SceneManager.LoadScene("MenuInicio");
    }
}