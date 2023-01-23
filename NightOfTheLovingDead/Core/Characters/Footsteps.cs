using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private CharacterController _controllerSrc;

    [SerializeField]
    private AudioClip _stepSound;

    [SerializeField]
    private AudioSource _stepSoundSrc;

    private bool _stepCd;

    // Start is called before the first frame update
    void Start()
    {
        if (this._controllerSrc == null)
        {
            this._controllerSrc = this.GetComponent<CharacterController>();
        }

        if (this._stepSoundSrc == null)
        {
            this._stepSoundSrc = this.GetComponent<AudioSource>();
        }

        this._stepSoundSrc.clip = this._stepSound;
        this._stepCd = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this._controllerSrc.velocity.magnitude >= 1f && !this._stepSoundSrc.isPlaying && this._stepCd)
        {
            this._stepSoundSrc.Play();
            this._stepCd = false;
            StartCoroutine(ResetCooldown());
        }
    }

    IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(0.85f);
        this._stepCd = true;
    }
}
