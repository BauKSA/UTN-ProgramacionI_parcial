using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeartsContainerController : MonoBehaviour
{
    [SerializeField] private int _initialHeartsCount = 3;
    [SerializeField] private GameObject _HeartPrefab;

    [SerializeField] private float _heartSize = 8f;
    [SerializeField] private float _heartSpacing = 2f;
    [SerializeField] private Vector2 _screenMargin = new(4f, 4f);

    private readonly Stack<GameObject> _Hearts = new();

    private Camera _camera;
    private float _lastOrthoSize;
    private float _lastAspect;

    public static HeartsContainerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance != this) return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        Instance = null;
    }

    private void LateUpdate()
    {
        if (!TryGetCamera(out Camera cam)) return;

        if (Mathf.Approximately(_lastOrthoSize, cam.orthographicSize) &&
            Mathf.Approximately(_lastAspect, cam.aspect)) return;

        RepositionHearts();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _camera = null;

        int livesToRestore = _Hearts.Count;
        ClearHearts();

        for (int i = 0; i < livesToRestore; i++)
            AddHeart();
    }

    private bool TryGetCamera(out Camera cam)
    {
        if (_camera == null) _camera = Camera.main;
        cam = _camera;
        return cam != null;
    }

    public void Initialize(int initialHeartsCount)
    {
        _initialHeartsCount = initialHeartsCount;
        ClearHearts();

        for (int i = 0; i < _initialHeartsCount; i++)
            AddHeart();
    }

    public void AddHeart()
    {
        if (!TryGetCamera(out Camera cam)) return;

        GameObject heart = Instantiate(_HeartPrefab, cam.transform);
        _Hearts.Push(heart);
        RepositionHearts();
    }

    public void RemoveHeart()
    {
        if (_Hearts.Count == 0) return;

        GameObject heart = _Hearts.Pop();
        if (heart != null) Destroy(heart);
    }

    public int GetHearts() => _Hearts.Count;

    private void ClearHearts()
    {
        foreach (GameObject heart in _Hearts)
            if (heart != null) Destroy(heart);

        _Hearts.Clear();
    }

    private void RepositionHearts()
    {
        if (!TryGetCamera(out Camera cam)) return;

        _lastOrthoSize = cam.orthographicSize;
        _lastAspect = cam.aspect;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float startX = -halfWidth + _screenMargin.x + _heartSize * 0.5f;
        float startY = halfHeight - _screenMargin.y - _heartSize * 0.5f;
        float step = _heartSize + _heartSpacing;

        int i = 0;
        foreach (GameObject heart in _Hearts)
        {
            if (heart != null)
            {
                int index = _Hearts.Count - 1 - i;
                heart.transform.SetLocalPositionAndRotation(
                    new Vector3(startX + step * index, startY, 10f),
                    Quaternion.identity
                );
            }
            i++;
        }
    }
}