using UnityEngine;

namespace _Scripts.RoundManagement
{
    public class ThanksForPlayingController : MonoBehaviour
    {
        public void LoadMenu()
        {
            SceneController.LoadMenu();
        }

        public void LoadCredits()
        {
            SceneController.LoadCredits();
        }
    }
}