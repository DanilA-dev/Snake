using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeColor : MonoBehaviour , IChangeColor
{
    [SerializeField] private ColorType currentColorType;
    [SerializeField] private Color startColor;
    [SerializeField] private Material snakeMaterial;

    #region PROPERTIES

    public ColorType CurrentColorType { get => currentColorType; set => currentColorType = value; }
    public Material SnakeMaterial { get => snakeMaterial; set => snakeMaterial = value; }


    #endregion
    public void ChangeColor(Color newColor, ColorType newColorType)
    {
        snakeMaterial.color = newColor;
        currentColorType = newColorType;
    }

    private void OnEnable()
    {
        if(snakeMaterial != null)
        {
            snakeMaterial.color = startColor;
        }
    }

}
