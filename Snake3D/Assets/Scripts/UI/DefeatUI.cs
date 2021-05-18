using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    [SerializeField] private List<SimpleTweenAnimation> animations = new List<SimpleTweenAnimation>();

    private void OnEnable()
    {
        Animate();
    }

    private void Animate()
    {
        if(animations.Count > 0)
        {
          for (int i = 0; i < animations.Count; i++)
          {
                animations[i].Animate();
          }
        }
    }

    public void RestartGame()
    {

    }
   
}
