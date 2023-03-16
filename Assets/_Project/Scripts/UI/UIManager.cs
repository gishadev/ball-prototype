using System.Collections.Generic;
using Gisha.BallGame.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gisha.BallGame.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject losePopup;

        private void OnEnable()
        {
            EventManager.StartListening(Constants.EVENT_LOSE, OnLose);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Constants.EVENT_LOSE, OnLose);
        }

        private void OnLose(Dictionary<string, object> obj)
        {
            losePopup.SetActive(true);
        }

        public void OnClick_Retry()
        {
            GameManager.Instance.Restart();
        }
    }
}