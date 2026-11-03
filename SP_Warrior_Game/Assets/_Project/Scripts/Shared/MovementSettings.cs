using UnityEngine;

public class MovementSettings : MonoBehaviour
{
    [SerializeField] private Vector2 speed = new(75f, 75f);
    [SerializeField] private float acceleration = 45f;
    [SerializeField] private float deceleration = 25f;
    [SerializeField] private bool instantAcceleration = false;

    public Vector2 Speed { get => speed; set => speed = value; }
    public float Acceleration
    {
        get => instantAcceleration ? Mathf.Infinity : acceleration;
        set => acceleration = value;
    }
    public float Deceleration
    {
        get => instantAcceleration ? Mathf.Infinity : deceleration;
        set => deceleration = value;
    }
}