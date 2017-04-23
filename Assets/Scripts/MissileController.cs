using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {

    public float speed = 5f;

    private void FixedUpdate()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.asteroidsDestroyed += 1;
        Destroy(collision.gameObject);
        Destroy(this.gameObject);
    }

}
