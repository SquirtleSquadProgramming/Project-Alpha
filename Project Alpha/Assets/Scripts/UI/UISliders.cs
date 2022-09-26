using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using TMPro;

public class UISliders : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    public void UpdateSlider()
        => text.text = slider.value.ToString(CultureInfo.InvariantCulture);
}
