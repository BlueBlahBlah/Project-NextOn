/*
 * Object Glitch Shader
 * Copyright (c) 2019-Present, Inan Evin, Inc. All rights reserved.
 * Author: Inan Evin
 * contact: inanevin@gmail.com
 *
 * Feel free to ask about the package and talk about recommendations!
 *
 *
 *
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IE.ObjectGlitchShader
{
    public class CameraController : MonoBehaviour
    {

        [SerializeField] private Vector2 sensitivity = new Vector2(3.0f, 3.0f);                             // Mouse sensitivity.
        [SerializeField] private Vector2 smooth = new Vector2(0.03f, 0.03f);                                // Mouse smooth.
        [SerializeField] private float minY = -85.0f;                                                       // Minimum angle on X rotation axis. (mouse Y)
        [SerializeField] private float maxY = 85.0f;                                                        // Maximum angle on X rotation axis. (mouse Y)
        [SerializeField] private float speed = 6.0f;                                                       // Cam's movement speed.

        private Vector2 mouseInputs;                                            // Storage for mouse inputs.
        private Vector2 smoothedInputs;                                         // Storage for smoothing mouse inputs.
        private Vector2 refInputs;                                              // Reference used in Mathf.SmoothDamp method.
        private Quaternion thisRotation;                                        // Storage for the rotation of this transform.

        void Awake()
        {
            // Store rotation variables.
            thisRotation = transform.localRotation;
        }

        void Update()
        {
            if (Input.GetMouseButton(1))
            {
                // Get mouse inputs & multiply them with sensitivity, and then store them.
                mouseInputs.x += Input.GetAxis("Mouse X") * sensitivity.x;
                mouseInputs.y -= Input.GetAxis("Mouse Y") * sensitivity.y;
            }


            // Clamp vertical mouse input.
            mouseInputs.y = Mathf.Clamp(mouseInputs.y, minY, maxY);

            // Smooth out mouse inputs.
            smoothedInputs.x = Mathf.SmoothDamp(smoothedInputs.x, mouseInputs.x, ref refInputs.x, smooth.x);
            smoothedInputs.y = Mathf.SmoothDamp(smoothedInputs.y, mouseInputs.y, ref refInputs.y, smooth.y);

            // Create rotations based on the angles from smoothed mouse inputs.
            Quaternion q_X = Quaternion.AngleAxis(smoothedInputs.x, Vector3.up);
            Quaternion q_Y = Quaternion.AngleAxis(smoothedInputs.y, Vector3.right);

            // Apply rotation to both player and the camera.
            transform.localRotation = thisRotation * q_X * q_Y;

            transform.position += Input.GetAxis("Vertical") * Time.deltaTime * transform.forward * speed;
            transform.position += Input.GetAxis("Horizontal") * Time.deltaTime * transform.right * speed;

        }

    }

}
