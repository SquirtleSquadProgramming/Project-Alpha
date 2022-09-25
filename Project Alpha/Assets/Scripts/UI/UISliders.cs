using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;

public class UISliders : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    public void UpdateSlider()
    {
        text.text = slider.value.ToString(CultureInfo.InvariantCulture);
        switch (name) {
            case "MouseXSense":
                PlayerData.MouseSensitivity = new Vector2(slider.value, PlayerData.MouseSensitivity.y);
                break;
            case "MouseYSense":
                PlayerData.MouseSensitivity = new Vector2(PlayerData.MouseSensitivity.x, slider.value);
                break;
            case "FieldOfView":
                PlayerData.FieldOfView = (int)slider.value;
                break;
            default:
                Debug.LogError("No Slider found, therefore no parameter modified!");
                break;
        }
    }
}
