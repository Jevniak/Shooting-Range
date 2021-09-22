using System.Collections.Generic;
using UnityEngine;

namespace Difficulty
{
    public class DifficultyLoad : MonoBehaviour
    {
        [SerializeField] private List<GameObject> difficultyList;

        private void Start()
        {
            Instantiate(difficultyList[PlayerPrefs.GetInt("Difficulty")]);
        }
    }
}
