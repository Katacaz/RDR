using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AggroDetection : MonoBehaviour
{

    public event Action<Transform> OnAgrro = delegate { };
    private void OnTriggerEnter(Collider other)
    {
        var player = other.CompareTag("Player");

        if (player)
        {
            OnAgrro(other.transform);
            //Debug.Log("Player in Range");
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.CompareTag("Player");
        if (player)
        {
            int randomChar = UnityEngine.Random.Range(0, FindObjectOfType<CharacterManager>().characters.Length);
            Transform newTarget = FindObjectOfType<CharacterManager>().characters[randomChar].transform;
            OnAgrro(newTarget);
        }
    }
}
