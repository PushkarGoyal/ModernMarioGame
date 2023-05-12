using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dangerBall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {

            Invoke(nameof(DestroyBall), 0.5f);

        }


    }



    void DestroyBall()
    {
        Destroy(gameObject);
    }

}
