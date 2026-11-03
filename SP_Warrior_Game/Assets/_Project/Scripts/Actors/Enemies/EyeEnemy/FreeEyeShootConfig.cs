using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Free Eye/Config")]
public class FreeEyeShootConfig : ScriptableObject
{
    [field: SerializeField] public GameObject[] BulletPrefabs { get; private set; }
}