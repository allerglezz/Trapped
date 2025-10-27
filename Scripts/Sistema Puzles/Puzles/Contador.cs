using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 13/06/2024
Descripción: Script que controla el contador de puzles y sus canvas
*/

public class Contador : MonoBehaviour
{
    //los diferentes canvas
    [SerializeField] private TextMeshProUGUI contadorIntText;
    [SerializeField] private TextMeshProUGUI textoContador;
    [SerializeField] private TextMeshProUGUI textoBrujas;

    void LateUpdate()
    {
        checkPuzles();
    }
    //se comprueba cuantos puzles hay completados y se actualizan los canvas
    public void checkPuzles()
    {
        int contador = 8;
        PuzleController si = PuzleController.Instance;
        if (PuzleController.Instance.puzles != null)
        {
            foreach(Puzle puzle in PuzleController.Instance.puzles)
            {
                if (puzle.completado == true)
                {
                    contador -= 1;
                }
            }
        }
        contadorIntText.text = contador.ToString();
        if (contador <= 4)
        {
            textoBrujas.gameObject.SetActive(true);
            ObjetosBrujas[] objetosBrujas = FindObjectsOfType<ObjetosBrujas>();
            placasBrujas[] placasBrujas = FindObjectsOfType<placasBrujas>();
            foreach(ObjetosBrujas objeto in objetosBrujas)
            {
                objeto.activarInteraccion();
            }
            foreach(placasBrujas placaBruja in placasBrujas)
            {
                placaBruja.activarInteraccion();
            }
        }
        //se desactivan los canvas una vez completados los puzles
        if (contador == 0)
        {
            textoBrujas.gameObject.SetActive(false);
            textoContador.gameObject.SetActive(false);
            contadorIntText.gameObject.SetActive(false);
        }
    }
}