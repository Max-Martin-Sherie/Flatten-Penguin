using UnityEngine;

public class OnCollideWithPlayerIgnoreHim : MonoBehaviour
{
    private void OnCollisionEnter(Collision p_collision)
    {
        if (p_collision.gameObject.layer == 6) return;
        Physics.IgnoreLayerCollision(p_collision.gameObject.layer, gameObject.layer);
        Destroy(this);
    }
}
