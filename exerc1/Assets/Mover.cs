using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    public Rigidbody jogador;
    private bool noChao;
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
            jogador.AddForce(0, 140, 0);
        }

    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
            noChao = false;
    }
}
