using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public static GateController instance;
    public GameObject gatePrefab;
    private int direction;
    public GameObject gate;
    private int lastPos;
    private void OnEnable()
    {
        instance = this;
        ChangePosition();
    }
    private void Update()
    {
        if (BasketPad.instance.GetPosition().x - Ball.instance.GetPosition().x > 0)
        {
            Ball.instance.directionX = 0.5f;
        }
        else
        {
            Ball.instance.directionX = -0.5f;
        }
    }
    public void ChangePosition()
    {
        gate.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Random.Range(0.24f, 4.8f), 1.764677f);
        direction = Random.Range(0, 2);
        if (direction==0)
        {
            if (lastPos==1)
            {
                gate.transform.Rotate(new Vector3(0, 180, 0));
            }
            else
            {
                gate.transform.Rotate(new Vector3(0, 0, 0));
            }
            lastPos = 0;
            //gate.transform.rotation = new Quaternion(0, 90, 0,gate.transform.rotation.w);
            //Ball.instance.directionX = 0.5f;
        }
        else
        {
            if (lastPos == 0)
            {
                gate.transform.Rotate(new Vector3(0, -180, 0));
            }
            else
            {
                gate.transform.Rotate(new Vector3(0, 0, 0));
            }
            lastPos = 1;
            //gate.transform.rotation = new Quaternion(0, -90, 0, gate.transform.rotation.w);
            //Ball.instance.directionX = -0.5f;
        }
    }
    
}
