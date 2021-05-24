using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ColorType : int
{
    Purple,
    Green,
    Yellow,
    Cyan,
    Pink,
    Orange
}

public class ColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ColorType colorType;
    [SerializeField] private Color newColor;


    private void OnEnable()
    {
        InitMaterialColor();
    }

    private void InitMaterialColor()
    {
        meshRenderer.material.color = newColor;
        ParticleSystem.MainModule main = particles.main;
        main.startColor = newColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        var snakeColor = other.GetComponent<IChangeColor>();
        if(snakeColor != null)
        {
            snakeColor.ChangeColor(newColor, colorType);
        }
    }

   
}

