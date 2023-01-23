using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmfScanner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _onEmfLights;
    [SerializeField]
    private GameObject[] _offEmfLight;

    [SerializeField]
    private float _sphereRadius;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private LayerMask _mask;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private bool _useAsMarker;

    private bool _flashlighted;

    private Character _characterSource;

    // Start is called before the first frame update
    void Start()
    {
        this._flashlighted = false;
        this._audioSource = GetComponent<AudioSource>();
        this._characterSource = GetComponentInParent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
            float emfRecoil = Random.Range(0.01f, 0.1f);
            Vector3 rayOrigin = new Vector3(0.5f + emfRecoil, 0.5f + emfRecoil, 0f); // center of the screen

            // actual Ray
            Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, this._distance))
            {
                GameObject target = hit.collider.gameObject;

                if (target.GetComponent<Ghost>() != null)
                {
                    ActivateLight(5);
                    this._flashlighted = true;
                }
                else if (target.GetComponent<CurvedItem>() != null && !this._useAsMarker)
                {
                    ActivateLight(4 - (int)Mathf.Clamp(Vector3.Distance(this.transform.position, target.transform.position), 0, this._onEmfLights.Length));
                    this._flashlighted = true;
                }
                else
                {
                    if (this._flashlighted)
                    {
                        DisableLight();
                    }
                }
            }
            else
            {
                if (this._flashlighted)
                {
                    DisableLight();
                }
            }

        if (this._useAsMarker)
        {
            if (this._characterSource.GetCurrentMarker() == CursedRoomMarkers.Emf)
            {
                ActivateLight(3);
            }
        }
    }

    private void DisableLight()
    {
        for (int i = 0; i < this._onEmfLights.Length; i++)
        {
            this._onEmfLights[i].SetActive(false);
            this._offEmfLight[i].SetActive(true);
        }

        this._flashlighted = false;
        StopSoud();
    }

    private void ActivateLight(int lightCount)
    {
        for (int i = 0; i < lightCount; i++)
        {
            this._onEmfLights[i].SetActive(true);
            this._offEmfLight[i].SetActive(false);
        }

        EventBus.Dispath(EventType.EMF_HIGHLITED, this, this);

        PlaySound();
    }

    private void PlaySound()
    {
        this._audioSource.Play();
    }


    private void StopSoud()
    {
        this._audioSource.Stop();
    }
}
