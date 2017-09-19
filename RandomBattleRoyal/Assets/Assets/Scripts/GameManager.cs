using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;
    [SerializeField]private GameObject projectile_prefab;
    private GameObject[] staticObjects;
    private GameObject[] dynamicObjects;
    private UIManager uiManager;
    private AudioManager audioManager;

    public static GameManager Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    public GameObject Bullet_prefab {
        get {
            return projectile_prefab;
        }

        set {
            projectile_prefab = value;
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}