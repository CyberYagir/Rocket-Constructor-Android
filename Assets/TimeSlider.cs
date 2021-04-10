using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSlider : MonoBehaviour
{
    public GameObject sliderPanel;
    public Slider slider;
    public TMP_Text text;
    public void OnEnable()
    {
        Time.timeScale = 1;
        slider.value = 1;
    }


    public void OnDisable()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        text.text = $"T.{Time.timeScale.ToString("F3")}x:";
        Time.timeScale = slider.value;
        sliderPanel.SetActive(UIManager.simulate);
    }


}
