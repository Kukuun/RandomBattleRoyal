using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    - Replace collision detection with ray casting.
    - Make particles for muzzle flash.
*/

public class Projectile : MonoBehaviour {
    private float lifetime;
    private float timer;
    private int travelingSpeed = 50;
    private int damage = 10;
    
    private void Start() {
        timer = 0;
        lifetime = 3;
    }

    private void Update() {
        RunTimer();
        ProjectileMovement();
    }

    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Enemy") {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    private void ProjectileMovement() {
        transform.Translate(Vector3.forward * travelingSpeed * Time.deltaTime);
    }

    private void RunTimer() {
        if (timer > lifetime) {
            Destroy(gameObject);
        }

        timer += Time.deltaTime;
    }

    private void DestroyObject() {
    }
}
