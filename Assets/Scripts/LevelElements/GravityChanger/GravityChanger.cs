using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityChanger : MonoBehaviour, Interactable
{
	[SerializeField] GravitySystem gravitySystem;
	[SerializeField] SpriteRenderer[] triangles;

	const float cooldown = 0.5f;
	float lastTimeTriggered;

    private void Awake()
    {
        lastTimeTriggered = Time.time;
	}

    private void Start()
    {
		gravitySystem.onGravityChanged.AddListener(ChangeColor);
    }

    void ChangeColor(GravitySystem.Direction direction)
    {
		if (gravitySystem.CurrentDirection == GravitySystem.Direction.Normal)
			foreach (var triangle in triangles)
			{
				triangle.color = Color.green;
				triangle.transform.rotation = Quaternion.Euler(0, 0, 180);
			}
		else
			foreach (var triangle in triangles)
			{
				triangle.color = Color.red;
				triangle.transform.rotation = Quaternion.Euler(0, 0, 0);
			}
	}

	public void Interact()
	{
		/*OnGravityChange.RaiseEvent();*/
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if((Time.time - lastTimeTriggered) > cooldown && collision.GetComponent<Player>() != null)
        {
			gravitySystem.SwapGravity();
			lastTimeTriggered = Time.time;
        }
	}
}
