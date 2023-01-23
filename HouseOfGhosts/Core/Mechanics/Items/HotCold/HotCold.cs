using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotCold : MonoBehaviour
{
    private CurvedItem _trackedItem;

    private List<CurvedItem> _findedCurvs;

    [SerializeField]
    private GameObject _flame;
    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private float _exlodeDistance;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioClip _flameClip;

    private bool _isExploded;

    [SerializeField]
    private bool _useAsMarker;
    private bool _activated;
    private Character _characterSource;

    private void Start()
    {
        this._characterSource = GetComponentInParent<Character>();
        this._findedCurvs = new List<CurvedItem>();

        this._activated = false;

        this._isExploded = false;
        EventBus.SubscribeToEvent(EventType.CURVEDITEM_TAKED, ResetCandle);
    }

    private void OnEnable()
    {
        if (!this._isExploded)
        {
            EventBus.Dispath(EventType.CANDLE_ACTIVATED, null, null);
        }
    }

    private void OnDisable()
    {
        this._explosion.SetActive(false);
        this._flame.SetActive(false);

        EventBus.Dispath(EventType.CANDLE_DIACTIVATED, null, null);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.CURVEDITEM_TAKED, ResetCandle);
    }

    private void Update()
    {
        if (this._isExploded)
        {
            return;
        }

        if (!this._useAsMarker)
        {
            if (this._findedCurvs.Count == 0)
            {
                this._flame.SetActive(false);
            }

            if (this._trackedItem != null)
            {
                if (Vector3.Distance(this.transform.position, this._trackedItem.transform.position) <= this._exlodeDistance)
                {
                    Explode();
                }
            }
        }

        if (this._useAsMarker && !this._activated)
        {
            Debug.Log(this._activated = this._characterSource.GetCurrentMarker() == CursedRoomMarkers.Candle);
            this._activated = this._characterSource.GetCurrentMarker() == CursedRoomMarkers.Candle;
            if (this._activated)
            {
                this._flame.SetActive(true);

                this._audioSource.clip = this._flameClip;
                this._audioSource.Play();

                ExplodeAfterDelay();
            }
        }
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this._isExploded || this._useAsMarker)
        {
            return;
        }

        CurvedItem curved = other.GetComponent<CurvedItem>();
        if (curved != null)
        {
            if (this._trackedItem != null && curved != this._trackedItem)
            {
                if (Vector3.Distance(this.transform.position, this._trackedItem.transform.position) > Vector3.Distance(this.transform.position, curved.transform.position))
                {
                    this._trackedItem = curved;
                }
            }
            if (this._trackedItem == null)
            {
                this._trackedItem = curved;
            }

            if (!this._flame.activeSelf)
            {
                this._flame.SetActive(true);

                this._audioSource.clip = this._flameClip;
                this._audioSource.Play();
            }

            this._findedCurvs.Add(curved);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (this._isExploded || this._useAsMarker)
        {
            return;
        }

        CurvedItem curved = other.GetComponent<CurvedItem>();
        if (curved != null)
        {
            if (this._trackedItem == curved)
            {
                this._trackedItem = null;
            }
            this._findedCurvs.Remove(curved);
        }
    }

    private void Explode()
    {
        this._isExploded = true;

        this._flame.SetActive(false);
        this._explosion.SetActive(true);

        this._audioSource.clip = this._explosionClip;
        this._audioSource.Play();

        EventBus.Dispath(EventType.CANDLE_DIACTIVATED, null, null);

        StartCoroutine(DisableExplosion());
    }

    private void ResetCandle(object sender, object param)
    {
        this._explosion.SetActive(false);
        this._isExploded = false;

        EventBus.Dispath(EventType.CANDLE_ACTIVATED, null, null);
    }

    IEnumerator DisableExplosion()
    {
        yield return new WaitForSeconds(2.5f);
        this._explosion.SetActive(false);
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(5f);
        Explode();
    }
}
