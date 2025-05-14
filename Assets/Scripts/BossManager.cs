using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] GameObject imageToChange; // Imagem na UI para mudar quando o Boss estiver quase spawnando
    [SerializeField] private float delayToChangeIcon = 15f; // Quanto tempo antes de spawnar o Boss a imagem deve mudar
    private float timeToChangeIcon; // Quanto tempo depois de comecar a fase o icone deve mudar
    [SerializeField] public Sprite bossAproachingIcon; // Sprite para o qual o icone deve mudar para indicar que o boss esta quase spawnando

    [SerializeField] private float timeToSpawnBoss1 = 40f; // Quanto tempo depois de comecar a fase o boss deve spawnar
    private float timePassed; // Quanto tempo passou desde o comeco da fase
    private bool boss1CanSpawn = true; // Se o boss pode spawnar
    [SerializeField] private GameObject boss1Prefab; // Prefab do boss
    
    void Start()
    {
        timePassed = Time.time;
        timeToChangeIcon = timeToSpawnBoss1 - delayToChangeIcon;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timePassed > timeToSpawnBoss1 && boss1CanSpawn) {
            boss1CanSpawn = false;
            Instantiate(boss1Prefab);
        }
        if (Time.time - timePassed > timeToChangeIcon) {
            Image i = imageToChange.GetComponent<Image>();
            // i.color = new Color(5f, 100f, 200f);
            i.sprite = bossAproachingIcon;
        }
    }
}
