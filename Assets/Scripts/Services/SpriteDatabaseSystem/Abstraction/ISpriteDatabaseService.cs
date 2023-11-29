using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.SpriteDatabaseSystem.Abstraction
{
    public interface ISpriteDatabaseService
    {
        Sprite GetSpriteByName(string name);
    }
}