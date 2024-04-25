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
    public class BandGlitchLooper : MonoBehaviour
    {
        private enum LoopType { StartOver, PingPong };

        [SerializeField]
        private Material targetMaterial;    // Target material to apply loop on.

        [SerializeField]
        private LoopType loopType;          // Loop type. StartOver -> goes from 0 to 1, then does the same. PingPong -> 0 to 1, 1 to 0, ...

        [SerializeField]
        private int loopCount;              // How many times will the band be looped?

        [SerializeField]
        private float loopDuration;         // In seconds how long does one loop take?

        [SerializeField]
        private float startFill;            // Initial fill percent while starting the loop.

        [SerializeField]
        private float targetFill;           // Target fill percent. Loop will happen bw startFill and targetFill.

        [SerializeField]
        private float lastFill;             // After the loops are done, fill percent will be set to this.

        private float fillPercentage;

        private void Awake()
        {
            // Make sure fill percentage is enabled on the material.
            targetMaterial.SetFloat("_UseFillPercent", 1.0f);

            // Start looping.
            StartCoroutine(BandLoop());
        }

        private IEnumerator BandLoop()
        {
            int looped = 0;
            float current = startFill;
            float target = targetFill;
            bool pingPongToggle = false;
            bool increment = looped == -1 ? false : true;
            float i = 0.0f;

            while (looped < loopCount)
            {
                // Interpolate fill perc.
                while (i < 1.0f)
                {
                    i += Time.deltaTime * 1.0f / loopDuration;
                    fillPercentage = Mathf.Lerp(current, target, i);
                    targetMaterial.SetFloat("_FillPercent", fillPercentage);
                    yield return null;
                }

                // Set target values depending on loop choice.
                if (loopType == LoopType.StartOver)
                    fillPercentage = 0.0f;
                else
                {
                    pingPongToggle = !pingPongToggle;
                    target = pingPongToggle ?  startFill : targetFill;
                }

                current = fillPercentage;
                targetMaterial.SetFloat("_FillPercent", fillPercentage);

                if (increment)
                    looped++;

                i = 0.0f;

            }

            targetMaterial.SetFloat("_FillPercent", lastFill);

            LoopCompleted();
        }

        void LoopCompleted()
        {
            // Edit here to change the material of the object to non-glitch material after the loop.
        }
    }


}
