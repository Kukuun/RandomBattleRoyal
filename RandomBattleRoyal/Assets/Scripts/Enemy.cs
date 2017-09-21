using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    /// <summary>
    /// The enemies current health.
    /// </summary>
    [SerializeField] private int health = 100;
    /// <summary>
    /// The maximum amount of health an enemy can have.
    /// </summary>
    [SerializeField] private int maxHealth = 100;

    /// <summary>
    /// Method that subtracts damage when taking damage and destroy itself when reaching 0 health.
    /// </summary>
    /// <param name="damage">How much damage the player should take.</param>
    public void TakeDamage(int damage) {
        health -= damage;
        
        if (health <= 0) {
            Die();
        }
    }

    /// <summary>
    /// Method to destroy itself.
    /// </summary>
    private void Die() {
        Destroy(gameObject);
    }

    /// <summary>
    /// Method that adds health to the player.
    /// </summary>
    /// <param name="_health">How much the player should be healed for.</param>
    public void AddHealth(int _health) {
        health -= _health;

        if (health >= maxHealth) {
            health = maxHealth;
        }
    }
}