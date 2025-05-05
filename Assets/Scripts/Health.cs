using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healthPoints;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private Slider slider;
    
    void Start()
    {
        healthPoints = 100;
    }

    public void HandleMudarSlider(float valor) {
        healthTxt.SetText(valor.ToString("F0"));
    }

    public void TomarDano(float dano) {
        healthPoints -= dano;
        HandleMudarSlider(healthPoints);
        slider.value = healthPoints;
    }
}
