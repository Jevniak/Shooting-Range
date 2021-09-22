using System;
using System.Collections;
using System.Globalization;
using Target;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIGameTimer : MonoBehaviour
    {
        public static bool GameStarted;

        [SerializeField, Header("Время игры в секундах")]
        private int timeToEnd;

        [SerializeField, Header("Ссылка на текстовое поле для таймера")]
        private TMP_Text timer;

        [SerializeField, Header("Окно игры")] 
        private GameObject windowGame;

        [SerializeField, Header("Окно с результатом")] 
        private GameObject windowResult;

        [SerializeField] private Button buttonMainMenu;

        private void Awake()
        {
            buttonMainMenu.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainMenu");
            });
        }

        private void Start()
        {
            GameStarted = true;
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            while (timeToEnd > 0)
            {
                string minutes = Math.Truncate((decimal)(timeToEnd / 60)).ToString(CultureInfo.InvariantCulture);
                string seconds = (timeToEnd % 60).ToString();
                timer.text = $"{minutes}:{(seconds.Length == 1 ? 0 + seconds : seconds)}";
                timeToEnd--;
                yield return new WaitForSeconds(1);
            }
            timer.text = "0:00";
            FinishGame();
        }

        private void FinishGame()
        {
            GameStarted = false;
            windowGame.SetActive(false);
            windowResult.SetActive(true);
            ScoreSystem.Inst.SaveCurrentPlayer();
        }
    }
}