using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BulletHitObject : MonoBehaviour
{
   public int startLivePoints = 5;
   public int livePoins = 5;

   private void Awake()
   {
      Debug.Log("bullethit object initiniated");
   }

   public virtual void ApplyDamage(int damage)
   {
      Debug.Log("applied damage");
      livePoins -= damage;
      var t = CanvasController.Instance.HealthBar.GetComponent<RectTransform>();
      t.localScale = new Vector3(((float)livePoins)/((float)startLivePoints) * 8f,t.localScale.y,t.localScale.z);
      if (livePoins <= 0)
      {
         Debug.Log("you lost");
         CanvasController.Instance.GameOverPage.SetActive(true);
      }
      var d = GetComponent<AudioSource>();
      d.clip = AudioController.Instance.GotHitSound;
      d.Play(0);
   }
}
