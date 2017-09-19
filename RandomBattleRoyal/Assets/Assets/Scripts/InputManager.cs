using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {
    private static InputManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
    }

    private void Update () {
        KeyboardInput();
    }

    private void KeyboardInput() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene("Scene01");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene("Scene02");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SceneManager.LoadScene("Scene03");
        }
        if (Input.GetKeyDown(KeyCode.W)) {

        }
    }
}