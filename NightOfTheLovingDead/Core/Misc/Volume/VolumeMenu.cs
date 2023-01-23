using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : MonoBehaviour
{
    [SerializeField]
    private Slider _volumeSlider;

    [SerializeField]
    private bool _isMusicSlider;

    private void Start()
    {
        if (this._volumeSlider == null)
        {
            this._volumeSlider = this.GetComponent<Slider>();
        }

        if (this._isMusicSlider)
        {
            this._volumeSlider.value  = VolumeData.MusicVolume;
        }
        else
        {
            this._volumeSlider.value = VolumeData.Volume;
        }
    }

    public void ChangeVolumeDatat()
    {
        if (this._isMusicSlider)
        {
            VolumeData.MusicVolume = this._volumeSlider.value;
            PlayerPrefs.SetFloat("Music", this._volumeSlider.value);
        }
        else
        {
            VolumeData.Volume = this._volumeSlider.value;
            PlayerPrefs.SetFloat("Volume", this._volumeSlider.value);
        }
    }
}
