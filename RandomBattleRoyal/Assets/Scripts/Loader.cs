using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int loadNextSceneIn = 5;
    [SerializeField] private Text loading;
    private Canvas canvas;
    private bool isFading;

    void Start() {
        InstantiateManagers();
        canvas = FindObjectOfType<Canvas>();

        if (canvas) {
            Debug.Log("Canvas found.");
        }
        else {
            Debug.Log("Canvas not found.");
        }
    }

    private void Update() {
        LoadNextScene();
    }

    private void InstantiateManagers() {
        Instantiate(gameManager);
        //Instantiate(inputManager);
        Instantiate(audioManager);
        Instantiate(uiManager);
    }

    private void LoadNextScene() {
        if (Time.time > loadNextSceneIn) {
            SceneManager.LoadScene("Scene01");
        }
    }
}