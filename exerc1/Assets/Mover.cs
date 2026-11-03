using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    public Rigidbody jogador;
    public float maximo = 3f;
    public float crescimento = 0.5f;
    public AudioSource SomSalto;
    public AudioClip Salto;
    [SerializeField, Range(0f, 1f)] private float volume;

    [Header("Salto")]
    [Tooltip("Impulso do salto. Sobe para saltar mais alto.")]
    public float forcaSalto = 7.2f;

    [Header("Inclinacao (Android)")]
    [Tooltip("Quanto e preciso inclinar antes de comecar a andar. Evita deriva com o telemovel parado.")]
    public float zonaMorta = 0.05f;
    [Tooltip("Inclinacao (em g) que corresponde a forca maxima. Quanto maior, mais tens de inclinar.")]
    public float inclinacaoMaxima = 0.4f;
    [Tooltip("Forca ao sair da zona morta (0 a 1). Quanto maior, mais rapido arranca.")]
    [Range(0f, 1f)] public float forcaMinima = 0.35f;
    [Tooltip("Forca lateral maxima.")]
    public float forcaLateral = 18f;
    [Tooltip("Velocidade lateral maxima (unidades por segundo). Limita a rapidez a virar.")]
    public float velocidadeMaxima = 6f;
    [Tooltip("Rapidez com que perde a velocidade lateral no chao quando nao inclinas.")]
    public float paragemLateral = 30f;
    [Tooltip("Rapidez com que perde a velocidade lateral no ar. Maior = para mais depressa.")]
    public float paragemLateralAr = 80f;
    [Tooltip("Desligado (recomendado): eixo X, horizontal do ecra. Ligado: eixo Y, vertical do ecra.")]
    public bool usarEixoY = false;
    [Tooltip("Inverte o sentido da inclinacao, se estiver ao contrario.")]
    public bool inverter = false;

    private bool noChao;
    private bool pedidoSalto;
    private float neutro;

    void Awake()
    {
        if (Accelerometer.current != null)
            InputSystem.EnableDevice(Accelerometer.current);
    }

    void Start()
    {
        CalibrarInclinacao();
    }

    public void CalibrarInclinacao()
    {
        if (Accelerometer.current == null) return;

        Vector3 a = Accelerometer.current.acceleration.ReadValue();
        neutro = usarEixoY ? a.y : a.x;
    }

    private void OnCollisionEnter(Collision quem)
    {
        if (quem.gameObject.tag == "Bom")
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    void Update()
    {
        if (noChao && Saltou())
            pedidoSalto = true;
    }

    void FixedUpdate()
    {
        jogador.AddForce(0, 0, 4);

        float eixo = EixoHorizontal();

        if (Mathf.Abs(eixo) > 0.01f)
        {
            jogador.AddForce(eixo * forcaLateral, 0, 0);
        }
        else
        {
            float paragem = noChao ? paragemLateral : paragemLateralAr;
            Vector3 parado = jogador.linearVelocity;
            parado.x = Mathf.MoveTowards(parado.x, 0f, paragem * Time.fixedDeltaTime);
            jogador.linearVelocity = parado;
        }

        if (Mathf.Abs(jogador.linearVelocity.x) > velocidadeMaxima)
        {
            Vector3 v = jogador.linearVelocity;
            v.x = Mathf.Sign(v.x) * velocidadeMaxima;
            jogador.linearVelocity = v;
        }

        if (noChao && pedidoSalto)
        {
            pedidoSalto = false;

            if (SomSalto != null)
                SomSalto.PlayOneShot(Salto, volume);

            jogador.AddForce(0, forcaSalto, 0, ForceMode.Impulse);
        }
    }

    float EixoHorizontal()
    {
        float eixo = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) eixo -= 1f;
            if (Keyboard.current.dKey.isPressed) eixo += 1f;
        }

        if (eixo == 0f)
            eixo = EixoInclinacao();

        return eixo;
    }

    float EixoInclinacao()
    {
        if (Accelerometer.current == null) return 0f;

        Vector3 a = Accelerometer.current.acceleration.ReadValue();
        float valor = (usarEixoY ? a.y : a.x) - neutro;

        if (inverter) valor = -valor;

        float magnitude = Mathf.Abs(valor);
        if (magnitude < zonaMorta) return 0f;

        float maximo = Mathf.Max(inclinacaoMaxima, zonaMorta + 0.01f);
        float forca = Mathf.Lerp(forcaMinima, 1f, Mathf.InverseLerp(zonaMorta, maximo, magnitude));

        return Mathf.Sign(valor) * Mathf.Clamp01(forca);
    }

    bool Saltou()
    {
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame)
            return true;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            return true;

        return false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = true;
        if (collision.gameObject.CompareTag("Mau"))
        {
            float max = transform.localScale.x + crescimento * Time.deltaTime;
            max = Mathf.Min(max, maximo);
            transform.localScale = Vector3.one * max;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = false;
    }
}
