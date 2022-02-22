using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DefaultGravitable : MonoBehaviour, Gravitable
{
	protected new Rigidbody2D rigidbody2D;
	[SerializeField] protected VoidEvent OnGravityChange;

	protected virtual void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	protected virtual void OnEnable()
	{
		OnGravityChange.OnEventRaised += SwapGravity;
	}
	protected virtual void OnDisable()
	{
		OnGravityChange.OnEventRaised -= SwapGravity;
	}

	public virtual void SwapGravity()
	{
		transform.localScale = new Vector3(
			transform.localScale.x,
			-transform.localScale.y,
			transform.localScale.z
		);
		rigidbody2D.gravityScale *= -1;
	}
}
