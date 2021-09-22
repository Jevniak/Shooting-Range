using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

namespace Target
{
    public class ScoreSystem : MonoBehaviour
    {
        #region variables

        [SerializeField] private int currentScore;

        public static ScoreSystem Inst;

        [SerializeField] private string xmlPath;
        [SerializeField] private string xmlName;

        [XmlRoot("Leaderboard"), Serializable]
        public class Leaderboard
        {
            [XmlElement("Score")] public List<Score> score;
        }

        [XmlRoot("Score"), Serializable]
        public class Score
        {
            [XmlElement("Name")] public string name;
            [XmlElement("Value")] public string value;
        }

        [SerializeField] private Leaderboard leaderboard;

        private UIGameScore _uiGameScore;

        [SerializeField] private GameObject leaderboardParent;
        [SerializeField] private GameObject leaderboardChildPrefab;

        #endregion


        private void Awake()
        {
            Inst = this;
            if (TryGetComponent(out UIGameScore uiGameScore))
            {
                _uiGameScore = uiGameScore;
            }
        }

        private void Start()
        {
            CheckXMLExist();
            if (leaderboardParent && leaderboardChildPrefab)
                ShowLeaderboard();
        }

        private string _playerName = "Player";

        private void CheckXMLExist()
        {
            if (File.Exists(xmlPath+"/"+xmlName))
            {
                // получаем лидерборд если существует файл
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Leaderboard));
                FileStream fileStream = new FileStream(xmlPath+"/"+xmlName, FileMode.Open);
                leaderboard = xmlSerializer.Deserialize(fileStream) as Leaderboard;
                fileStream.Close();
            }
            else
            {
                // если не существует файла

                // сохраняем файл
                SaveResult();
            }
        }

        private void ShowLeaderboard()
        {
            foreach (Score score in leaderboard.score)
            {
                GameObject leaderboardItem = Instantiate(leaderboardChildPrefab, leaderboardParent.transform);
                leaderboardItem.transform.GetChild(0).GetComponent<TMP_Text>().text = score.name;
                leaderboardItem.transform.GetChild(1).GetComponent<TMP_Text>().text = score.value;
            }
        }

        private void SaveResult()
        {
            if (!Directory.Exists(xmlPath))
            {
                Directory.CreateDirectory(xmlPath);
            }
            
            // сохраняем лист в xml
            XmlSerializer serializer = new XmlSerializer(leaderboard.GetType());
            StreamWriter writer = new StreamWriter(xmlPath+"/"+xmlName);
            serializer.Serialize(writer.BaseStream, leaderboard);
            writer.Close();
        }

        public void SaveCurrentPlayer()
        {
            // добавляем текущего пользователя (его имя и результат)
            if (PlayerPrefs.HasKey("PlayerName"))
                _playerName = PlayerPrefs.GetString("PlayerName");
            leaderboard.score.Add(new Score()
            {
                name = _playerName,
                value = currentScore.ToString(),
            });

            //сортируем по убыванию
            leaderboard.score = leaderboard.score.OrderByDescending(t => Convert.ToInt32(t.value)).ToList();

            //удаляем лишнии записи ( в данном случае не больше 5)
            for (int i = leaderboard.score.Count - 1; i > 4; i--)
            {
                leaderboard.score.RemoveAt(i);
            }

            SaveResult();
        }

        public void ChangeCurrentScore(int value)
        {
            currentScore += value;
            _uiGameScore.UpdateScoreText(currentScore.ToString());
        }
    }
}