using UnityEngine;

public class Chegada : MonoBehaviour
{
    public GameObject Ganhou;
    public string TagPlayer = "Player";
    public bool pararJogo = true;
    public AudioSource SomGanhou;
    public AudioClip Ganha;
    [SerializeField, Range(0f, 1f)] private float volume;

    void OnTriggerEnter(Collider outro)
    {
        if (!outro.CompareTag(TagPlayer)) return;

        SomGanhou.PlayOneShot(Ganha, volume);
        Ganhou.SetActive(true);

        if (pararJogo)
            Time.timeScale = 0f;
    }
}