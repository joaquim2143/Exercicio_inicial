using UnityEngine;

public class Perdeu : MonoBehaviour
{
    public string TagPerdeu = "Perdeu";
    public GameObject telaDerrota;
    public bool pararJogo = true;

    private bool perdeu = false;

    void OnCollisionEnter(Collision colisao)
    {
        if (colisao.gameObject.CompareTag(TagPerdeu))
            Perder();
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag(TagPerdeu))
            Perder();
    }

    void Perder()
    {
        if (perdeu) return;
        perdeu = true;

        if (telaDerrota != null)
            telaDerrota.SetActive(true);

        if (pararJogo)
            Time.timeScale = 0f;

        Debug.Log("Você perdeu!");
    }
}