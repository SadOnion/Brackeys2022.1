using UnityEngine;

namespace Onion2D.Movement
{
	public class GroundCheck : MonoBehaviour
	{
		[SerializeField] Collider2D groundCheckUnderThisCollider;
		[SerializeField] LayerMask groundLayer;
		[SerializeField] bool drawGizmo;
		[SerializeField] float groundBoxSize = .15f;
		public Vector2 Center
		{
			get
			{
				Vector2 center = groundCheckUnderThisCollider.bounds.center - Vector3.up * groundCheckUnderThisCollider.bounds.extents.y;
				center.y -= groundBoxSize * .5f;
				return center;
			}
		}
		public bool IsGrounded => CheckForGround();

		public bool CheckForGround()
		{
			var col = Physics2D.OverlapBox(Center, new Vector2(groundCheckUnderThisCollider.bounds.size.x, groundBoxSize), 0, groundLayer);
			return col is null ? false : true;
		}

		void OnDrawGizmos()
		{
			if (drawGizmo)
			{
				Gizmos.color = Color.red;
				Vector2 size = new Vector2(groundCheckUnderThisCollider.bounds.size.x, groundBoxSize);
				Gizmos.DrawWireCube(Center, size);
			}
		}
	}
}
