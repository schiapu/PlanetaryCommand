using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour {
    public GameObject planet;
    Rigidbody2D rb;
    public float speed = 100f;
    public GameObject player;
    public GameObject earthExplosion;
    public AudioClip earthDestroySound;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

        if (LevelManager.gameDifficulty == Difficulty.Hard)
        {
            speed = 125f;
        }
    }

    private void FixedUpdate()
    {
        Vector3 planetPosition = planet.transform.position;
        Vector3 planetDirection = (planetPosition - transform.position).normalized;
        rb.velocity = planetDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Asteroid(Clone)" && collision.gameObject.name != "Asteroid")
        {
            if (collision.gameObject.name == player.name)
            {
                GameManager.gameOver = true;
                AudioSource.PlayClipAtPoint(earthDestroySound, this.transform.position);
                Instantiate(earthExplosion, planet.transform.position, planet.transform.rotation);
            }

            Destroy(gameObject);
        }
           
    }
}
