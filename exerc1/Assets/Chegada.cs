using UnityEngine;

public class Chegada : MonoBehaviour
{
    public GameObject Ganhou;
    public string Finish = "Player";
    public bool pararJogo = true;

    void OnTriggerEnter(Collider outro)
    {
        if (!outro.CompareTag(Finish)) return;

        Ganhou.SetActive(true);

        if (pararJogo)
            Time.timeScale = 0f;
    }
}