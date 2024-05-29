using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Vector3 playerPos;
    void Update()
    {
        playerPos = new Vector3(Player.instance.transform.position.x, Player.instance.transform.position.y, -0.9f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,playerPos, 5f * Time.deltaTime);
    }
}
