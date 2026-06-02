using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class WallHealthDisplay : MonoBehaviour
{
    [SerializeField] private Wall wall;

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        if (wall == null)
            return;

        textMesh.text = wall.HealthText;
    }
}