using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject missile;
    public GameObject turret;
    public float speed = 135.0f;
    AudioSource shootSound;

    private void Start()
    {
        shootSound = GetComponent<AudioSource>();

        if (LevelManager.gameDifficulty == Difficulty.Easy)
        {
            speed = 150f;
        }
    }

    private void FixedUpdate()
    {
        float aroundEarth = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        transform.Rotate(0f, 0f, -aroundEarth);
    }

    private void Update()
    {
        if (GameManager.gameOver)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if (Input.GetButtonDown("Fire"))
            {
                Vector3 turretPosition = turret.transform.position;
                Instantiate(missile, turretPosition, transform.rotation);
                shootSound.Play();
            }
        }
        
    }
}
