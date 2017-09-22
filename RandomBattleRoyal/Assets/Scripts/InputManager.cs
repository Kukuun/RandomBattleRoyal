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
            GameObject floor = null;
            GameObject enemy = null;

            foreach (var item in GameManager.Instance.StaticObjects) {
                if (item == null) {
                    Debug.Log("item is null");
                    break;
                }
            }

            foreach (var item in GameManager.Instance.StaticObjects) {
                if (item.name == "Floor") {
                    floor = item;
                    break;
                }
            }

            foreach (var item in GameManager.Instance.Prefabs) {
                if (item.name == "Enemy") {
                    //GameManager.Instance.DynamicObjects.Add(item);
                    enemy = item;
                    Vector3 spawnPosition = CalculateRandomSpawnPosition(floor, 5f, 2f);

                    Instantiate(enemy, spawnPosition, Quaternion.identity);
                    break;
                }
            }            
        }
    }

    /// <summary>
    /// Calculates a position within the floor where an object can safely spawn.
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