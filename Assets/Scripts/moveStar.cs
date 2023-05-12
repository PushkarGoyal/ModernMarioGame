using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveStar : MonoBehaviour
{



    void Update()
    {
        transform.position = transform.position + new Vector3(3f, 1f) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Star Collided With Player");
            Destroy(gameObject);
            playerController.isfire = true;
        }

        else if (collision.gameObject.CompareTag("water"))
        {
            Debug.Log("Star Collided with Water");
            Destroy(gameObject);
        }

        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
        }
    }

}
