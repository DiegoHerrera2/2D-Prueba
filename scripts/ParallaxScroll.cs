using UnityEngine;
public class ParallaxScroll : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private Renderer _renderer;
        
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }
        
    private void Update()
    {
        var materials = _renderer.materials;
        for (var i = 0; i < materials.Length; i++)
        {
            var offset = materials[i].mainTextureOffset;
            offset.x += Time.deltaTime * speed * (i + 1);
            materials[i].mainTextureOffset = offset;
        }
    }
}
