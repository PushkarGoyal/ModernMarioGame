using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mystery : MonoBehaviour
{

    [SerializeField] Sprite mysteryChangesprite;
    [SerializeField] GameObject[] powerGifts;
    bool createPower = true;

    private void Start()
    {
        createPower = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Player") && collision.GetContact(0).normal.y > 0)
        if (collision.gameObject.CompareTag("Player") && BootSquash.DotTest(collision.transform, transform, Vector2.up))
        {
            Debug.Log("Player Collided With Mystery box ");
            Debug.Log("cREATE Power : " + createPower);

            if (createPower)
            {
                int randomIndex = Random.Range(0, powerGifts.Length);
                GameObject gift = powerGifts[randomIndex];

                GameObject poweruP = Instantiate(gift, transform.position, Quaternion.identity);

                // Disabling Collider

                poweruP.GetComponent<Collider2D>().enabled = false;

                StartCoroutine(enableCollider(poweruP));


                GetComponent<SpriteRenderer>().sprite = mysteryChangesprite;
                createPower = false;
            }

        }

    }


    IEnumerator enableCollider(GameObject powerUp)
    {
        yield return new WaitForSeconds(0.5f);
        powerUp.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    //IEnumerator changeColliderState(GameObject powerUp)
    //{
    //    yield return new WaitForSeconds(0.01f);
    //    Debug.Log("Making Trigger False");

    //    if (powerUp.gameObject.CompareTag("flower"))
    //        powerUp.GetComponent<BoxCollider2D>().isTrigger = false;
    //    else
    //        powerUp.GetComponent<CircleCollider2D>().isTrigger = false;
    //}


}
