using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script To Handle Thief(Enemy) Movement And Collision

public class thiefRun : MonoBehaviour
{

    // Boolean Check References

    public bool isDead = false;
    public bool isStop = false;
    bool direction = false;




    private void Update()
    {
        if (!isDead && !isStop)
        {
            if (!direction)
                transform.position += new Vector3(-1f, 0f) * Time.deltaTime;
            else
                transform.position += new Vector3(1f, 0f) * Time.deltaTime;
        }
        else
        {
            Debug.Log("Not moving");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe"))
        {
            direction = !direction;
        }
        else if (collision.gameObject.CompareTag("flower"))
        {
            Destroy(collision.gameObject);
        }
        else
             if (collision.gameObject.CompareTag("spike"))
        {
            direction = !direction;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            isDead = true;
            Invoke(nameof(Destroythief), 0.5f);

        }
        else
            isDead = false;
    }

    private void Destroythief()
    {
        Destroy(gameObject);
    }


}
