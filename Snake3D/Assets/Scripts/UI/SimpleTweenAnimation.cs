using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum AnimationType
{
    Fade,PopUp,BounceLoop,MoveLoop,RotateLoop
}


[System.Serializable]
public class SimpleTweenAnimation 
{

    [SerializeField] private string Name;
    [SerializeField] private Ease ease;
    [SerializeField] private AnimationType type;
    [SerializeField] private Vector3 endVectorValue;
    [SerializeField, Min(0)] private float endValue;
    [SerializeField, Min(0)] private float startValue;
    [SerializeField, Min(0)] private float duration;
    [SerializeField] private List<Component> elements = new List<Component>();


    public void Animate()
    {
        switch(type)
        {
            case AnimationType.Fade:
                Fade();
                break;
            case AnimationType.BounceLoop:
                BounceLoop();
                break;
            case AnimationType.PopUp:
                PopUp();
                break;
            case AnimationType.MoveLoop:
                MoveLoop();
                break;
            case AnimationType.RotateLoop:
                RotateLoop();
                break;
        }
    }

    private  void PopUp()
    {
        foreach (var e in elements)
        {
           bool transformFound = e.TryGetComponent<Transform>(out Transform t);
           if(transformFound)
           {
             t.DOScale(endValue, duration).From(startValue).SetEase(ease);
           }
        }
    }

    private void Fade()
    {
        foreach (var e in elements)
        {
            bool graphicFound = e.TryGetComponent<Graphic>(out Graphic g);
            if(graphicFound)
            {
                g.DOFade(endValue, duration).From(startValue).SetEase(ease);
            }
        }
    }

    private void BounceLoop()
    {
        foreach(var e in elements)
        {
            bool transformFound = e.TryGetComponent<Transform>(out Transform t);
            if(transformFound)
            {
                var seq = DOTween.Sequence();
                seq.Append(t.DOScale(endValue, duration)).SetEase(ease);
                seq.Append(t.DOScale(startValue, duration)).SetEase(ease);
                seq.SetLoops(-1);
            }
        }
    }

    private void MoveLoop()
    {
        foreach (var e in elements)
        {
            bool transformFound = e.TryGetComponent<Transform>(out Transform t);
            if(transformFound)
            {
                Vector3 startPos = t.localPosition;

                var seq = DOTween.Sequence();
                seq.Append(t.DOLocalMove(endVectorValue, duration).SetEase(ease));
                seq.Append(t.DOLocalMove(startPos, duration).SetEase(ease));
                seq.SetLoops(-1);
            }
        }
    }

    private void RotateLoop()
    {
       foreach (var e in elements)
       {
          bool transformFound = e.TryGetComponent<Transform>(out Transform t);
          if (transformFound)
          {
                  Vector3 startPos = t.localPosition;
               
                  var seq = DOTween.Sequence();
                  seq.Append(t.DOLocalRotate(endVectorValue, duration).SetEase(ease));
                  seq.Append(t.DOLocalRotate(startPos, duration).SetEase(ease));
                  seq.SetLoops(-1);
          }
       }
    }
    


}
