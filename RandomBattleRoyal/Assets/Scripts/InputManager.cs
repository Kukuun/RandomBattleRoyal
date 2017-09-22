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

    /// <summary>
    /// Method for input events.
    /// </summary>
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
        if (Input.GetKeyDown(KeyCode.G)) {
            SpawnEnemy("Enemy", "Floor");
        }
    }

    /// <summary>
    /// Spawns an enemy at a possible spawn position.
    /// </summary>
    /// <param name="prefabName">Name of Enemy prefab.</param>
    /// <param name="spawningArea">Name of area where an enemy should be able to spawn.</param>
    private void SpawnEnemy(string prefabName, string spawningArea) {
        foreach (var prefab in GameManager.Instance.Prefabs) {
            if (prefab.name == prefabName) {
                foreach (var obj in GameManager.Instance.StaticObjects) {
                    if (obj.name == spawningArea) {
                        GameObject floor = obj;
                        GameObject enemy = prefab;

                        Vector3 spawnPosition = CalculateRandomSpawnPosition(floor, 5f, 2f);
                        Instantiate(enemy, spawnPosition, Quaternion.identity);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Calculates a possible spawn position within the platform where an object can safely spawn.
    /// </summary>
    /// <param name="obj">Floor object.</param>
    /// <param name="borderMargin">A distrance from the border going inwards to get a secure spawn position.</param>
    /// <param name="yMargin">How far on the Y axis an object should spawn.</param>
    /// <returns></returns>
    public Vector3 CalculateRandomSpawnPosition(GameObject obj, float borderMargin, float yMargin) {
        float xPosition = Random.Range(-obj.GetComponent<Renderer>().bounds.size.x / 2 + borderMargin, obj.GetComponent<Renderer>().bounds.size.x / 2 - borderMargin);
        float zPosition = Random.Range(-obj.GetComponent<Renderer>().bounds.size.z / 2 + borderMargin, obj.GetComponent<Renderer>().bounds.size.z / 2 - borderMargin);
        Vector3 spawnPosition = new Vector3(xPosition, yMargin, zPosition);

        return spawnPosition;
    }
}