﻿using _Scripts.RoundManagement;
using UnityEngine;

namespace _Scripts.Menu
{
    public class CreditsController : MonoBehaviour
    {
        public void GoToMenu()
        {
            SceneController.LoadMenu();
        }
    }
}