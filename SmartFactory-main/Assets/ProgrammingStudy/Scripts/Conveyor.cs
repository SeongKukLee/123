using MPS;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float speed = 3;
    public float resetTime = 10;
    public float currentTime = 0;
    public GameObject pushObj;
    public AudioClip clip;
    Vector3 pushObjOriginPos;
    public int plcInputValue;
    bool isConveyorMoving = false;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = 0.5f;
    }

    private void Update()
    {
        if (MPSMxComponent.instance.connection == MPSMxComponent.Connection.Connected)
            if (plcInputValue > 0 && !isConveyorMoving)
                TurnOnConveyor();
            else if(plcInputValue == 0 && !isConveyorMoving)
                audioSource.Stop();
    }

    public void TurnOnConveyor()
    {
        pushObjOriginPos = pushObj.transform.localPosition;
        pushObj.SetActive(true);

        StartCoroutine(CoMovePushObject());

        audioSource.Play();
    }

    IEnumerator CoMovePushObject()
    {
        isConveyorMoving = true;

        while (true)
        {
            currentTime += Time.deltaTime;

            if(currentTime > resetTime)
            {
                currentTime = 0;
                pushObj.transform.localPosition = pushObjOriginPos;
                //pushObj.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                break;
            }

            pushObj.transform.position += (-transform.forward) * Time.deltaTime * speed; 

            yield return new WaitForSeconds(Time.deltaTime);
        }

        isConveyorMoving = false;
    }
}
