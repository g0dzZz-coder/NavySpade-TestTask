using System;
using UnityEditor;
using UnityEngine;

namespace NavySpade.Core
{
    public static class Game
    {
        public static event Action Restarted;
        public static event Action<bool> Ended;

        public static void EndGame(bool win = false)
        {
            Ended?.Invoke(win);
        }

        public static void Restart()
        {
            Restarted?.Invoke();
        }

        public static void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}