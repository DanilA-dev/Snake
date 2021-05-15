using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    Meal,Bonus
}


public abstract class ACollectable : MonoBehaviour
{
    [SerializeField] private CollectableParams collectableParams;
    [SerializeField] private CollectablePosition collectablePosition;

    protected abstract CollectableType Collectable { get; }



    #region PROPERITES

    public CollectableParams CollectableParams { get => collectableParams; }
    public CollectablePosition CollectablePosition { get => collectablePosition; }

    #endregion


    protected abstract void Activate();



    protected virtual IEnumerator Eat(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator InProcess(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected virtual IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
    }


}

[System.Serializable]
public class CollectableParams
{
    [SerializeField, Min(0)] private int scoreForCollect;
    [SerializeField, Min(0)] private float toEatTime;
    [SerializeField, Min(0)] private float processingTime;
    [SerializeField, Min(0)] private float coolDownTime;

    #region PROPERTIES

    public int ScoreForCollect { get => scoreForCollect; }
    public float ToEatTime { get => toEatTime; }
    public float ProcessingTime { get => processingTime; }
    public float CoolDownTime { get => coolDownTime; }

    #endregion
} 

[System.Serializable]
public class CollectablePosition
{
    [SerializeField] private Vector3 minPos;
    [SerializeField] private Vector3 maxPos;

    private Vector3 RandomizePos()
    {
        var randomX = Random.Range(minPos.x, maxPos.x);
        var randomZ = Random.Range(minPos.z, maxPos.z);

        var newPos = new Vector3(randomX, 0.35f, randomZ);
        return newPos;
    }

    public void SetNewPosition(GameObject obj)
    {
        obj.transform.position = RandomizePos();
    }
    
}

