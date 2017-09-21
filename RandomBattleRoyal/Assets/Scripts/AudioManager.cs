using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    private static AudioManager instance = null;
    /// <summary>
    /// List for all music tracts.
    /// </summary>
    private List<AudioClip> music;
    /// <summary>
    /// List for all sound effects.
    /// </summary>
    private List<AudioClip> sfx;
    /// <summary>
    /// List for all voice lines.
    /// </summary>
    private List<AudioClip> voice;

    public static AudioManager Instance {
        get {
            return instance;
        }

        set {
            instance = value;
        }
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}