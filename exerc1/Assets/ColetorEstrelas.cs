using UnityEngine;

public class ColetorEstrelas : MonoBehaviour
{
    public Moedas moedas;
    public string tagEstrela = "Estrela";

    void OnTriggerEnter(Collider outro)
    {
        if (!outro.CompareTag(tagEstrela)) return;

        if (moedas != null)
            moedas.AdicionarMoeda();

        outro.gameObject.SetActive(false);
    }
}