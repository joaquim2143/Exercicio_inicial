using TMPro;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Pontos : MonoBehaviour
{
    public Transform jogador;
    public TMP_Text contar;
    string original;
    private void Start()
    {
        original = contar.text;
    }
    void Update()
    {
        contar.text = original + jogador.position.z.ToString("0");
    }
}