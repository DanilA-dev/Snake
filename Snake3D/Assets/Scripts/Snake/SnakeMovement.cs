using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;

    [Range(1, 20)]
    [SerializeField] private float snakeSpeed;

    [Range(10, 300)]
    [SerializeField] private float steeringPower;
    [Range(0.5f, 10)]
    [SerializeField] private float rotatePower;

    [SerializeField] private float minSteeringX, maxSteeringX;


    private float screenWidth;
    private Quaternion startRot;
    private List<Transform> tails = new List<Transform>();

    #region PROPERTIES

    public List<Transform> Tails { get => tails; set => tails = value; }
    public Transform SnakeHead { get => snakeHead; }
    public float SnakeSpeed { get => snakeSpeed; }
    public float SteeringPower { get => steeringPower; }
    public float RotatePower { get => rotatePower; }
    public float MinSteeringX { get => minSteeringX; }
    public float MaxSteeringX { get => maxSteeringX; }
    public Quaternion StartRot { get => startRot; }

    #endregion

    private void Awake()
    {
        tails.Add(snakeHead);
        screenWidth = Screen.width / 2;
        startRot = tails[0].rotation;
    }


    public void Movement()
    {
       if (tails.Count > 0)
       {
           HeadMovement();
           Steering();
       }
    }
    
    private void HeadMovement()
    {
        tails[0].Translate(Vector3.forward * Time.deltaTime * snakeSpeed, Space.World);
    }

    
    private void Steering()
    {
       if (Input.touchCount > 0)
       {
           var touch = Input.GetTouch(0);

           if (touch.position.x > screenWidth)
           {
               if (tails[0].position.x >= maxSteeringX)
               {
                   ResetRotation();
                   return;
               }

               tails[0].Translate(Vector3.right * Time.deltaTime * snakeSpeed, Space.World);
               tails[0].Rotate(Vector3.up, SteeringRotation());
           }
           else if (touch.position.x < screenWidth)
           {
               if (tails[0].position.x <= minSteeringX)
               {
                   ResetRotation();
                   return;
               }

               tails[0].Translate(Vector3.left * Time.deltaTime * snakeSpeed, Space.World);
               tails[0].Rotate(Vector3.up, -SteeringRotation());
           }
           else
           {
               ResetRotation();
           }
       }

        if (Input.GetKey(KeyCode.D))
        {
            if (tails[0].position.x >= maxSteeringX)
            {
                ResetRotation();
                return;
            }

            tails[0].Translate(Vector3.right * Time.deltaTime * snakeSpeed, Space.World);
            tails[0].Rotate(Vector3.up, SteeringRotation());
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (tails[0].position.x <= minSteeringX)
            {
                ResetRotation();
                return;
            }

            tails[0].Translate(Vector3.left * Time.deltaTime * snakeSpeed, Space.World);
            tails[0].Rotate(Vector3.up, -SteeringRotation());
        }
        else
        {
            ResetRotation();
        }

    }

    private void ResetRotation()
    {
        tails[0].rotation = Quaternion.RotateTowards(tails[0].rotation, startRot, rotatePower);
    }

    private float SteeringRotation()
    {
        float steeringOffset = steeringPower * Time.deltaTime;
        var maxSteeringRotation = 0.5f;
        steeringOffset = Mathf.Clamp(steeringOffset, 0, maxSteeringRotation);
        return steeringOffset;
    }

    
}
