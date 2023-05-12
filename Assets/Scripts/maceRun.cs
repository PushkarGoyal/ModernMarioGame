using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maceRun : MonoBehaviour
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

                transform.position += new Vector3(-1f, 0f) * Time.deltaTime;
            }

            else
            {

                transform.position += new Vector3(1f, 0f) * Time.deltaTime;
            }

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            isDead = true;
            Invoke(nameof(DestroyMace), 0.5f);

        }
        else
            isDead = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("spike"))
        {

            direction = !direction;
        }

    }

    private void DestroyMace()
    {
        Destroy(gameObject);
    }



}

