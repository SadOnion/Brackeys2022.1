using UnityEngine;

public class PlayAudio : MonoBehaviour
{
	[SerializeField] AudioClip clip;
	[SerializeField] FloatVariable volume;
	public void Play() => LeanAudio.play(clip, volume.RuntimeValue);

}
