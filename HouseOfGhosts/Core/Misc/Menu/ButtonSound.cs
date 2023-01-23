using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSrc;
    [SerializeField]
    private AudioClip _clickClip;

   public void PlayClickSound()
   {
        if (this._audioSrc.clip != this._clickClip)
        {
            this._audioSrc.clip = this._clickClip;
        }

        this._audioSrc.Play();  
   }
}
