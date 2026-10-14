using UnityEngine;

public class Chegada : MonoBehaviour
{
    public GameObject Ganhou;
    public string TagPlayer = "Player";
    public bool pararJogo = true;

    void OnTriggerEnter(Collider outro)
    {
        if (!outro.CompareTag(TagPlayer)) return;

        Ganhou.SetActive(true);

        if (pararJogo)
            Time.timeScale = 0f;
    }
}