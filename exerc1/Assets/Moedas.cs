using TMPro;
using UnityEngine;

public class Moedas : MonoBehaviour
{
    public TMP_Text contar;
    public int moedas;
    string original;
    public AudioSource SomMoeda;
    public AudioClip Moeda;
    [SerializeField, Range(0f, 1f)] private float volume;
    void Start()
    {
        original = contar.text;
        Mostrar();
    }

    public void AdicionarMoeda()
    {
        SomMoeda.PlayOneShot(Moeda,volume);
        moedas++;
        Mostrar();
    }

    void Mostrar()
    {
        if (contar != null)
            contar.text = original + moedas.ToString();
    }
}