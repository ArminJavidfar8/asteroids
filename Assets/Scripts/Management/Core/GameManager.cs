using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.Core {
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            _ = ServiceHolder.ServiceProvider;
        }
    }
}