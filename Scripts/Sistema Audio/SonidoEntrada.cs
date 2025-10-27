using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Creado por: Beatriz Aller
Fecha de creación: 10/06/2024
Descripción: Script que controla el sonido de entrada de los estados de la animación.
*/

public class SonidoEntrada : StateMachineBehaviour
{
    [SerializeField] private TiposSonido sound;
    [SerializeField, Range(0, 1)] private float volume = 1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.SonarSonido(sound, volume);
    }


}
