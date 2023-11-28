using UnityEngine;

namespace Services.PoolSystem.Data
{
    [CreateAssetMenu(fileName = "PoolData", menuName = "PoolSystem/PoolData")]
    public class PoolData : ScriptableObject
    {
        [SerializeField] private GameObject[] _prefabs;

        public GameObject[] Prefabs => _prefabs;
    }
}