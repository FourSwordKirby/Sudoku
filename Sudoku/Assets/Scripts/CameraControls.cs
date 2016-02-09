using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CameraControls : MonoBehaviour {

    private Camera cameraComponent;

    public MagnifyingGlass magnifyGlass;

    /* camera moving constants */
    private const float Z_OFFSET = -10.0f;

    /*CONSTANTS*/
    private const float PAN_SPEED = 5.0f;

	// Use this for initialization
	void Start () {
        cameraComponent = GetComponent<Camera>();
	}

    void Update()
    {
        if (Input.GetMouseButton(1))
            magnifyGlass.gameObject.SetActive(true);
        else
            magnifyGlass.gameObject.SetActive(false);
    }
}
