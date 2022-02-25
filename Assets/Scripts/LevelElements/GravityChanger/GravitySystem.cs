using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/GravitySystem")]
public class GravitySystem : ScriptableObject
{
	public enum Direction { Normal, Reverse }

	public Direction CurrentDirection { get; private set; }

	public UnityEvent<Direction> onGravityChanged;
	[SerializeField] AudioClip normalGravity;
	[SerializeField] AudioClip reverseGravity;
	[SerializeField] FloatVariable volume;


	private void OnEnable()
	{
		CurrentDirection = Direction.Normal;
		onGravityChanged = new UnityEvent<Direction>();
	}

	public void SwapGravity(Direction direction)
	{
		CurrentDirection = direction;
		onGravityChanged.Invoke(CurrentDirection);
	}

	public void SwapGravity()
	{
		if (CurrentDirection == Direction.Normal)
		{
			CurrentDirection = Direction.Reverse;
			LeanAudio.play(reverseGravity, volume.RuntimeValue);
		}
		else
		{
			LeanAudio.play(normalGravity, volume.RuntimeValue);
			CurrentDirection = Direction.Normal;
		}

		onGravityChanged.Invoke(CurrentDirection);
	}
}
