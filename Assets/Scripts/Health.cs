using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealthPoints = 100f;
    public float healthPoints;
    public TMP_Text healthTxt;
    public Slider slider;
    public GameObject gameOverPanel; 
    
    public bool isKnockbackActive = false;
    public float knockbackAmount;
    
    void Start()
    {
        healthPoints = maxHealthPoints;
    }

    public void HandleMudarSlider(float valor) {
        if (healthTxt != null) {
            healthTxt.SetText(valor.ToString("F0"));
        }
    }

    public void TomarDano(float dano) {
        healthPoints -= dano;

        if (healthPoints <= 0) {
            gameOverPanel.SetActive(true);
        }

        HandleMudarSlider(healthPoints);
        slider.value = healthPoints;
    }
}
