using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int health;
    [SerializeField] private float movementSpeed = 10f;
    private int shootingRange = 50;
    private int floorMask;
    private float camRayLength = 100f;
    private Rigidbody rb;
    private float gunFireRate = 0.1f;
    private bool canFire = true;
    private int enemyLayer;
    private int damage = 10;

    private void Start() {
        floorMask = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        KeyboardInput();
    }

    private void FixedUpdate() {
        Movement();
        PlayerOrientation();
    }

    private void KeyboardInput() {
        if (Input.GetMouseButton(0) && canFire) {
            StartCoroutine(FireRoutine());
        }
    }

    IEnumerator FireRoutine() {
        canFire = false;
        Fire();

        yield return new WaitForSeconds(gunFireRate);

        canFire = true;
    }

    private void Fire() {
        Transform obj = null;
        RaycastHit hit;

        // Looks for the child GameObject used as a dummy for the projectile exit.
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "ProjectileExit") {
                obj = transform.GetChild(i);
            }
        }

        // Performs a raycast from
        if (Physics.Raycast(obj.position, obj.forward, out hit, shootingRange)) {
            hit.transform.GetComponent<Enemy>().TakeDamage(damage);

            // Draws a line from the players ProjectileExit towards the object that's
            // been hit with a green line for 5 seconds.
            Debug.DrawLine(obj.position, hit.transform.position, Color.green, 5.0f);
        }
        else {
            // Draws a line from the players ProjectileExit towards the direction the
            // player was facing with a red line for 5 seconds.
            Debug.DrawLine(obj.position, obj.position + (obj.forward * shootingRange), Color.red, 5.0f);
        }
    }

    private void Movement() {
        float directionX = Input.GetAxisRaw("Horizontal");
        float directionZ = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(directionX, 0, directionZ) * movementSpeed * Time.deltaTime;

        if (directionZ != 0) {
            rb.useGravity = false;
        }
        else {
            rb.useGravity = true;
        }

        rb.MovePosition(transform.position + direction);
    }

    private void PlayerOrientation() {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            // Create a vector from the player to the point on the floor the raycast from the mouse hit.
            Vector3 playerToMouse = floorHit.point - transform.position;

            // Ensure the vector is entirely along the floor plane.
            playerToMouse.y = 0f;

            // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // Set the player's rotation to this new rotation.
            rb.MoveRotation(newRotation);
        }
    }
}
