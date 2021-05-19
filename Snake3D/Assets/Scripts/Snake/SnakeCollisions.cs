using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CollisionType
{
    Obstacle, Collectable
}

public class SnakeCollisions : MonoBehaviour
{
    [SerializeField] private List<CollisionEvent> collisions = new List<CollisionEvent>();

    private CollisionType collType;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            collType = CollisionType.Obstacle;
            for (int i = 0; i < collisions.Count; i++)
            {
                StartCoroutine(collisions[i].StartEvent(collType));
            }

            CameraShake.Instance.Shake(1f, 1f);
        }

        if(other.gameObject.CompareTag("Collectable"))
        {
            collType = CollisionType.Collectable;
            for (int i = 0; i < collisions.Count; i++)
            {
                StartCoroutine(collisions[i].StartEvent(collType));
            }

            CameraShake.Instance.Shake(0.1f,0.2f);
        }
    }
}

[System.Serializable]
public class CollisionEvent
{
    [SerializeField] private CollisionType collision;
    [SerializeField] private float toInvoke;
    [SerializeField] private UnityEvent OnCollision;

    #region PROPERITES

    public CollisionType Collision { get => collision; }

    #endregion


    public IEnumerator StartEvent(CollisionType type)
    {
        if(collision == type)
        {
           yield return new WaitForSeconds(toInvoke);
           OnCollision?.Invoke();
        }
    }

}
