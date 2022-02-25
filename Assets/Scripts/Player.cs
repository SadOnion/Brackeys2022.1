using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Onion2D.Movement;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
	[Tooltip("If spawn point is not specified initial player position becomes spawn point")]
	[SerializeField] Transform spawnPoint;
	Vector3 initialPlayerPos;
	[Tooltip("For how long after respawn player isn't able to control character")]
	[SerializeField] float respawnNotMovableTime = 0.4f;
	[SerializeField] PlayerController playerController;
	[SerializeField] ParticleSystem deathParticlesPrefab;
	[SerializeField] GroundCheck groundCheck;
	[SerializeField] AccelerationMovement accelerationMovement;
	[SerializeField] ParticleSystem landingParticles;
	[SerializeField] PlayerAnimationController playerAnimationController;
	[SerializeField] ParticleSystem runningParticles;
	public UnityEvent onKilled = new UnityEvent();
	[SerializeField] AudioClip stepSound;

	Checkpoint currentCheckpoint;

	new Rigidbody2D rigidbody2D;

	bool lastFrameGrounded = true;
	float lastTimeLandParticle;
	float landingParticleCooldown = .1f;

	float timeFromLastStep = 0f;
	float timePerStep = 0.2f;

	private void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D>();
		initialPlayerPos = transform.position;
	}

	private void Update()
	{
		bool grounded = groundCheck.IsGrounded;
		if (grounded && !lastFrameGrounded && Time.time - lastTimeLandParticle > landingParticleCooldown)
		{
			playerAnimationController.PlayJumpAnimation();
			PlayStepSound();
			landingParticles.Play();
			lastTimeLandParticle = Time.time;
		}

		var runningParticlesMain = runningParticles.main;
		runningParticlesMain.loop = grounded && Mathf.Abs(accelerationMovement.HorizontalSpeed) > 3f;
		if (!runningParticles.isPlaying && runningParticlesMain.loop)
			runningParticles.Play();
		lastFrameGrounded = grounded;


        if (grounded)
        {
			if (accelerationMovement.HorizontalSpeed != 0)
				timeFromLastStep += Time.deltaTime;

			if (timeFromLastStep > timePerStep)
			{
				timeFromLastStep = 0f;
				PlayStepSound();
			}
		}
	}

	void PlayStepSound()
    {
		AudioSource audioSource = LeanAudio.play(stepSound);
		audioSource.pitch = Random.Range(1f, 1.5f);
	}

	public void Hit()
	{
		ParticleSystem deathParticles = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
		Respawn();
		onKilled.Invoke();
	}

	public void Respawn()
	{
		lastFrameGrounded = true;
		Vector2 respawnPosition;

		if (currentCheckpoint != null)
		{
			respawnPosition = currentCheckpoint.transform.position;
		}
		else
		{
			if (spawnPoint != null)
				respawnPosition = spawnPoint.position;
			else
				respawnPosition = initialPlayerPos;
		}

		rigidbody2D.position = respawnPosition;
		rigidbody2D.velocity = Vector2.zero;
		playerAnimationController.PlayRespawnAnimation();
		accelerationMovement.ResetSpeed();
		playerController.EnabledInputs = false;
		LeanTween.delayedCall(respawnNotMovableTime, () => playerController.EnabledInputs = true);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
		Checkpoint checkpoint = collision.GetComponent<Checkpoint>();
		if (checkpoint != null)
			currentCheckpoint = checkpoint;
	}

    public void BounceAnimation()
    {
		playerAnimationController.PlayBounceAnimation();
	}
}
