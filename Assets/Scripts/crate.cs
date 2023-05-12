using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crate : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            Debug.Log("Crate Collision with Water");
            Invoke(nameof(destroyCrate), 1f);
        }

    }

    void destroyCrate()
    {
        Destroy(gameObject);
        Debug.Log("crate Has Been Destroyed");
    }
}
