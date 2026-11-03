using System.Collections.Generic;
using UnityEngine;

public class HeartsContainerController : MonoBehaviour
{
    [SerializeField]
    private int _initialHeartsCount = 3;

    private int _heartsCount = 0;

    [SerializeField]
    private Stack<GameObject> _Hearts = new ();
    [SerializeField]
    private GameObject _HeartPrefab;
    [SerializeField]
    private GameObject _mainCamera;
    public static HeartsContainerController Instance { get; private set; }

    private Camera _camera;
    private float _lastOrthoSize;
    private float _lastAspect;

    [SerializeField] private float _heartSize = 8f;
    [SerializeField] private float _heartSpacing = 2f;
    [SerializeField] private Vector2 _screenMargin = new(4f, 4f);

    private Camera Camera
    {
        get
        {
            if (_camera == null) _camera = _mainCamera.GetComponent<Camera>();
            return _camera;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        if (Mathf.Approximately(_lastOrthoSize, Camera.orthographicSize) &&
            Mathf.Approximately(_lastAspect, Camera.aspect)) return;

        RepositionHearts();
    }

    private void OnDestroy()
    {
        if (Instance != this) return;
    }

    public void Initialize(int initialHeartsCount)
    {
        _initialHeartsCount = initialHeartsCount;

        for (int i = 0; i < _initialHeartsCount; i++)
            AddHeart();
    }

    public void AddHeart()
    {
        GameObject heart = Instantiate(_HeartPrefab, _mainCamera.transform);
        _Hearts.Push(heart);
        _heartsCount++;
        RepositionHearts();
    }

    private void RepositionHearts()
    {
        _lastOrthoSize = Camera.orthographicSize;
        _lastAspect = Camera.aspect;

        float halfHeight = Camera.orthographicSize;
        float halfWidth = halfHeight * Camera.aspect;

        float startX = -halfWidth + _screenMargin.x + _heartSize * 0.5f;
        float startY = halfHeight - _screenMargin.y - _heartSize * 0.5f;
        float step = _heartSize + _heartSpacing;

        int i = 0;
        foreach (GameObject heart in _Hearts)
        {
            int index = _Hearts.Count - 1 - i;
            heart.transform.SetLocalPositionAndRotation(
                new Vector3(startX + step * index, startY, 10f),
                Quaternion.identity
            );
            i++;
        }
    }

    public void RemoveHeart()
    {
        if (_Hearts.Count == 0) return;

        GameObject heart = _Hearts.Pop();
        Destroy(heart);
        _heartsCount--;
    }

    public int GetHearts() => _heartsCount;
}
