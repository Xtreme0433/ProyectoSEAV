using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoBrillo : MonoBehaviour
{
    public Slider slider;
    public float SliderValue;
    public Image panelBrillo;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("brillo", 0.5f);

        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChageSlider(float value)
    {
        slider.value = value;
        PlayerPrefs.SetFloat("brillo", SliderValue);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, slider.value);
    }
}
