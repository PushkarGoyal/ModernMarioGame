using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script To Handle FireBall Movement And Collision With Enemies

public class Fire : MonoBehaviour
{
    // Rigidbody2D Reference
    private Rigidbody2D fireBallRb;

    // Throw Speed
    [SerializeField] float throwSpeed;
    bool front = false;

    void Start()
    {
        front = !playerController.moveBack;
        fireBallRb = GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        transform.position += (front ? new Vector3(5.5f, 0, 0) : new Vector3(-5.5f, 0, 0)) * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("thief"))
        {
            Debug.Log("Damage Made To Thief");
            Destroy(collision.gameObject);

        }
        else
             if (collision.gameObject.CompareTag("duck"))
        {
            Debug.Log("Damage Made To Duck");
            Destroy(collision.gameObject);
        }


        Destroy(gameObject);
    }

}
