using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // Position references
    Vector2 playerPosition;
    float movementX;

    [SerializeField] float runSpeed = 3f;

    // Boot Squash Script Reference

    [SerializeField] BootSquash bootRef;


    // Animator References
    public static Animator playerAnim;

    // RigidBody references

    Rigidbody2D rb;

    // Boolean Check References

    bool isrun = false;
    public static bool moveBack = false;
    public static bool isjump = false;
    public static bool islarge = false;
    public static bool isDead = false;
    bool isPause = false;

    bool isgrounded;

    public static bool isfire = false;

    public static bool isStop = false;

    public static bool bootBump = false;



    // Jump Speed Refrence

    [SerializeField] float jumpSpeed = 6f;


    // Sprite Refrences

    SpriteRenderer playerSprite;

    // Ui Manager Script Reference

    public UiManager uiRef;

    // Maintaining Coin Count

    int coinCount = 0;

    // Player Hand References

    [SerializeField] Transform playerLeftHand;

    [SerializeField] Transform playerRightHand;

    // FireBall Prefab Reference

    [SerializeField] GameObject fireBall;

    // Star Prefab Reference

    [SerializeField] GameObject star;

    bool createStarPower = true;

    // Clone Object Reference

    public static GameObject ball;

    // Fire Counter
    int counter = 0;

    Vector2 vel = Vector2.zero;

    // Player Boot Position vector

    Vector2 playerBootPos;

    // Ground Layer Mask

    public LayerMask groundLayerMask;

    Vector2 change;

    // Coin Audio Clip

    [SerializeField] AudioClip coinSound;

    // fireball Audio Clip

    [SerializeField] AudioClip fireSound;



    // Audio Manager Script Reference

    public AudioSource jumpSound;

    public AudioSource gameOverSound;

    public AudioClip powerUpSound;

    public AudioSource MainSound;


    // plane Prefab Reference

    [SerializeField] GameObject airPlane;

    bool create;

    [SerializeField] float planeCallTime = 30f;

    // Parallax Script Reference

    public parallaxScroll parallax;

    float move;

    Vector2 move_change;

    public bool isbrun;

    leftMove leftBtnRef;

    //rightMove1 rightBtnRef;



    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void Start()
    {
        isStop = false;
        isDead = false;
        islarge = false;
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        MainSound = Camera.main.GetComponent<AudioSource>();
        create = true;
        move = 0;

        leftBtnRef = GameObject.FindObjectOfType<leftMove>();
        InvokeRepeating(nameof(changeState), 0f, planeCallTime);




    }

    void changeState()
    {
        create = !create;
    }



    void Update()
    {

        movementX = Input.GetAxis("Horizontal"); // getting Horizontal Input
        move = leftBtnRef.setMovement(); // Left Right Button Input
        isrun = !Mathf.Approximately(movementX, 0f);
        isbrun = (move != 0);
        moveBack = movementX < 0 || move < 0;
        isjump = (Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.UpArrow)) || uiRef.jump;
        playerBootPos = transform.Find("playerBoot").transform.position;
        isgrounded = Physics2D.OverlapCircle(playerBootPos, 0.1f, groundLayerMask);


        playerPosition = transform.position;
        change = new Vector2(movementX * runSpeed, 0f);
        move_change = new Vector2(move * runSpeed, 0f);

        // Back To Main Menu

        if (Input.GetKey(KeyCode.Escape))
        {
            uiRef.BackToMenu();
        }

        // Pausing And UnPausing Game

        if (Input.GetKeyDown(KeyCode.P))
        {
            isPause = !isPause;
            uiRef.pauseGame(isPause);
        }

        // Linear Movement Logic


        float xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (isrun && !isStop)
        {


            float pos = playerPosition.x + change.x * Time.deltaTime;

            pos = Mathf.Clamp(pos, xMin + 0.5f, xMax);
            playerPosition.x = pos;

            transform.position = playerPosition;

        }


        if (isbrun && !isStop)
        {
            Debug.Log("Moving Player");
            float pos = playerPosition.x + move_change.x * Time.deltaTime;
            pos = Mathf.Clamp(pos, xMin + 0.5f, xMax);
            playerPosition.x = pos;
            transform.position = playerPosition;
        }


        if (moveBack)
            playerSprite.flipX = true;
        else
            playerSprite.flipX = false;


        playerAnim.SetBool("isRun", (isrun || isbrun));
        playerAnim.SetBool("isJump", isjump);

        // Jump Logic

        if (isgrounded)
        {

            if (isjump && !isStop)
            {
                // Playing Jump Sound
                jumpSound.volume = 0.5f;
                jumpSound.Play();

                vel.y = (!islarge ? jumpSpeed : jumpSpeed * 1.1f);
                rb.velocity = vel;
            }



        }


        if (isfire)
        {
            uiRef.handleFireUi(true);

            if (counter == 0)
            {
                Debug.Log("Starting Firing : " + counter);

                InvokeRepeating(nameof(FireNow), 0f, 0.5f);
                isfire = false;
            }


        }




    }

    private void FixedUpdate()
    {
        parallax.offset = transform.position.x;
    }

    private void LateUpdate()
    {
        if (create)
        {
            Invoke(nameof(createPlane), 1.5f);
            create = false;
        }



    }


    void createPlane()
    {
        Instantiate(airPlane);

    }

    void FireNow()
    {

        uiRef.firePowerSlider.value -= counter * Time.deltaTime;


        if (!moveBack)
        {
            ball = Instantiate(fireBall, playerLeftHand.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireSound, playerLeftHand.position);
        }
        else
        {
            ball = Instantiate(fireBall, playerRightHand.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(fireSound, playerRightHand.position);
        }

        counter++;

        if (counter >= 22)
        {
            CancelInvoke(nameof(FireNow));
            uiRef.handleFireUi(false);
            uiRef.firePowerSlider.value = 1;
            counter = 0;
        }
    }

    // Detecting Collision With Collectables And PowerUps

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mushroom"))  // Collision Detection With Mushroom
        {
            Debug.Log("Collision With Mushroom");
            Debug.Log("Player Large : " + islarge);
            Destroy(collision.gameObject);
            if (!islarge)
            {
                AudioSource.PlayClipAtPoint(powerUpSound, transform.position);
                islarge = true;
                StartCoroutine(BlinkSprite());
            }

        }
        else
             if (collision.gameObject.CompareTag("coin"))  // Collision Detection with coin
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            Destroy(collision.gameObject);
            coinCount += 1;
            uiRef.coinCountText.text = coinCount.ToString();
        }



    }

    // Detecting Collision With Enemies

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("thief"))
        {
            if (!bootBump)
            {
                // Stopping Thief Movement On Collision

                collision.gameObject.GetComponent<thiefRun>().isStop = true;


                if (islarge)
                {
                    reduceSize();

                }
                else
                {
                    isStop = true;

                    isDead = true;
                    playerAnim.SetBool("isDead", isDead);
                    StartCoroutine(playerDeath(1f));

                }
            }

        }

        else
             if (collision.gameObject.CompareTag("duck"))
        {


            // Stopping Duck Movement On Collision

            collision.gameObject.GetComponent<duckRun>().isStop = true;

            if (!bootBump)
            {
                if (islarge)
                {
                    reduceSize();

                }
                else
                {
                    Debug.Log("Player Died");

                    isStop = true;

                    isDead = true;
                    playerAnim.SetBool("isDead", isDead);
                    StartCoroutine(playerDeath(1f));

                }
            }



        }

        else
             if (collision.gameObject.CompareTag("mace"))
        {

            if (!bootBump)
            {

                if (islarge)
                {
                    reduceSize();

                }
                else
                {
                    Debug.Log("Player Died");

                    isStop = true;

                    isDead = true;
                    playerAnim.SetBool("isDead", isDead);
                    StartCoroutine(playerDeath(1f));

                }
            }

        }

        else
             if (collision.gameObject.CompareTag("spike"))
        {
            if (islarge)
            {
                reduceSize();

            }
            else
            {
                rb.velocity = new Vector2(0f, 3f);
                Debug.Log("Player Died");
                isStop = true;
                isDead = true;
                playerAnim.SetBool("isDead", isDead);
                StartCoroutine(playerDeath(0.5f));

            }
        }

        // Detecting collison With Flower PowerUp 

        else if (collision.gameObject.CompareTag("flower"))
        {
            Debug.Log("player Collided With Flower");
            Destroy(collision.gameObject);
            isfire = true;
            Debug.Log("Fire Set To True");
        }

        else
             if (collision.gameObject.name == "chest")
        {
            Debug.Log("Collision With Chest");
            if (createStarPower)
            {
                collision.gameObject.GetComponent<Animator>().SetBool("isOpen", true);
                Instantiate(star, collision.gameObject.transform);
                createStarPower = false;
                Invoke(nameof(changeStarPowerState), 5f);
            }


        }

        else
             if (collision.gameObject.CompareTag("dangerBall"))
        {


            Debug.Log("Collision With Danger Ball");

            if (islarge)
                reduceSize();
            else
            {
                isStop = true;
                isDead = true;
                playerAnim.SetBool("isDead", isDead);
                StartCoroutine(playerDeath(1.5f));
            }
        }



    }

    void changeStarPowerState()
    {
        createStarPower = true;
    }

    public void PlayGameoverSound()
    {
        MainSound.Stop();
        gameOverSound.Play();
    }

    void movePlayerBack()
    {
        transform.position = playerPosition + new Vector2(2f, 0f) * Time.deltaTime;
    }




    // Coroutine handling Player Death

    IEnumerator playerDeath(float time)
    {
        yield return new WaitForSeconds(time);
        PlayGameoverSound();
        bootRef.HandlePlayerDeath();
    }

    // Method To Reduce Size Of Player

    void reduceSize()
    {
        Vector2 playerScale = transform.localScale;
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;

        playerScale.x = scaleX - 0.1f;
        playerScale.y = scaleY - 0.1f;
        transform.localScale = playerScale;
        islarge = false;
    }

    // Coroutine To Blink Player                                                                                                                    

    IEnumerator BlinkSprite()
    {


        float time = 0f;

        while (time < 0.02f)
        {
            playerSprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            time += Time.deltaTime;
        }
        Vector2 playerScale = transform.localScale;
        playerScale.x = 0.3f;  // Scaling Player
        playerScale.y = 0.3f;
        transform.localScale = playerScale;

    }

}







