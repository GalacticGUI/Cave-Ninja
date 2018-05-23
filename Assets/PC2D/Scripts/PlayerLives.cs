using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerLives : MonoBehaviour
{
    public GameObject exit;
    public PlatformerMotor2D motor;
    public PlayerController2D controller;
    public Transform player;
    public int maxLives = 3;
    private int currentLives;
    public int maxHealth = 3;
    private int currentHealth;
    public float fallDistanceTakeDamage = 10f;
    public float hurtTimer = 1.5f;
    private float timeHurt;
    public float knockbackAmount = 7.5f;
    public float knockbackTimer = 0.69f;
    private float timeKnockback;

    private bool isHurt = false;
    private bool isDead = false;
    private bool hasKey = false;
    private bool isExitOpen = false;
    private Vector2 waypoint;

    public Sprite[] lifeSprites;
    public Image lifeUI;
    public Sprite[] heartSprites;
    public Image heartUI;
    public Sprite keySprite;
    public Image keyUI;
    public Text keyText;
    public Text exitText;

    private void Awake() {
        FindObjectOfType<AudioManager>().PlaySound("Music_Level_1");
    }

    // Use this for initialization
    void Start() {
        exit.GetComponent<Renderer>().enabled = false;
        motor.onLanded += OnFallFinished;
        GetComponent<Rigidbody2D>().isKinematic = true;
        currentLives = maxLives;
        currentHealth = maxHealth;
        motor = GetComponent<PlatformerMotor2D>();
        waypoint = player.position;
    }
        
    // handle fall damage
    public void OnFallFinished() {
        if (motor.amountFallen > fallDistanceTakeDamage) {
            FindObjectOfType<AudioManager>().PlaySound("Player_Take_Damage");
            if (!isHurt) {
                timeHurt = Time.time;
                currentHealth--;
                isHurt = true;
            }
        }
    }

    // UPDATE is called once per frame
    void Update() {
        if (!PauseMenu.isPaused) {
            if (hasKey) {
                keyText.text = "x1";
            }
            else {
                keyText.text = "x0";
            }

            // ensure health never goes below 0 to avoid index out of bounds on sprite array
            if (currentHealth < 0) {
                currentHealth = 0;
            }
            heartUI.sprite = heartSprites[currentHealth];

            // ensure lives never goes below 0 to avoid index out of bounds on sprite array
            if (currentLives < 0) {
                currentLives = 0;
            }
            lifeUI.sprite = lifeSprites[currentLives];

            // added the ability to teleport the player via keypress because for some reason the player...
            // will randomly get stuck in a collider (only happens on slopes, after adding in moving platforms)
            if (Input.GetButtonDown(PC2D.Input.RESET)) {
                GoToWaypoint();
            }

            // knockback cooldown timer
            if (controller.knockback) {
                if (Time.time - timeKnockback > knockbackTimer) {
                    controller.knockback = false;
                }
            }

            // player damage taken cooldown timer
            if (isHurt) {
                if (Time.time - timeHurt >= hurtTimer) {
                    isHurt = false;
                }
            }

            // check if below world e.g. fell into water or health is 0
            if (player.transform.position.y <= -7.5 || currentHealth <= 0) {
                if (!isDead) {
                    isDead = true;
                    currentHealth = 0;
                    currentLives--;
                    motor.velocity = Vector2.zero;
                    if (currentLives > 0) {
                        currentHealth = maxHealth;
                        StartCoroutine(BackToStart());
                    }
                    else {
                        //else { GAME OVER }
                        StartCoroutine(ReloadLevel());
                    }
                }
            }
        }        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Key") {
            hasKey = true;
            FindObjectOfType<AudioManager>().PlaySound("Pickup_Key");
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Health") {
            if (currentHealth < maxHealth) {
                currentHealth++;
                FindObjectOfType<AudioManager>().PlaySound("Pickup_Health");
                Destroy(other.gameObject);
            }                
        }
        if (other.gameObject.tag == "Trap" || other.gameObject.tag == "Enemy") {
            timeKnockback = Time.time;
            controller.knockback = true;
            FindObjectOfType<AudioManager>().PlaySound("Player_Take_Damage");

            FindObjectOfType<AudioManager>().PlaySound("Wilhelm_Scream");   // just because..!

            if (motor.transform.position.x > other.transform.position.x) {
                motor.velocity = new Vector2(knockbackAmount, knockbackAmount / 1.5f);
            }
            else {
                motor.velocity = new Vector2(-knockbackAmount, knockbackAmount / 1.5f);
            }                
            if (!isHurt) {
                timeHurt = Time.time;
                currentHealth--;
                isHurt = true;
            } 
        }
        if (other.gameObject.tag == "Exit") {
            if (hasKey) {
                FindObjectOfType<AudioManager>().PlaySound("Exit_Opening");
                exit.GetComponent<Renderer>().enabled = true;
                hasKey = false;
                isExitOpen = true;
            }
            if (isExitOpen) {
                if (Input.GetButtonDown(PC2D.Input.INTERACT)) {
                    StartCoroutine(ReloadLevel());
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Exit") {
            if (isExitOpen) {
                exitText.text = "Press 'Interact' to exit.";
                if (Input.GetButtonDown(PC2D.Input.INTERACT)) {
                    StartCoroutine(ReloadLevel());
                }
            }
            else {
                exitText.text = "You need a key to unlock the exit.";
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Exit") {
            exitText.text = "";
        }
    }

    // teleports the player to the starting position without a delay
    void GoToWaypoint() {
        player.position = waypoint;
    }

    // teleports the player to the starting position after a slight delay
    IEnumerator BackToStart() {
        yield return new WaitForSeconds(1.5f);
        isDead = false;
        player.position = waypoint;
    }

    // reloads the scene after a slight delay, resetting player lives and health to max
    IEnumerator ReloadLevel() {  
        yield return new WaitForSeconds(1.5f);
        isDead = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    //void ShakeCamera()
    //{
    //    CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(2.43f, 1f, 0.1f, 2f);
    //    c.PositionInfluence = new Vector3(0.25f, 0.25f, 0.25f);
    //    c.RotationInfluence = new Vector3(1f, 1f, 1f);
    //}

}


