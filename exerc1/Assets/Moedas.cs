using TMPro;
using UnityEngine;

public class Moedas : MonoBehaviour
{
    public TMP_Text contar;
    public int moedas;
    string original;

    void Start()
    {
        original = contar.text;
        Mostrar();
    }

    public void AdicionarMoeda()
    {
        moedas++;
        Mostrar();
    }

    void Mostrar()
    {
        if (contar != null)
            contar.text = original + moedas.ToString();
    }
}