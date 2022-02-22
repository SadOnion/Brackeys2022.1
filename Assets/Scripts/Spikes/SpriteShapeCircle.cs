using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
public class SpriteShapeCircle : MonoBehaviour
{
    [Range(3, 5)]
    [SerializeField] int points;
    [Range(0f, 30f)]
    [SerializeField] float radius;
    SpriteShapeController spriteShapeController;

    private void Awake()    
    {
        spriteShapeController = GetComponent<SpriteShapeController>();
    }

    private void OnValidate()
    {
        points = Mathf.Max(0, points);
        spriteShapeController = GetComponent<SpriteShapeController>();

        GenerateSpline();
    }

    void GenerateSpline()
    {
        Spline spline = spriteShapeController.spline;
        spline.Clear();

        for (int i = 0; i < points; i++)
        {
            float angle = 360f * (float)-i / points;
            spline.InsertPointAt(i, Quaternion.Euler(0, 0, angle) * Vector3.up * radius);
            spline.SetTangentMode(i, ShapeTangentMode.Continuous);
            float tangentLength = 4f / 3f * Mathf.Tan(Mathf.PI / (2f * points)) * radius;
            Vector3 leftTangent = Quaternion.Euler(0, 0, angle) * Vector3.left * tangentLength;
            Vector3 rightTangent = -leftTangent;
            spline.SetLeftTangent(i, leftTangent);
            spline.SetRightTangent(i, rightTangent);
        }
    }
}
