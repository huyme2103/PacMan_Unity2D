
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Tốc độ di chuyển
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        // Lấy component Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lấy đầu vào từ bàn phím cho trục ngang và trục dọc
        float moveInputHorizontal = Input.GetAxis("Horizontal");
        float moveInputVertical = Input.GetAxis("Vertical");

        // Tạo vector di chuyển từ đầu vào
        moveDirection = new Vector2(moveInputHorizontal, moveInputVertical).normalized;

        // Cập nhật vận tốc cho nhân vật dựa trên vector di chuyển và tốc độ
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Pellet")
    //    {
    //        Destroy(collision.gameObject);
    //        Debug.Log("Destroy");
    //        Controller.instance.InscrementScore();
    //    }
    //}
}
