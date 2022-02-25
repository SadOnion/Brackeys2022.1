using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ChangeStartParticleSizeOverTime : MonoBehaviour
{
    [SerializeField] float time = 1f;
    new ParticleSystem particleSystem;

    Vector3 originalSize;

    float timeLeft;

    private void Awake()
    {
        timeLeft = time;
        particleSystem = GetComponent<ParticleSystem>();
        originalSize = new Vector3(
            particleSystem.main.startSizeX.constant, 
            particleSystem.main.startSizeY.constant, 
            particleSystem.main.startSizeZ.constant
        );
    }

    private void Update()
    {
        if(timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            var particlesMain = particleSystem.main;
            particlesMain.startSizeX = new ParticleSystem.MinMaxCurve(originalSize.x * (timeLeft / time));
            particlesMain.startSizeY = new ParticleSystem.MinMaxCurve(originalSize.y * (timeLeft / time));
            particlesMain.startSizeZ = new ParticleSystem.MinMaxCurve(originalSize.z * (timeLeft / time));
        }


    }
}
