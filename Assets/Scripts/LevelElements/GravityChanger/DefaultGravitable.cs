using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DefaultGravitable : MonoBehaviour, Gravitable
{
	protected new Rigidbody2D rigidbody2D;
	[SerializeField] protected GravitySystem gravitySystem;
	[SerializeField] Player player;

	protected virtual void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		gravitySystem.onGravityChanged.AddListener(SwapGravity);
		player.onKilled.AddListener(() => gravitySystem.SwapGravity(GravitySystem.Direction.Normal));
	}

    public virtual void SwapGravity(GravitySystem.Direction dir)
	{
		if((dir == GravitySystem.Direction.Normal && rigidbody2D.gravityScale < 0) || (dir == GravitySystem.Direction.Reverse && rigidbody2D.gravityScale > 0))
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
}
