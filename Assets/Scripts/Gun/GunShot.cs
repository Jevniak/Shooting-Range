using System.Collections;
using System.Collections.Generic;
using Sound;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gun
{
    public class GunShot : Gun
    {
        [SerializeField, Header("Префаб пули")] private GameObject bullet;
        [SerializeField] private List<AudioClip> audioShot;
        [SerializeField] private AudioClip audioReload;
        [SerializeField] private GunStatistic gunStatistic;

        private int amount;
    
        private bool reload;
        private bool shotCooldown;

        private AudioSource _audioSource;
        
        protected override void Awake()
        {
            base.Awake();
            amount = gunStatistic.amount;
            _audioSource = GetComponent<AudioSource>();
        }

        protected override void Shot()
        {
            amount--;
            
            if (amount == 0)
            {
                reload = true;
                StartCoroutine(Reload());
            }
            // Создаем пулю
            GameObject newBullet = Instantiate(bullet, thisTransform);
            newBullet.transform.SetParent(null);
            
            _audioSource.clip = audioShot[Random.Range(0,audioShot.Count)];
            _audioSource.Play();
        }

        protected override IEnumerator Reload()
        {
            SoundManager.Inst.PlayAudio(audioReload);
            yield return new WaitForSeconds(gunStatistic.reloadTime);
            amount = gunStatistic.amount;
            reload = false;
            SoundManager.Inst.StopAudio();
        }

        private IEnumerator ShotCooldown()
        {
            shotCooldown = true;
            yield return new WaitForSeconds(gunStatistic.shotCooldown);
            shotCooldown = false;
        }

        private void Update()
        {
            if (!reload && !shotCooldown && Input.GetMouseButtonDown(0) && UIGameTimer.GameStarted)
            {
                Shot();
                StartCoroutine(ShotCooldown());
            }
        }
    }
}
