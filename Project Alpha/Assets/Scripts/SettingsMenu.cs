using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Text HMSText;
    public Slider HMSSlider;
    public TMP_Text VMSText;
    public Slider VMSSlider;
    public void SliderUpdate(string slider)
    {
        switch (slider)
        {
            case "Vertical":
                VMSText.text = VMSSlider.value.ToString(CultureInfo.InvariantCulture);
                PlayerData.MouseSensitivity = new Vector2(PlayerData.MouseSensitivity.x, VMSSlider.value);
                break;
            case "Horizontal":
                HMSText.text = HMSSlider.value.ToString(CultureInfo.InvariantCulture);
                PlayerData.MouseSensitivity = new Vector2(HMSSlider.value, PlayerData.MouseSensitivity.y);
                break;
        }
    }
}
