using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private Transform snakeHead;
    [SerializeField] private Joystick joystick;

    [Range(1, 20)]
    [SerializeField] private float snakeSpeed;
    [Range(1, 20)]
    [SerializeField] private float maxSnakeSpeed;
    [Range(10, 300)]
    [SerializeField] private float steeringPower;


    private List<Transform> tails = new List<Transform>();

    #region PROPERTIES

    public List<Transform> Tails { get => tails; set => tails = value; }
    public Transform SnakeHead { get => snakeHead; }
    public float SnakeSpeed { get => snakeSpeed; }
    public float SteeringPower { get => steeringPower; }

    #endregion

    private void Awake()
    {
        tails.Add(snakeHead);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
       if (tails.Count > 0)
       {
           HeadMovement();
           Steering();
       }
    }
    
    private void HeadMovement()
    {
        tails[0].Translate(tails[0].forward * Time.deltaTime * snakeSpeed, Space.World);
    }

    private void Steering()
    {
        var horizontalMove = joystick.Horizontal * steeringPower * Time.deltaTime;
        tails[0].Rotate(Vector3.up, horizontalMove);
    }


    public void Accelerate(float amount)
    {
        snakeSpeed += amount;
        if (snakeSpeed > maxSnakeSpeed)
        {
            snakeSpeed = maxSnakeSpeed;
        }
    }
    
}
