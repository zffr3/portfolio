using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    [SerializeField]
    private DoorScript _doorSrc;

    [SerializeField]
    private AudioSource _audioSrc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != null)
        {
            if (this._doorSrc == null)
            {
                return;
            }

            this._doorSrc.CloseDoor();
            this._audioSrc.Play();

            this._doorSrc.tag = "Untagged";

            StartCoroutine(DestroyWithDelay());
        }
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
