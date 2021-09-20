using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetControl : Target
{
    [SerializeField, Header("Список целей за которых будем получать очки")]
    private List<Transform> targetList;

    [SerializeField, Header("Список целей за которых будем терять очки")]
    private List<Transform> targetBadList;

    [SerializeField, Header("Перерыв меджу актвациями мишеней")] private float timeEnabledTarget = 1f;

    private void Awake()
    {
        foreach (Transform target in targetList)
        {
            target.rotation = Quaternion.Euler(180, 0, 0);
        }
    }

    void Start()
    {
        StartCoroutine(EnableTarget());
    }

    private IEnumerator EnableTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeEnabledTarget);
            Transform target = GetUnusedRandom();
            target.rotation = Quaternion.Euler(90, 0, 0);
            target.tag = "Touchable";
        }
    }

    private Transform GetUnusedRandom()
    {
        int maxNumber = targetList.Count;
        int number = Random.Range(0, maxNumber);

        if (targetList[number].CompareTag("Touchable"))
        {
            return GetUnusedRandom();
        }

        return targetList[number];
    }
}