using UnityEngine;
public class follow : MonoBehaviour
{
    public Transform Sphere;
    public Vector3 distancia;
    void Update()
    {
        transform.position = Sphere.position + distancia;
    }
}
