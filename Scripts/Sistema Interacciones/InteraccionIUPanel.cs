using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
Creado por: Beatriz Aller
Fecha de creación: 14/04/2024
Descripción: Script que controla la animación de la escena exterior
*/

public class InteraccionIUPanel : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI tooltipText;

    public void SetTooltip(string tooltip)
    {
        tooltipText.SetText(tooltip);
    }

    public void ResetUI()
    {
        tooltipText.SetText("");
    }
}
