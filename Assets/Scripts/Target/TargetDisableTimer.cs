using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Target
{
    public class TargetDisableTimer : Target
    {
        private void Awake()
        {
            ThisTransform = transform;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
        
        private void OnEnable()
        {
            print($"tag: {tag}");
            StartCoroutine(TimeDisableTarget(ThisTransform, tag));
        }
        
        private IEnumerator TimeDisableTarget(Transform target,string valueTag)
        {
            
            yield return new WaitForSeconds(Random.Range(1.4f, 2.8f));
            DisableTarget(target, valueTag);
        }
    }
}
