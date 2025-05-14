using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealthPoints = 100f;
    public float healthPoints;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject gameOverPanel; 
    
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
