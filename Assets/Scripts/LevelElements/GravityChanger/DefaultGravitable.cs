using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DefaultGravitable : MonoBehaviour, Gravitable
{
	protected new Rigidbody2D rigidbody2D;
	[SerializeField] protected GravitySystem gravitySystem;

	protected virtual void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		gravitySystem.onGravityChanged.AddListener((GravitySystem.Direction dir) => SwapGravity());
	}

    public virtual void SwapGravity()
	{
		//To jest bardzo robocze xd
		Vector2 offset = new Vector3(0, 0.3125f);
		if (rigidbody2D.gravityScale < 0)
			offset *= -1;

		rigidbody2D.position = rigidbody2D.position + offset;
		transform.position = rigidbody2D.position;

		transform.localScale = new Vector3(
			transform.localScale.x,
			-transform.localScale.y,
			transform.localScale.z
		);

		rigidbody2D.gravityScale *= -1;
	}
}
