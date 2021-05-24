using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : ACollectable, ICompareColor
{

    [SerializeField] private ColorType colorType;
    [SerializeField] private Color humanColor;
    [SerializeField] private MeshRenderer meshRenderer;

    #region PROPERTIES

    public override CollectableType CollectableType => CollectableType.Human;


    #endregion


    private void Start()
    {
        meshRenderer.material.color = humanColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        SnakeCollisions snake = other.GetComponent<SnakeCollisions>();
        if(snake != null)
        {
            Activate();
        }
    }

    protected override void Activate()
    {
        if(CollectableState == CollectableState.NotEaten)
        {
            StartCoroutine(Eat(CollectableParams.ToEatTime));
        }
    }

    protected override IEnumerator Eat(float time)
    {
        CollectableState = CollectableState.Eaten;
        if(CollectableState == CollectableState.Eaten)
        {
            yield return new WaitForSeconds(time);
            Animate();
            CollectableState = CollectableState.NotEaten;
        }
    }

    private void Animate()
    {
        foreach (var a in animations)
        {
            a.Animate();
        }
    }

    public bool CheckColor(ColorType type)
    {
        if(colorType == type)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
