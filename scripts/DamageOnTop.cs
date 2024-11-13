using UnityEngine;

public class DamageOnTop : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    private float counter = 0f;
    private void Update() {
        counter += Time.deltaTime;
        var hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, playerLayer);
        if (hit && counter > 2f) {
            hit.transform.gameObject.GetComponent<PlayerController>().TakeDamage(10);
            counter = 0;
        }

    }
}
