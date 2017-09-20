using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager instance = null;
    [Header("Prefabs")]
    /// <summary>
    /// Enemy prefab.
    /// </summary>
    [SerializeField] private GameObject enemy_prefab;
    /// <summary>
    /// Floor prefab.
    /// </summary>
    [SerializeField] private GameObject floor_prefab;

    [Space(5)]
    [Header("Lists of objects")]
    /// <summary>
    /// List for all static objects in the scene.
    /// </summary>
    [SerializeField] private List<GameObject> staticObjects;
    /// <summary>
    /// List for all dynamic objects in the scene.
    /// </summary>
    private List<GameObject> dynamicObjects;
    /// <summary>
    /// UIManager reference.
    /// </summary>
    private UIManager uiManager;
    /// <summary>
    /// AudioManager reference.
    /// </summary>
    private AudioManager audioManager;

    /// <summary>
    /// Property that grants access to the other properties in this class.
    /// </summary>
    public static GameManager Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    public GameObject Enemy_prefab {
        get {
            return enemy_prefab;
        }

        set {
            enemy_prefab = value;
        }
    }

    public GameObject Ground_prefab {
        get {
            return floor_prefab;
        }

        set {
            floor_prefab = value;
        }
    }

    public List<GameObject> StaticObjects {
        get {
            return staticObjects;
        }

        set {
            staticObjects = value;
        }
    }

    public List<GameObject> DynamicObjects {
        get {
            return dynamicObjects;
        }

        set {
            dynamicObjects = value;
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