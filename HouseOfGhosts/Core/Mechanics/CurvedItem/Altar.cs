using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    [SerializeField]
    private GameObject _altarVFX;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _correctSound;
    [SerializeField]
    private AudioClip _wrongSound;

    [SerializeField]
    private int _altarHealthCount;

    [SerializeField]
    private GameObject _deathDemonObject;

    [SerializeField]
    private int _wrongItemsCount;

    public void ActivateAltar(bool isItemCurved)
    {
        this._altarVFX.SetActive(true);

        PlayAltarSound(isItemCurved);

        if (isItemCurved)
        {
            this._altarHealthCount--;
            if (this._altarHealthCount <= 0)
            {
                KillAltar();
            }
        }
    }

    public void TakeWrongItem()
    {
        this._wrongItemsCount--;
        if (this._wrongItemsCount <= 0)
        {
            EventBus.Dispath(EventType.KILL_PLAYER, this, this);
        }
    }

    private void PlayAltarSound(bool isItemCurved)
    {
        if (isItemCurved)
        {
            this._audioSource.clip = this._correctSound;
        }
        else
        {
            this._audioSource.clip = this._wrongSound;
        }

        this._audioSource.Play();
    }

    private void KillAltar()
    {
        Debug.Log("Called");
        this._deathDemonObject.SetActive(true);
        EventBus.Dispath(EventType.KILL_DEMON,this,this);
        Destroy(this.gameObject);
    }
}
