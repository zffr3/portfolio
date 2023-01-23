using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    bool _startDestroing;

    private void OnCollisionEnter(Collision collision)
    {
        EventBus.Dispath(EventType.BULLET_COLISTION_ENTER, this,this);

        if (collision.gameObject.tag == "Fragment")
        {
            EventBus.Dispath(EventType.BULLETS_FRAGMENT_TOUCHED, this, this);
        }

        if (!this._startDestroing)
        {
            DestroyAfterDelay();
        }
    }

    private async void DestroyAfterDelay()
    {
        this._startDestroing = true;
        await System.Threading.Tasks.Task.Delay(2000);
        if (this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
    }
}
