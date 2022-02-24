using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityChanger : MonoBehaviour, Interactable
{
	[SerializeField] VoidEvent OnGravityChange;

	const float cooldown = 1f;
	float lastTimeTriggered;

    private void Awake()
    {
        lastTimeTriggered = Time.time;
	}

    public void Interact()
	{
		/*OnGravityChange.RaiseEvent();*/
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if((Time.time - lastTimeTriggered) > cooldown && collision.GetComponent<Player>() != null)
        {
			OnGravityChange.RaiseEvent();
			lastTimeTriggered = Time.time;
        }
	}
}
