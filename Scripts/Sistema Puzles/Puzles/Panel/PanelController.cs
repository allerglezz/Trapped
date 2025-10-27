using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/*
Creado por: Beatriz Aller
Fecha de creación: 20/05/2024
Descripción: Script que controla el puzle de las mariposas.
*/

public class PanelController : MonoBehaviour
{
    [SerializeField] public Button palancaGrande;
    private Animator anim;
    public List<Animator> palanquitas = new List<Animator>();
    public List<bool> solucion = new List<bool>();

    public GameObject luzvela;
    public GameObject luzlampara;
    public PlayableDirector timelinePanel;
    public Animator puerta;
    private bool completado = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(!completado)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public bool Correcto()
    {
        for(int i = 0; i < palanquitas.Count; i++)
        {
            if (palanquitas[i].GetBool("Bajar") != solucion[i])
            {
                return false;
            }
        }
        return true;
    }

    public void OnMouseDown()
    {
        StartCoroutine(HandleAnimationSequence());
    }

    IEnumerator HandleAnimationSequence()
    {
        anim.SetTrigger("Correcto");
        Debug.Log("Correcto: true");

        yield return new WaitForSeconds(1); // Ajusta este tiempo seg�n la duraci�n de tu animaci�n

        if (!Correcto())
        {
            anim.SetTrigger("Incorrecto");
            Debug.Log("Correcto: false");
        }
        else
        {
            completado = true;
            RenderSettings.fog = false;
            luzvela.SetActive(false);
            luzlampara.SetActive(false);
            timelinePanel.Play();
            puerta.enabled = true;
            yield return new WaitForSeconds((float)timelinePanel.duration);
            PuzleController.Instance.PuzleCompletado("Panel");
            /* DataPersistanceManager.Instance.SaveGameAsync(); */
            SceneManager.LoadScene("MainScene");
        }
    }
}
