using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class AppleTree : MonoBehaviour {
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float chanceToChangeDirection;

    // Rate at which Apple will be instantiated
    public float secondsBetweenAppleDrop;

    void Start() {
        // Dropping apples every second
        Invoke ("DropApple", 2f);
    }

    void DropApple() {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrop);
    }

    void Update() {
        // Basic Movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        // Changing Direction
        if (pos.x < -leftAndRightEdge) {
            speed = Mathf.Abs(speed); // Move right
        } else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed); // Move left
        }
    }

    void FixedUpdate() {
        // Changing Direction Randomly is now time-based because of FixedUpdate()
        if (Random.value < chanceToChangeDirection) {
            speed *= -1; // Change direction
        }
    }
}
