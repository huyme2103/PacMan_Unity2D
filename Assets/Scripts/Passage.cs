
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Passage : MonoBehaviour
{
    [SerializeField] private Transform connection;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = new Vector3(connection.position.x, connection.position.y, other.transform.position.z);
        }
    }
}
