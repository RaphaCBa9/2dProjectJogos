using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealthPoints = 100f;
    public float healthPoints;
    public TMP_Text healthTxt;
    public Slider slider;
    public GameObject gameOverPanel;

    private float damageCooldown = 1f;

    public bool isKnockbackActive = false;
    public float knockbackAmount;

    void Start()
    {
        healthPoints = maxHealthPoints;
    }

    void Update()
    {
        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }
        else
        {
            damageCooldown = 0;
        }
    }

    public void HandleMudarSlider(float valor)
    {
        if (healthTxt != null)
        {
            healthTxt.SetText(valor.ToString("F0"));
        }
    }

    public void TomarDano(float dano)
    {
        if (damageCooldown == 0)
        {
            healthPoints -= dano;

            if (healthPoints <= 0)
            {
                SceneManager.LoadSceneAsync("GameOver");
            }

            HandleMudarSlider(healthPoints);
            slider.value = healthPoints;
            damageCooldown = 1f;
        }

    }
}