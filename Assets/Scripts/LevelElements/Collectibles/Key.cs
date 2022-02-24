using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : DefaultCollectible
{
    [SerializeField] int openedDoorsId;
    [SerializeField] IntEvent onKeyCollected;
    [SerializeField] ParticleSystem keyCollectParticlesPrefab;
    
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Collect()
    {
        onKeyCollected.RaiseEvent(openedDoorsId);
        var keyParticles = Instantiate(keyCollectParticlesPrefab, transform.position, Quaternion.identity);
        var keyParticlesMain = keyParticles.main;
        keyParticlesMain.startColor = new ParticleSystem.MinMaxGradient((Color.white + spriteRenderer.color) / 2f,  spriteRenderer.color);

    }
}
