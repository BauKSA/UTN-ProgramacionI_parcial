using UnityEngine;

public class HealthStateController : MonoBehaviour
{
    [SerializeField] private bool _alive = true;
    [SerializeField] private int _lives = 3;
    [SerializeField] private bool _invincibility = false;

    public bool Alive
    {
        get => _alive;
        private set
        {
            if (_alive == value) return;
            _alive = value;
        }
    }

    public int Lives
    {
        get => _lives;
        private set
        {
            if (_lives == value) return;
            _lives = value;
        }
    }

    public bool Invincibility
    {
        get => _invincibility;
        private set
        {
            if (_invincibility == value) return;
            _invincibility = value;
        }
    }

    public void SetAlive(bool value) => Alive = value;
    public void AddLives(int sum) => Lives += sum;
    public void SetInvincibility(bool value) => Invincibility = value;
}