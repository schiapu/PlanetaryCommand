using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour {

    public float speed = 100.0f;
    public float rotationSpeed = 5f;

    Rigidbody2D rb;
    public GameObject planet;
    public GameObject player;
    public GameObject asteroidExplosion;
    public GameObject earthExplosion;
    public AudioClip destroySound;
    public AudioClip earthDestroySound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (LevelManager.gameDifficulty == Difficulty.Hard)
        {
            speed = 125f;
        }
        if (LevelManager.gameDifficulty == Difficulty.Easy)
        {
            speed = 75f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 planetPosition = planet.transform.position;
        Vector3 planetDirection = (planetPosition - transform.position).normalized;
        rb.velocity = planetDirection * speed * Time.deltaTime;
        transform.Rotate(0f, 0f, rotationSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != this.gameObject.name && collision.gameObject.name != player.gameObject.name && collision.gameObject.name != "Beam(Clone)" && collision.gameObject.name != "Beam")
        {
            if (collision.gameObject.name == planet.name)
            {
                GameManager.gameOver = true;
                AudioSource.PlayClipAtPoint(earthDestroySound, this.transform.position);
                Instantiate(earthExplosion, planet.transform.position, planet.transform.rotation);
            }
            else
            {
                AudioSource.PlayClipAtPoint(destroySound, this.transform.position);
            }

            Instantiate(asteroidExplosion,transform.position,transform.rotation);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        
    }
}
