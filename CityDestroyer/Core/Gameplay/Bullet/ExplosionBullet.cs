using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : MonoBehaviour
{
    private List<Rigidbody> _findedColistions;

    [SerializeField]
    private float _explosionForce;
    [SerializeField]
    private float _explosionRadius;

    private void Start()
    {
        this._findedColistions = new List<Rigidbody>();

        EventBus.SubscribeToEvent(EventType.BULLET_COLISTION_ENTER, StartExplosion);
    }

    private void OnDestroy()
    {
        EventBus.UnsubscribeFromEvent(EventType.BULLET_COLISTION_ENTER, StartExplosion);
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherBody = other.GetComponent<Rigidbody>();
        if (otherBody != null)
        {
            this._findedColistions.Add(otherBody);
        }
    }

    private void StartExplosion(object sender, object param)
    {
        Explode();
    }

    private async void Explode()
    {
        Debug.Log("StartExploding");
        await System.Threading.Tasks.Task.Delay(500);
        for (int i = 0; i < this._findedColistions.Count; i++)
        {
            this._findedColistions[i].AddForce(this.transform.forward * this._explosionForce, ForceMode.Impulse);
        }
    }
}
