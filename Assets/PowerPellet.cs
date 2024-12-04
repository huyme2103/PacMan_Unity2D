using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    // Khi Pac-Man va chạm với mồi to
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Kiểm tra nếu Pac-Man va chạm
        {
            Pacman pacman = other.GetComponent<Pacman>();
            if (pacman != null)
            {
                pacman.ActivatePowerUp();  // Kích hoạt trạng thái mạnh mẽ cho Pac-Man
            }

            // Sau khi Pac-Man ăn mồi to, mồi to sẽ biến mất
            Destroy(gameObject);
        }
    }
}
