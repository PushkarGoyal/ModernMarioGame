using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlower : MonoBehaviour
{
    bool isDead = false;
    private Rigidbody2D flowerRb;
    bool direction = true;

    private void Awake()
    {
        flowerRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

        StartCoroutine(BlinkFlower());
        Vector2 vel = new Vector2(10f, 0f) * Time.deltaTime;
        flowerRb.velocity = vel;
    }

    private void Update()
    {
        if (!isDead)
        {
            if (direction)
                transform.position += new Vector3(0.5f, 0f) * Time.deltaTime;
            else
                transform.position -= new Vector3(0.5f, 0f) * Time.deltaTime;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("pipe"))
            direction = !direction;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("water"))
        {
            isDead = true;
            Invoke(nameof(Destroyflower), 0.5f);

        }
        else
            isDead = false;
    }
    private void Destroyflower()
    {
        Destroy(gameObject);
    }

    IEnumerator BlinkFlower()
    {

        SpriteRenderer flowerSprite = GetComponent<SpriteRenderer>();

        float time = 0f;

        while (time < 0.02f)
        {
            flowerSprite.color = Color.green;
            yield return new WaitForSeconds(0.1f);
            flowerSprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            time += Time.deltaTime;
        }

    }

}





