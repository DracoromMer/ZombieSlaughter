using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    public Vector2 offset;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position * offset;
    }
}
