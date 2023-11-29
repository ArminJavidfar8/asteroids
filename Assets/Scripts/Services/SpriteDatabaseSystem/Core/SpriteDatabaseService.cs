using Services.SpriteDatabaseSystem.Abstraction;
using Services.SpriteDatabaseSystem.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.SpriteDatabaseSystem.Core
{
    public class SpriteDatabaseService : ISpriteDatabaseService
    {
        private readonly Dictionary<string, Sprite> _sprites;
        public SpriteDatabaseService()
        {
            SpriteDatabase spriteDatabase = Resources.Load<SpriteDatabase>("SpriteDatabase/Sprites");
            _sprites = new Dictionary<string, Sprite>();
            foreach (Sprite sprite in spriteDatabase.Sprites)
            {
                _sprites.Add(sprite.name, sprite);
            }
        }

        public Sprite GetSpriteByName(string name)
        {
            foreach (var item in _sprites)
            {
                if (item.Key == name)
                {
                    return item.Value;
                }
            }
            return null;
        }
    }
}