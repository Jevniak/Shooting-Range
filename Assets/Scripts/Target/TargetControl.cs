using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Target
{
    public class TargetControl : Target
    {
        [SerializeField, Header("Список целей за которых будем получать очки")]
        private List<Transform> targetList;

        [SerializeField, Header("Список целей за которых будем терять очки")]
        private List<Transform> targetBadList;

        [SerializeField, Header("Перерыв меджу актвациями мишеней")]
        private float timeEnabledTarget = 1f;


        private void Awake()
        {
            foreach (Transform target in targetList)
            {
                target.rotation = Quaternion.Euler(180, 0, 0);
            }

            foreach (Transform target in targetBadList)
            {
                target.rotation = Quaternion.Euler(180, 0, 0);
            }
        }

        protected override void Start()
        {
            StartCoroutine(EnableGodTarget());
            if (targetBadList.Count > 0)
                StartCoroutine(EnableBadTarget());
        }

        private IEnumerator EnableGodTarget()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeEnabledTarget);
                print($"God: {CountActiveTargets}");
                if (CountActiveTargets < targetList.Count)
                {
                    Transform target = GetUnusedRandom(targetList, "God");
                    CountActiveTargets++;
                    EnableTarget(target, "God");
                }
            }
        }

        private IEnumerator EnableBadTarget()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(timeEnabledTarget, timeEnabledTarget + 1f));
                if (CountActiveBadTargets < targetBadList.Count)
                {
                    Transform target = GetUnusedRandom(targetBadList, "Bad");
                    CountActiveBadTargets++;
                    EnableTarget(target, "Bad");
                }
            }
        }


        private Transform GetUnusedRandom(List<Transform> list, string valueTag)
        {
            int maxNumber = list.Count;
            int number = Random.Range(0, maxNumber);

            if (list[number].CompareTag(valueTag))
            {
                return GetUnusedRandom(list, valueTag);
            }
            
            return list[number];
        }
    }
}