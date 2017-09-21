using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {
    private static UIManager instance = null;
    /// <summary>
    /// List containing references for all buttons.
    /// </summary>
    private List<Button> buttons;
    /// <summary>
    /// List containing references for all texts.
    /// </summary>
    private List<Text> texts;
    /// <summary>
    /// List containing references for all sliders.
    /// </summary>
    private List<Slider> sliders;

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