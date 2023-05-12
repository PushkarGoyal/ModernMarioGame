using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootSquash : MonoBehaviour
{
    [SerializeField] Sprite thiefFlatSprite;
    [SerializeField] Sprite duckClosedSprite; // Reference To Closed Duck Sprite
    private Rigidbody2D playerRb; // Reference To Player RigidBody2D
    [SerializeField] UiManager uiRef; // Reference to UiManager Script
    [SerializeField] playerController playerControllerRef; // Reference to PlayerController Script



    private void Start()
    {
        playerRb = transform.parent.gameObject.GetComponent<Rigidbody2D>(); // Getting Rigidbody2D Component
    }

    // Checking Player Boot collision With water using trigger To handle Death

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("water"))
        {
            Debug.Log("Player In Contact With Water");
            Debug.Log("Player Died");
            playerControllerRef.PlayGameoverSound();
            playerController.isStop = true;
            playerController.playerAnim.SetBool("isDead", true);
            Invoke(nameof(HandlePlayerDeath), 1f);

        }
    }

    // Method To Handle Player Death

    public void HandlePlayerDeath()
    {


        Time.timeScale = 0;
        uiRef.RestartDialog.gameObject.SetActive(true);


    }

    // Checking Player Boot Collision With Enemies Head To Destroy Them

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("thief") && DotTest(transform, collision.transform, Vector2.down))
        {
            if (!playerController.isDead)
            {
                Debug.Log("Bounced Enemy Head");
                playerController.bootBump = true;
                playerRb.velocity = new Vector2(0f, 5f); // To Bounce The Player
                SpriteRenderer thiefSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                thiefSpriteRenderer.sprite = thiefFlatSprite; // Changing Thief Sprite
                collision.gameObject.GetComponent<thiefRun>().isDead = true;
                //thiefRun.isDead = true;
                StartCoroutine(dieEnemy(collision.gameObject));
            }


        }

        else
             if (collision.gameObject.CompareTag("duck") && DotTest(transform, collision.transform, Vector2.down))
        {

            if (!playerController.isDead)
            {
                Debug.Log("Bounced Duck Back");
                //duckRun.isDead = true;
                playerController.bootBump = true;
                playerRb.velocity = new Vector2(0f, 4f); // Adding Bounce To Player
                SpriteRenderer duckSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
                duckSpriteRenderer.sprite = duckClosedSprite; // Changing Duck sprite
                collision.gameObject.GetComponent<duckRun>().isDead = true;
                StartCoroutine(dieEnemy(collision.gameObject));
            }

        }

        else
             if (collision.gameObject.CompareTag("mace") && DotTest(transform, collision.transform, Vector2.down))
        {
            if (!playerController.isDead)
            {
                Debug.Log("Bounced Mace Back");
                //duckRun.isDead = true;
                playerController.bootBump = true;

                // Descaling Mace on Hit By Player Boot

                Vector2 maceScale = collision.gameObject.transform.localScale;
                maceScale.x -= 0.3f;
                maceScale.y -= 0.3f;
                collision.gameObject.transform.localScale = maceScale;

                playerRb.velocity = new Vector2(0f, 4f); // Adding Bounce To Player

                collision.gameObject.GetComponent<maceRun>().isDead = true;
                StartCoroutine(dieEnemy(collision.gameObject));
            }
        }

        else
             if (collision.gameObject.CompareTag("spike"))
        {
            if (playerController.islarge)
            {
                Debug.Log("Reducing Size On Spike Collision");
            }
            else
            {
                Debug.Log("Player Died");
                playerController.isStop = true;
                playerController.isDead = true;
                playerController.playerAnim.SetBool("isDead", true);
                //StartCoroutine(playerDeath(0.5f));
            }
        }


    }

    public static bool DotTest(Transform transform, Transform othertransform, Vector2 testDirection)
    {
        Vector2 direction = othertransform.position - transform.position;
        return Vector2.Dot(direction.normalized, testDirection) > 0.5f;
    }

    // Coroutine To Destroy Enemy After Particular Time Period

    IEnumerator dieEnemy(GameObject enemyObject)
    {
        yield return new WaitForSeconds(0.7f);
        Destroy(enemyObject);
        playerController.bootBump = false; // disabling Boot bump


    }




}
