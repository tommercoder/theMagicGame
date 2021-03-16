using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class enemySwordInteract : MonoBehaviour
{
    public CharacterController otherController;
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            otherController = other.GetComponent<CharacterController>();
            otherController.enabled = false;
            other.gameObject.transform.DOMove(other.gameObject.transform.position + (transform.root.forward * 4), 0.2f);
            playerHealth.instance.currentHealth -= damage;
            Debug.Log("other name" + other.name);
            Debug.Log("root" + transform.root.name);
            StartCoroutine(turnOffCharacter());
        }
    }

    IEnumerator turnOffCharacter()
    {
        yield return new WaitForSeconds(2f);
        otherController.enabled = true;
    }
}
