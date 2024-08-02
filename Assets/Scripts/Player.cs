using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject loseScreen;
    public Text healthDisplay;
    public Text timeDisplay; 

    public int health;
    public float speed;
    float input;
    public float moveSmoothing = 0.1f;
    public float stopDistance = 0.1f;

    float timeSpent = 0f;

    Animator anim;
    Rigidbody2D rb;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthDisplay.text = health.ToString();
    }

    void Update()
    {
        timeSpent += Time.deltaTime;
        timeDisplay.text = "Time: " + (int)timeSpent;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            float distanceToTouch = Mathf.Abs(touchPosition.x - transform.position.x);

            if (distanceToTouch > stopDistance)
            {
                Vector3 targetPosition = new Vector3(touchPosition.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmoothing);
                input = Mathf.Sign(touchPosition.x - transform.position.x);
            }
            else
            {
                input = 0f; 
            }
        }
        else
        {
            input = 0f;
        }

        if (input > 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else if (input < 0)
            transform.eulerAngles = new Vector3(0, 0, 0);

        anim.SetBool("isRunning", input != 0f);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    public void TakeDamage(int damageAmount)
    {
        source.Play();
        health -= damageAmount;
        healthDisplay.text = health.ToString();

        if (health <= 0)
        {
            loseScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
}
