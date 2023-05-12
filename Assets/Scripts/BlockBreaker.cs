using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BlockBreaker : MonoBehaviour
{

    [SerializeField] Sprite blockChangesprite;
    [SerializeField] GameObject flowerPower;

    [SerializeField] AudioClip blockSound;

    //[SerializeField] GameObject block;
    bool createPower = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && collision.GetContact(0).normal.y > 0)
        {

            if (createPower)
            {
                AudioSource.PlayClipAtPoint(blockSound, collision.GetContact(0).point);
                Instantiate(flowerPower, gameObject.transform.Find("brickTop").transform.position, Quaternion.identity);

                // Adjusting Box Collider Size Based On Sprite

                gameObject.GetComponent<SpriteRenderer>().sprite = blockChangesprite;
                Vector2 spriteSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
                gameObject.GetComponent<BoxCollider2D>().size = spriteSize;

            }
            createPower = false;


        }
    }
}
