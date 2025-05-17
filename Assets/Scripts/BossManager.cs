using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject imageToChange; // Imagem na UI para mudar quando o Boss estiver quase spawnando
    private float timeToChangeIcon1; // Quanto tempo depois de comecar a fase o icone deve mudar
    private float timeToChangeIcon2; // Quanto tempo depois de comecar a fase o icone deve mudar
    private float timeToChangeIcon3; // Quanto tempo depois de comecar a fase o icone deve mudar
    private float timeToChangeIcon4; // Quanto tempo depois de comecar a fase o icone deve mudar
    [SerializeField] public Sprite bossAproachingIcon1quarto; // Sprite para o qual o icone deve mudar para indicar que o boss esta quase spawnando
    [SerializeField] public Sprite bossAproachingIcon2quarto; // Sprite para o qual o icone deve mudar para indicar que o boss esta quase spawnando
    [SerializeField] public Sprite bossAproachingIcon3quarto; // Sprite para o qual o icone deve mudar para indicar que o boss esta quase spawnando
    [SerializeField] public Sprite bossAproachingIcon4quarto; // Sprite para o qual o icone deve mudar para indicar que o boss esta quase spawnando

    [SerializeField] private float timeToSpawnBoss1 = 90f; // Quanto tempo depois de comecar a fase o boss deve spawnar
    public float timePassed; // Quanto tempo passou desde o comeco da fase
    public bool boss1CanSpawn = false; // Se o boss pode spawnar
    [SerializeField] private GameObject boss1Prefab; // Prefab do boss

    void Start()
    {
        timePassed = Time.time;

        timeToChangeIcon1 = timeToSpawnBoss1 / 4;
        timeToChangeIcon2 = 2 * timeToSpawnBoss1 / 4;
        timeToChangeIcon3 = 3 * timeToSpawnBoss1 / 4;
        timeToChangeIcon4 = 4 * timeToSpawnBoss1 / 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (boss1CanSpawn)
        {
            if (Time.time - timePassed > timeToSpawnBoss1) {
                boss1CanSpawn = false;
                Instantiate(boss1Prefab);
                GameObject[] teleports = GameObject.FindGameObjectsWithTag("TeleportZone");
                foreach (GameObject obj in teleports) {
                    obj.GetComponent<RoomTeleport>().enabled = false;
                    obj.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 255f);
                    obj.GetComponent<BoxCollider2D>().isTrigger = false;
                }
            }
            if (Time.time - timePassed > timeToChangeIcon4) {
                Image i = imageToChange.GetComponent<Image>();
                i.sprite = bossAproachingIcon4quarto;
            }
            else if (Time.time - timePassed > timeToChangeIcon3) {
                Image i = imageToChange.GetComponent<Image>();
                i.sprite = bossAproachingIcon3quarto;
            }
            else if (Time.time - timePassed > timeToChangeIcon2) {
                Image i = imageToChange.GetComponent<Image>();
                i.sprite = bossAproachingIcon2quarto;
            }
            else if (Time.time - timePassed > timeToChangeIcon1)
            {
                Image i = imageToChange.GetComponent<Image>();
                i.sprite = bossAproachingIcon1quarto;
            }
        }
    }
}
