using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketPad : MonoBehaviour
{
    public static BasketPad instance;
    public ParticleSystem goalEffect;
    public SphereCollider sphereCollider;
    private void Awake()
    {
        instance = this;
    }
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            GameManager.instance.AddScore();
            goalEffect.Play();
            StartCoroutine(DeactivateSphere());
        }
    }
    private IEnumerator DeactivateSphere()
    {
        sphereCollider.enabled = false;
        yield return new WaitForSeconds(1);
        sphereCollider.enabled = true;
    }
}
