using UnityEngine;
using System.Collections;

public class GloveControl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerCombat>().hasGlove = true;
        Destroy(this.gameObject);
    }

}
