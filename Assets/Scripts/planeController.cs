using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeController : MonoBehaviour
{

    float offset;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dangerBallPrefab;



    Vector2 playerPosition;

    bool create;

    float minX;

    float posX;

    float x = 0.2f;



    void Start()
    {

        minX = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        Vector2 pos = transform.position;
        pos.x = minX;
        transform.position = pos;

        Invoke(nameof(setState), 0.5f);
    }

    void setState()
    {
        create = true;
    }


    void Update()
    {
        if (x < 1f)
        {
            //Debug.Log(posX);
            posX = Camera.main.ViewportToWorldPoint(new Vector3(x, 0.9f, 0f)).x;
            Vector2 pos = transform.position;
            pos.x = posX;
            transform.position = pos;
            if (create)
            {
                GameObject ball = Instantiate(dangerBallPrefab);
                ball.transform.position = transform.position;
                Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                ballRb.velocity = Vector2.zero;
                ballRb.freezeRotation = true;

                create = false;

            }

            x += (0.2f * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }


    }






}
