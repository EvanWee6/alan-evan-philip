using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceText : MonoBehaviour
{
    public float bounceHeight = 100f;
    public float bounceSpeed = 2.0f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
