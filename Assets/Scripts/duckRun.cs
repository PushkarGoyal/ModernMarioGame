using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckRun : MonoBehaviour
{

    public bool isDead = false;
    public bool isStop = false;
    bool direction = false;




    private void Update()
    {
        if (!isDead && !isStop)
        {


            if (!direction)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                transform.position += new Vector3(-1f, 0f) * Time.deltaTime;
            }

            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                transform.position += new Vector3(1f, 0f) * Time.deltaTime;
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            isDead = true;
            Invoke(nameof(Destroyduck), 0.5f);

        }
        else
            isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe"))
        {

            direction = !direction;
        }

    }

    private void Destroyduck()
    {
        Destroy(gameObject);
    }



}
