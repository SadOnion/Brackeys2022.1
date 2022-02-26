using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Portal : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] float time = 3f;
    [SerializeField] LeanTweenType positionEaseType;
    [SerializeField] VoidEvent onSceneFinished;
    [SerializeField] QuoteIntro quoteIntro;

    private void Awake()
    {
        if (quoteIntro == null)
            quoteIntro = FindObjectOfType<QuoteIntro>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.GetComponent<PlayerController>().EnabledInputs = false;
            Rigidbody2D rigidbody2d = player.GetComponent<Rigidbody2D>();
            //rigidbody2d.velocity = Vector2.zero;
            //rigidbody2d.isKinematic = true;
            rigidbody2d.bodyType = RigidbodyType2D.Static;

            Vector3 originalPos = player.transform.position;
            Vector3 originalScale = player.transform.localScale;

            LeanTween.value(0f, time, time)
                .setOnUpdate((float currentTime) =>
                {
                    player.transform.Rotate(new Vector3(0, 0, -360f) * Time.deltaTime);
                    player.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, currentTime / time);
                });

            player.transform.LeanMove(transform.position, time).setEase(positionEaseType);

            quoteIntro.ShowBackground();
            quoteIntro.onShowBackgroundFinished.AddListener(onSceneFinished.RaiseEvent);
        }
    }
}
