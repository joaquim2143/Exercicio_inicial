using UnityEngine;
using UnityEngine.InputSystem;

public class Mover1 : MonoBehaviour
{
    public Rigidbody jogador;
    void Update()
    {
        jogador.AddForce(0, 0, 8);
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            jogador.AddForce(-4, 0, 0);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            jogador.AddForce(4, 0, 0);
        }
    }  
}
