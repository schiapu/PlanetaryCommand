using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour {

    public float duration = 1.0f;
    float totalDuration;
	// Use this for initialization
	void Start () {
        totalDuration = duration;
    }
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;

        Color expColor = gameObject.GetComponent<SpriteRenderer>().color;
        expColor.a = duration / totalDuration;
        gameObject.GetComponent<SpriteRenderer>().color = expColor;

        if (duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
