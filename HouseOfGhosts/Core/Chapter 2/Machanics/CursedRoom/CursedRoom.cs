using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedRoom : MonoBehaviour
{
    [SerializeField]
    private bool _isRoomCursed;

    [SerializeField]
    private List<Rigidbody> _cursedTargetBodys;

    private Vector3[] _directions;

    [SerializeField]
    private List<GameObject> _runes;

    [SerializeField]
    private CursedRoomMarkers _currentMarker;

    private bool _isMarkerActive;

    [SerializeField]
    private float _markerActivationCdMin;
    [SerializeField]
    private float _markerActivationCdMax;

    private void Start()
    {
        this._isMarkerActive = false;
        this._directions = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.up};

        this._currentMarker = CursedRoomMarkers.None;
    }

    public void CurseRoom()
    {
        this._isRoomCursed = true;
        this._runes[Random.Range(0, this._runes.Count)].SetActive(true);

        this._currentMarker = (CursedRoomMarkers)Random.Range(0,2);

        ActivateGhostEvent();
    }

    public void FakeCurse()
    {
        ActivateGhostEvent();
    }

    private void ActivateGhostEvent()
    {
        if (this._cursedTargetBodys != null)
        {
            for (int i = 0; i < this._cursedTargetBodys.Count; i++)
            {
                Vector3 direction = this._directions[Random.Range(0, this._directions.Length)];
                this._cursedTargetBodys[i].AddForce(direction, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ActivateMarker());
            other.GetComponent<Character>().ActivateMarker(this._currentMarker);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (this._isRoomCursed)
        {
            if (other.tag == "Player")
            {
                if (this._isMarkerActive)
                {
                    Character chr = other.GetComponent<Character>();
                    if (chr != null)
                    {
                        chr.ActivateMarker(this._currentMarker);

                        StopCoroutine(ActivateMarker());
                        this._isRoomCursed = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine(ActivateMarker());
            other.GetComponent<Character>().ActivateMarker(CursedRoomMarkers.None);
        }
    }

    private IEnumerator ActivateMarker()
    {
        yield return new WaitForSecondsRealtime(Random.Range(this._markerActivationCdMin, this._markerActivationCdMax));

        int seed = Random.Range(0,100);
        if (seed < 50)
        {
            this._isMarkerActive = true;
        }
        else
        {
            StartCoroutine(ActivateMarker());
        }
    }

    public CursedRoomMarkers GetActualMarker()
    {
        return this._currentMarker;
    }
}

public enum CursedRoomMarkers
{
    Emf,
    Thermometr,
    Ouija,
    Candle,
    None
}
