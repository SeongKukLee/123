using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("���� On");
        GetComponent<MeshRenderer>().material.color = Color.green;
    }
}
