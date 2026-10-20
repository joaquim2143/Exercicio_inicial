using System.Collections;
using TMPro;
using UnityEngine;

public class Perdeu : MonoBehaviour
{
    private string tagPerdeu = "Perdeu";
    private string tagVoid = "Void";
    public AudioSource hurt;
    public AudioClip Dano;
    [SerializeField, Range(0f, 1f)] private float volume;
    public int vidasMaximas = 3;
    private int vidas;
    public TMP_Text contar;
    public GameObject telaDerrota;
    public bool pararJogo = true;
    public float empurrao = -25f;
    public float duracao = 0.3f;
    public float recuperacao = 0.2f;
    public AudioSource SomPerdeu;
    public AudioClip Perde;
    [SerializeField, Range(0f, 1f)] private float volumeperdeu;

    private Rigidbody jogador;

    string original;

    private bool perdeu = false;
    private Coroutine aEmpurrar;

    void Awake()
    {
        if (jogador == null)
            jogador = GetComponent<Rigidbody>();
    }

    void Start()
    {
        vidas = vidasMaximas;
        original = contar != null ? contar.text : "";
        Mostrar();
    }

    void OnCollisionEnter(Collision colisao)
    {
        Verificar(colisao.gameObject);
    }

    void OnTriggerEnter(Collider outro)
    {
        Verificar(outro.gameObject);
    }

    void Verificar(GameObject outro)
    {
        if (perdeu) return;

        if (outro.CompareTag(tagVoid))
        {
            Perder();
            return;
        }

        if (!outro.CompareTag(tagPerdeu)) return;

        if (jogador != null)
        {
            if (aEmpurrar != null)
                StopCoroutine(aEmpurrar);
            hurt.PlayOneShot(Dano, volume);

            aEmpurrar = StartCoroutine(Empurrar());
        }
      
        vidas--;
        Mostrar();

        if (vidas <= 0)
            Perder();
    }

    void Perder()
    {
        if (perdeu) return;
        perdeu = true;
        SomPerdeu.PlayOneShot(Perde, volumeperdeu);

        if (aEmpurrar != null)
            StopCoroutine(aEmpurrar);

        if (telaDerrota != null)
            telaDerrota.SetActive(true);

        if (pararJogo)
            Time.timeScale = 0f;

        Debug.Log("Você perdeu!");
    }

    void Mostrar()
    {
        if (contar != null)
            contar.text = original + vidas;
    }

    IEnumerator Empurrar()
    {
        float velocidadeAntes = Mathf.Abs(jogador.linearVelocity.z);

        for (float t = 0; t < duracao; t += Time.deltaTime)
        {
            float intensidade = 1f - (t / duracao);
            jogador.AddForce(0, 0, empurrao * intensidade);
            yield return null;
        }

        for (float t = 0; t < recuperacao; t += Time.deltaTime)
        {
            float f = Mathf.Clamp01(t / recuperacao);
            f = f * f * (3f - 2f * f);

            float z = Mathf.Lerp(jogador.linearVelocity.z, velocidadeAntes, f);
            jogador.linearVelocity = new Vector3(jogador.linearVelocity.x, jogador.linearVelocity.y, z);

            yield return null;
        }

        aEmpurrar = null;
    }
}