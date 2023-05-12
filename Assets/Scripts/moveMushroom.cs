using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveMushroom : MonoBehaviour
{
    bool isDead = false;
    private Rigidbody2D mushroomRb;

    SpriteRenderer mushroomSprite;
    SpriteRenderer playerCSprite;

    private void Awake()
    {
        mushroomRb = GetComponent<Rigidbody2D>();
        mushroomSprite = GetComponent<SpriteRenderer>();

    }

    void Start()
    {

        StartCoroutine(BlinkMSprite(mushroomSprite));
        Vector2 vel = new Vector2(7f, 0f) * Time.deltaTime;
        mushroomRb.velocity = vel;

    }

    private void Update()
    {
        if (!isDead)
        {
            transform.position += new Vector3(0.5f, 0f) * Time.deltaTime;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collided with player");
            Destroy(gameObject);
            if (!playerController.islarge)
            {
                playerCSprite = collision.gameObject.GetComponent<SpriteRenderer>();
                //StartCoroutine(BlinkMSprite(playerCSprite));
                Vector2 playerScale = collision.gameObject.transform.localScale;
                playerScale.x = 0.3f;  // Scaling Player
                playerScale.y = 0.3f;
                collision.gameObject.transform.localScale = playerScale;
                playerController.islarge = true;
            }

        }
    }

    IEnumerator BlinkMSprite(SpriteRenderer spriteRenderer)
    {
        float time = 0f;

        while (time < 0.02f)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            time += Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("water"))
        {
            isDead = true;
            Invoke(nameof(DestroyMushroom), 0.5f);

        }
        else
            isDead = false;
    }
    private void DestroyMushroom()
    {
        Destroy(gameObject);
    }



}


