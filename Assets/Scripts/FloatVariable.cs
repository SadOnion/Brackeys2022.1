using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : ScriptableObject
{

	public float StartValue;
	[HideInInspector]
	public float RuntimeValue;

	private void OnEnable()
	{
		RuntimeValue = StartValue;
	}
}
