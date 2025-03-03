using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPellet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            Controller.instance.InscrementScore();
        }
    }
}
