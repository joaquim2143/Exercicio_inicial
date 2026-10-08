using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RepetirQueda : MonoBehaviour
{
    public float segundos;
    Vector3 posInicial;
    Quaternion rot;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.GetPositionAndRotation(out posInicial, out rot);
        InvokeRepeating(nameof(Resetar), segundos, segundos);
    }

    void Resetar()
    {
        rb.linearVelocity = rb.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(posInicial, rot);
    }
}