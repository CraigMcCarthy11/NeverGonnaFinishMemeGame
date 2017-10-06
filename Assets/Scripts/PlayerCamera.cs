﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    //Variables
    public Transform player;
    public float smooth = 0.3f;
    public float height;

    private Vector3 velocity = Vector3.zero;

    //Methods
    private void Update()
    {
        Vector3 pos = new Vector3();
        pos.x = player.position.x;
        pos.y = player.position.y + height;
        pos.z = player.position.z - 7f;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);
    }

}
