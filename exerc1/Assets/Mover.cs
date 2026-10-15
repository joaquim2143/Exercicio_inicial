using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    public Rigidbody jogador;
    public float maximo = 3f;
    public float crescimento = 0.5f;
    private bool noChao;
    private void OnCollisionEnter(Collision quem)
    {
        if (quem.gameObject.tag == "Bom")
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }
    void FixedUpdate()
    {
        jogador.AddForce(0,0,4);
        if (Keyboard.current.aKey.isPressed)
        {
            jogador.AddForce(-16, 0, 0);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            jogador.AddForce(16, 0, 0);
        }
        if (Keyboard.current.spaceKey.isPressed && noChao)
        {
            jogador.AddForce(0, 180, 0);
        }

    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = true;
        if (collision.gameObject.CompareTag("Mau"))
        {
            float max = transform.localScale.x + crescimento*Time.deltaTime;
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
