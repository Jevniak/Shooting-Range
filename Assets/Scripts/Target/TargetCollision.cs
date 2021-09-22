using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Target
{
    public class TargetCollision : Target
    {
        [SerializeField] 
        private List<AudioClip> impact;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag($"Bullet") && !coll.gameObject.CompareTag("Untagged"))
            {
                _audioSource.clip = impact[Random.Range(0, impact.Count)];
                _audioSource.Play();
                switch (tag)
                {
                    case "God":
                        ScoreSystem.Inst.ChangeCurrentScore(1);
                        break;
                    case "Bad":
                        ScoreSystem.Inst.ChangeCurrentScore(-1);
                        break;
                }

                DisableTarget(ThisTransform, tag);
            }
        }
    }
}