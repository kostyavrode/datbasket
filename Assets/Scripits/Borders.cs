using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public Transform left;
    public Transform right;
    public int currentPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player" && Ball.instance.isInteractable)
        {
            if (currentPos==0)
            {
                Ball.instance.Interact();
                other.gameObject.transform.position = new Vector3(right.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            }
            else
            {
                Ball.instance.Interact();
                other.gameObject.transform.position = new Vector3(left.position.x, other.gameObject.transform.position.y, other.gameObject.transform.position.z);
            }
        }
    }
}
