using System;
using System.Collections;
using System.Collections.Generic;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIMainMenuManager : MonoBehaviour
    {
        [SerializeField] private AudioClip audioButtonClick;
        
        [SerializeField] private TMP_InputField username;
        [SerializeField] private Toggle[] togglesDifficulty;
        [SerializeField] private Button buttonPlay;
        [SerializeField] private Button buttonExit;
        [SerializeField] private TMP_Text loadingText;

        [SerializeField] private List<GameObject> windowList;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("PlayerName"))
                username.text = PlayerPrefs.GetString("PlayerName");
            ChangeWindow(0);
            
            for (int i = 0; i < togglesDifficulty.Length - 1; i++)
            {
                togglesDifficulty[i].onValueChanged.AddListener((value) =>
                {
                    SetPlayerPrefsDifficulty();
                    SoundManager.Inst.PlayAudio(audioButtonClick);
                });
            }
            
            

            buttonPlay.onClick.AddListener(() =>
            {
                PlayerPrefs.SetString("PlayerName", username.text);
                SoundManager.Inst.PlayAudio(audioButtonClick);
                ChangeWindow(1);
                StartCoroutine(LoadScene());
            });
            
            buttonExit.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            SoundManager.Inst.PlayAudio(audioButtonClick);
            Application.Quit();
        }
        
        private void ChangeWindow(int id)
        {
            /* id ==
             * 0 - WindowMainMenu
             * 1 - WindowProgressLoadLevel
             */
            foreach (GameObject window in windowList)
            {
                window.SetActive(false);
            }

            windowList[id].SetActive(true);
        }

        private IEnumerator LoadScene()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level");
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                loadingText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

                if (asyncOperation.progress >= 0.9f)
                {
                    loadingText.text = "Press left mouse button";
                    if (Input.GetMouseButtonDown(0))
                        asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey("Difficulty"))
                SetPlayerPrefsDifficulty();
            print(PlayerPrefs.GetInt("Difficulty"));
            togglesDifficulty[PlayerPrefs.GetInt("Difficulty")].isOn = true;
        }

        private void SetPlayerPrefsDifficulty()
        {
            for (int i = 0; i < togglesDifficulty.Length; i++)
            {
                if (togglesDifficulty[i].isOn)
                {
                    PlayerPrefs.SetInt("Difficulty", i);
                }
            }
            print("dif: "+PlayerPrefs.GetInt("Difficulty"));
        }
    }
}