using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.SpriteDatabaseSystem.Data
{
    [CreateAssetMenu(fileName = "SpriteDatabase", menuName = "Asteroids/SpriteDatabase")]
    public class SpriteDatabase : ScriptableObject
    {
        [SerializeField] private Sprite[] _sprites;

        public IEnumerable<Sprite> Sprites => _sprites;
    }
}