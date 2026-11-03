using System.Collections;
using UnityEngine;

public class FreeEyeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _freeEyePrefab;

    private readonly Vector2 _minPosition = new( 2740f, -60f );
    private readonly Vector2 _maxPosition = new(2885f, 50f);
    private readonly float _minSpawnTime = 5f;
    private readonly float _maxSpawnTime = 10f;

    private bool _active = false;

    public void Activate()
    {
        _active = true;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (_active)
        {
            float waitTime = Random.Range(_minSpawnTime, _maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnFreeEye();

            int secondSpawn = Random.Range(0, 7);
            if (secondSpawn <= 1)
            {
                yield return new WaitForSeconds(1);
                SpawnFreeEye();
            }

            int thirdSpawn = Random.Range(0, 1);
            if (thirdSpawn == 1)
            {
                yield return new WaitForSeconds(waitTime / 2);
                SpawnFreeEye();
            }
        }
    }

    private void SpawnFreeEye()
    {
        if (_freeEyePrefab == null)
            return;

        Vector3 randomPosition = new(
            Random.Range(_minPosition.x, _maxPosition.x),
            Random.Range(_minPosition.y, _maxPosition.y),
            0f
        );

        Instantiate(_freeEyePrefab, randomPosition, Quaternion.identity);
    }
}
