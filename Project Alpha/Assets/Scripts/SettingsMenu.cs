using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    private (Slider, TMP_Text) Horz, Vert, FOV;

    public void Awake()
    {
        Horz = (
            GameObject.Find("Settings/MouseXSense/MXSSlider")
                      .GetComponent<Slider>(),
            GameObject.Find("Settings/MouseXSense/MXSText")
                      .GetComponent<TMP_Text>()
        );
            
        Vert = (
            GameObject.Find("Settings/MouseYSense/MYSSlider")
                      .GetComponent<Slider>(),
            GameObject.Find("Settings/MouseYSense/MYSText")
                      .GetComponent<TMP_Text>()
        );

        FOV = (
            GameObject.Find("Settings/FieldOfView/FOVSlider")
                      .GetComponent<Slider>(),
            GameObject.Find("Settings/FieldOfView/FOVText")
                      .GetComponent<TMP_Text>()
        );
    }

    public void SliderUpdate(string slider)
    {
        switch (slider)
        {
            case "Horizontal":
                Horz.Item2.text = Horz.Item1.value.ToString(CultureInfo.InvariantCulture);
                PlayerData.MouseSensitivity = new Vector2(Horz.Item1.value, PlayerData.MouseSensitivity.y);
                break;
            case "Vertical":
                Vert.Item2.text = Vert.Item1.value.ToString(CultureInfo.InvariantCulture);
                PlayerData.MouseSensitivity = new Vector2(PlayerData.MouseSensitivity.x, Vert.Item1.value);
                break;
            case "Field Of View":
                FOV.Item2.text = FOV.Item1.value.ToString(CultureInfo.InvariantCulture);
                PlayerData.FieldOfView = (int)FOV.Item1.value;
                break;
        }
    }
}
