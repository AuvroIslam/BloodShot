using SingletonManager;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Singletons
{
    public class SceneLoader : SingletonPersistent
    {
        public void SceneLoad()
        {
            // Load the loading scene (assuming it's at index 2)
            // If you want to go directly to game, change to SceneManager.LoadScene(1);
            SceneManager.LoadScene("LoadingScene"); // Or use index if you prefer
        }
    }
}
