using UnityEngine;
using System.Collections;

public class RotateTile : MonoBehaviour
{
    public GameObject tileMap;
    public float interval = 3.0f;
    private bool isActive = true;
    
    private Collider2D tilemapCollider;

    void Start()
    {
        tilemapCollider = tileMap.GetComponent<Collider2D>();
        StartCoroutine(ToggleTransparencyAndCollider());
    }

    IEnumerator ToggleTransparencyAndCollider()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            isActive = !isActive;
            SetTileMapState(isActive);
        }
    }

    void SetTileMapState(bool active)
    {
        Renderer[] renderers = tileMap.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color color = renderer.material.color;
            color.a = active ? 1f : 0.5f;
            renderer.material.color = color;
        }

        if (tilemapCollider != null)
            tilemapCollider.enabled = active;
    }
}
