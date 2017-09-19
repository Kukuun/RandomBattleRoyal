using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager instance = null;
    private AudioClip[] music;
    private AudioClip[] sfx;
    private AudioClip[] voice;

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