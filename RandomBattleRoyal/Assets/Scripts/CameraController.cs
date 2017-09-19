using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private Transform player;

    private void Start() {
        player = GameObject.Find("Player").transform;
    }
    void Update () {
        transform.LookAt(player);

        float posX = Mathf.Lerp(transform.position.x, player.position.x, 8f * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.x = posX;
        transform.position = pos;
        //transform.Translate(pos);
    }
}
