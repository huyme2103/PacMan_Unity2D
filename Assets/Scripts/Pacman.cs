using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Movement))]
public class Pacman : MonoBehaviour
{
    [SerializeField]
    private bool isPoweredUp = false;
    private float powerUpDuration = 5f;
    // private AnimatedSprite deathSequence;
    private SpriteRenderer spriteRenderer;
    private CircleCollider2D circleCollider;
    private Movement movement;
    private Color originalColor;

    public bool IsPoweredUp
    {
        get { return isPoweredUp; }
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        movement = GetComponent<Movement>();

    }
    private void Start()
    {
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        // Set the new direction based on the current input
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }

        // Rotate pacman to face the movement direction
        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        enabled = true;
        spriteRenderer.enabled = true;
        circleCollider.enabled = true;
        // deathSequence.enabled = false;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void DeathSequence()
    {
        enabled = false;
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
        movement.enabled = false;
        // deathSequence.enabled = true;
        //deathSequence.Restart();
    }

    // Phương thức này sẽ được gọi khi Pac-Man ăn mồi to
    public void ActivatePowerUp()
    {
        if (!isPoweredUp)  // Nếu chưa kích hoạt mồi to
        {
            StartCoroutine(PowerUpCoroutine());  // Bắt đầu thời gian mạnh mẽ
        }
    }

    // Coroutine để đếm ngược thời gian mạnh mẽ
    IEnumerator PowerUpCoroutine()
    {
        isPoweredUp = true;  // Bật trạng thái mạnh mẽ
        Debug.Log("Pac-Man is now powered up!");
        spriteRenderer.color = Color.red;
        // Đợi 5 giây (hoặc thời gian bạn định trước)
        yield return new WaitForSeconds(powerUpDuration);

        // Kết thúc trạng thái mạnh mẽ
        isPoweredUp = false;
        spriteRenderer.color = originalColor;
        Debug.Log("Pac-Man power-up has ended.");
    }

    // Xử lý va chạm với Ghost

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            NPC_Controller ghostController = collision.gameObject.GetComponent<NPC_Controller>();
            if (isPoweredUp && ghostController != null)
            {
                ghostController.DestroyGhost();
            }
            else
            {
                Controller.instance.DecreaseLife();

                // Destroy(collision.gameObject);\
                if (Controller.instance.GameOver)
                {
                    Destroy(gameObject);
                }

            }
        }
    }
}
