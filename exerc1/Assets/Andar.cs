using UnityEngine;

public class Andar : MonoBehaviour
{
    public Rigidbody variavel;
    void Start()
    {
        variavel.AddForce(0, 0, 0);
    }
    void Update()
    {
        variavel.AddForce(0, 0, 8);
    }
}
