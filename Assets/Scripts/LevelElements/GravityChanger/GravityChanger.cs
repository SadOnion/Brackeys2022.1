using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GravityChanger : MonoBehaviour, Interactable
{
	[SerializeField] GravitySystem gravitySystem;
	[SerializeField] SpriteRenderer[] greenTriangles;
	[SerializeField] SpriteRenderer[] redTriangles;

	const float cooldown = 0.5f;
	float lastTimeTriggered;

    private void Awake()
    {
        lastTimeTriggered = Time.time;
	}

    private void Start()
    {
		ChangeColor(gravitySystem.CurrentDirection);
		gravitySystem.onGravityChanged.AddListener(ChangeColor);
    }

    void ChangeColor(GravitySystem.Direction direction)
    {
		if (gravitySystem.CurrentDirection == GravitySystem.Direction.Normal)
		{
			foreach (var triangle in greenTriangles)
				triangle.color = Color.green;
			foreach (var triangle in redTriangles)
				triangle.color = Color.gray;
		}
		else
		{
			foreach (var triangle in greenTriangles)
				triangle.color = Color.gray;
			foreach (var triangle in redTriangles)
				triangle.color = Color.red;
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
