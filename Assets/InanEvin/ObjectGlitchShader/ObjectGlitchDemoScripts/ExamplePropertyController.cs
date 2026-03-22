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
using UnityEngine.UI;

namespace IE.ObjectGlitchShader
{
    public class ExamplePropertyController : MonoBehaviour
    {
        #region Exposed Properties

        [SerializeField]
        private Material targetMaterial;

        [SerializeField]
        private Slider transitionLengthSlider;

        [SerializeField]
        private Text sliderValueText;

        [SerializeField]
        private InputField moveSpeedField;

        [SerializeField]
        private string sliderProperty;

        [SerializeField]
        private string inputFieldProperty;

        #endregion

        #region Other Properties

        #endregion

        #region Unity Operation(s)

        #endregion

        #region Exposed Operation(s)

        public void SliderChanged()
        {
            if (targetMaterial != null)
            {
                if (sliderValueText != null)
                    sliderValueText.text = transitionLengthSlider.value.ToString();
                targetMaterial.SetFloat(sliderProperty, transitionLengthSlider.value);
            }
        }

        public void InputFieldChanged()
        {
            if (targetMaterial != null)
            {
                float val = 0.0f;
                bool succ = float.TryParse(moveSpeedField.text, out val);

                if (succ)
                    targetMaterial.SetFloat(inputFieldProperty, val);
            }
        }

        #endregion

        #region Class Operation(s)

        #endregion

        #region Utility Operation(s)

        #endregion

        #region Coroutine(s)

        #endregion
    }
}
