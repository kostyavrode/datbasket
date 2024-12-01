using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject startEffect;
    [SerializeField] private GameObject fireEffect;
    public bool isInteractable;
    public float directionX;
    private void Awake()
    {
        instance = this;
        isInteractable = true;
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("Buy1"))
        {
            startEffect.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Buy2"))
        {
            fireEffect.SetActive(true);
        }
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Interact()
    {
        isInteractable = false;
        StartCoroutine(WaitForInteract());
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            audioSource.Play();
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.IsGameStarted())
        {
            rb.AddForce(new Vector3(directionX/4, 1, 0) * 300);
        }
        if (GameManager.instance.IsGameStarted() && transform.position.y>8.5f)
        {
            transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
    private IEnumerator WaitForInteract()
    {
        yield return new WaitForSeconds(0.2f);
        isInteractable = true;
    }
}
