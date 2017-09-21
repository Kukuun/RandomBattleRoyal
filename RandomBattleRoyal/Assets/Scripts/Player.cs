using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Player health - duuh.
    [SerializeField] private int health = 100;
    // Player movement speed.
    [SerializeField] private float movementSpeed = 10f;
    // Length of the RayCast used to specify how long range a weapon should have.
    private int shootingRange = 50;
    // Numeric value that tells the RayCast to interfere with an object with equal numeric Mask value.
    private int floorMask;
    // Length of the RayCast used to specify how long the ray from the camera is.
    private float cameraRayLength = 100f;
    // Rigidbody reference.
    private Rigidbody rb;
    // How fast a gun should fire when full automatic.
    private float gunFireRate = 0.1f;
    // A trigger used to make full automatic mode for a gun.
    private bool canFire = true;
    // Damage per projectile.
    private int damage = 10;
    
    private void Start() {
        Spawn();

        // Sets the layermask we desire.
        floorMask = LayerMask.GetMask("Floor");
        // Sets the reference for  the rigidbody.
        rb = GetComponent<Rigidbody>(); 
    }

    private void Update() {
        KeyboardInput();
    }

    /// <summary>
    /// Spawns the player at a spawn point.
    /// </summary>
    private void Spawn() {
        Debug.Log(GameManager.Instance.StaticObjects.Count);

        foreach (var item in GameManager.Instance.StaticObjects) {
            Debug.Log(item.name);
            if (item.name == "PlayerSpawnPoint") {
                transform.position = item.transform.position;
                Debug.Log("Player spawned at PlayerSpawnPoint");
            }
        }
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

    /// <summary>
    /// Coroutine that handles full automatic firing.
    /// </summary>
    /// <returns></returns>
    IEnumerator FireRoutine() {
        canFire = false;
        Fire();

        yield return new WaitForSeconds(gunFireRate);

        canFire = true;
    }

    /// <summary>
    /// Method that handles firing logic.
    /// </summary>
    private void Fire() {
        Transform obj = null;
        RaycastHit hit;

        // Looks for the child GameObject used as a dummy for the projectile exit.
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).name == "ProjectileExit") {
                obj = transform.GetChild(i);
            }
        }

        // Performs a raycast from the ProjectileExit and forward.
        if (Physics.Raycast(obj.position, obj.forward, out hit, shootingRange)) {
            hit.transform.GetComponent<Enemy>().TakeDamage(damage);

            // Draws a line from the players ProjectileExit towards the object that's
            // been hit with a green line for 5 seconds.
            Debug.DrawLine(obj.position, hit.transform.position, Color.green, 2.5f);
        }
        else {
            // Draws a line from the players ProjectileExit towards the direction the
            // player was facing with a red line for 5 seconds.
            Debug.DrawLine(obj.position, obj.position + (obj.forward * shootingRange), Color.red, 2.5f);
        }
    }

    /// <summary>
    /// Method used to catch movement events and used them on the rigidbody.
    /// </summary>
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

    /// <summary>
    /// Method used to calculate the player orientation.
    /// </summary>
    private void PlayerOrientation() {
        // Create a ray from the mouse cursor on screen in the direction of the camera.
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Create a RaycastHit variable to store information about what was hit by the ray.
        RaycastHit floorHit;

        // Perform the raycast and if it hits something on the floor layer...
        if (Physics.Raycast(camRay, out floorHit, cameraRayLength, floorMask)) {
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
