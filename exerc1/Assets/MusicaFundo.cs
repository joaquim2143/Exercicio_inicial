using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicaFundo : MonoBehaviour
{
    public AudioSource fonteMusica;
    public AudioClip[] musicas;
    [SerializeField, Range(0f, 1f)] private float[] volumes;
    int atual;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void AoIniciarJogo()
    {
        SceneManager.LoadScene(0);
    }

    void OnValidate()
    {
        if (musicas == null) return;

        if (volumes == null || volumes.Length != musicas.Length)
        {
            float[] novo = new float[musicas.Length];

            for (int i = 0; i < novo.Length; i++)
            {
                novo[i] = (volumes != null && i < volumes.Length) ? volumes[i] : 1f;
            }

            volumes = novo;
        }
    }

    void Start()
    {
        atual = Random.Range(0, musicas.Length);

        fonteMusica.clip = musicas[atual];
        fonteMusica.loop = true;
        fonteMusica.Play();
    }

    void Update()
    {
        fonteMusica.volume = (atual < volumes.Length) ? volumes[atual] : 1f;

        if (Time.timeScale == 0f)
        {
            if (fonteMusica.isPlaying)
                fonteMusica.Pause();
        }
        else
        {
            fonteMusica.UnPause();
        }
    }
}