using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarraDeVida : MonoBehaviour
{
    public Slider slider;

    public void MarcarVidaMaxima(float vida)
    {
        slider.maxValue = vida;
        slider.value = vida;
    }
    public void MarcarVida(float vida)
    {
        slider.value = vida;
    }
}