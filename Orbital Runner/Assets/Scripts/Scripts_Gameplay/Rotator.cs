using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationDegreesPerSecond = new (15, 30, 45);
    void Update()
    {
        transform.Rotate (rotationDegreesPerSecond * Time.deltaTime);
    }
}
