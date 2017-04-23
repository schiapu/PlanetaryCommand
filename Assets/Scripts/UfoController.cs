using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UfoController : MonoBehaviour {
    public float speed = 100f;
    Rigidbody2D rb;
    Vector2 direction;
    int startPosition = 0;
    public float fireTime = 2f;
    float fireTimeLeft = 2f;
    public GameObject beam;
    AudioSource beamSound;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        beamSound = GetComponent<AudioSource>();

        Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (viewPosition.x < 0)
        {
            startPosition = 0;
            direction = Vector2.right;
        }
        else if(viewPosition.x > 1)
        {
            startPosition = 1;
            direction = Vector2.left;
        }
        else if (viewPosition.y < 0)
        {
            startPosition = 2;
            direction = Vector2.up;
        }
        else if (viewPosition.y > 1)
        {
            startPosition = 3;
            direction = Vector2.down;
        }

        if (LevelManager.gameDifficulty == Difficulty.Hard)
        {
            fireTime = 1.5f;
            fireTimeLeft = fireTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void Update()
    {
        Vector3 viewPosition = Camera.main.WorldToViewportPoint(transform.position);
        switch (startPosition)
        {
            case 0:
                if (viewPosition.x > 1.1)
                {
                    Destroy(gameObject);
                }
                break;
            case 1:
                if (viewPosition.x < -0.1)
                {
                    Destroy(gameObject);
                }
                break;
            case 2:
                if (viewPosition.y > 1.1)
                {
                    Destroy(gameObject);
                }
                break;
            default:
                if (viewPosition.y < -0.1)
                {
                    Destroy(gameObject);
                }
                break;
        }

        fireTimeLeft -= Time.deltaTime;
        if (fireTimeLeft < 0)
        {
            if (GameManager.CoinToss())
            {
                Instantiate(beam, transform.position, transform.rotation);
                beamSound.Play();
            }
            fireTimeLeft = fireTime;
        }
        
    }
}
