using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameScore : MonoBehaviour
{
    [SerializeField, Header("Ссылка на текстовое поле для обображения счета")]
    private TMP_Text score;

    public void UpdateScoreText(string value)
    {
        score.text = $"Score: {value}";
    }
}