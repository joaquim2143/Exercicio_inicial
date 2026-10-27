using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chegada1 : MonoBehaviour
{
    public GameObject Ganhou;
    public string TagPlayer = "Player";
    public bool pararJogo = true;
    public AudioSource SomGanhou;
    public AudioClip Ganha;
    [SerializeField, Range(0f, 1f)] private float volume;
    public string proximaCena;
    public bool mudarAutomaticamente = false;
    public float tempoEspera = 3f;

    void OnTriggerEnter(Collider outro)
    {
        if (!outro.CompareTag(TagPlayer)) return;

        SomGanhou.PlayOneShot(Ganha, volume);
        Ganhou.SetActive(true);

        if (pararJogo)
            Time.timeScale = 0f;

        if (mudarAutomaticamente)
            StartCoroutine(EsperarEMudar());
    }

    IEnumerator EsperarEMudar()
    {
        yield return new WaitForSecondsRealtime(tempoEspera);
        IrParaProximaCena();
    }

    public void IrParaProximaCena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(proximaCena);
    }
}