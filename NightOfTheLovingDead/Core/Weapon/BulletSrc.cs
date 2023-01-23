using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSrc : MonoBehaviour
{
    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject muzzleParticle;
    public GameObject[] trailParticles;
    [Header("Adjust if not using Sphere Collider")]
    public float colliderRadius = 1f;
    [Range(0f, 1f)]
    public float collideOffset = 0.15f;

    private GameObject impactP;

    void Start()
    {
        projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation);
        projectileParticle.transform.parent = transform;
        if (muzzleParticle != null)
        {
            muzzleParticle = Instantiate( muzzleParticle, transform.position, transform.rotation) as GameObject;
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        float rad;
        if (transform.GetComponent<SphereCollider>())
            rad = transform.GetComponent<SphereCollider>().radius;
        else
            rad = colliderRadius;

        Vector3 dir = transform.GetComponent<Rigidbody>().velocity;
        if (transform.GetComponent<Rigidbody>().useGravity)
            dir += Physics.gravity * Time.deltaTime;
        dir = dir.normalized;

        float dist = transform.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;

        if (Physics.SphereCast(transform.position, rad, dir, out hit, dist))
        {
            transform.position = hit.point + (hit.normal * collideOffset);

            this.impactP = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
            this.StartCoroutine(WaitAndDestroyBullet(3f));
        }
    }

    IEnumerator WaitAndDestroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(this.gameObject);
    }
}
