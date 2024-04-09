using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public Material shader;
    [SerializeField] private bool isSpotted;
    [SerializeField] private bool isFadingAway;

    [SerializeField] private string shaderPropName;
    [SerializeField] private string shaderPropName2;
    
    [SerializeField] private float shaderValue;
    [SerializeField] private float shaderValue2;
    private void FixedUpdate()
    {

        if (isSpotted && shaderValue > 0)
        {
            shaderValue -= Time.deltaTime;
            shader.SetFloat(shaderPropName, shaderValue);
        };

        if (isSpotted && shaderValue2 > 0)
        {
            shaderValue2 -= Time.deltaTime;
            shader.SetFloat(shaderPropName2, shaderValue2);
        }
    }

    public void BeenSpotted(float value, string key, [CanBeNull] float? value2, [CanBeNull] string? key2)
    {
        isSpotted = true;
        isFadingAway = false;
        shaderValue = value;
        shaderValue2 = (float)value2;
        
        shaderPropName = key;
        shaderPropName2 = key2;
    }

    public void FadingAway()
    {
        isFadingAway = true;
        isSpotted = false;
    }
}
