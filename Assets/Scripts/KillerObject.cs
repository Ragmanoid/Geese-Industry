using UnityEngine;

public class KillerObject : MonoBehaviour
{
    protected GameObject Target { get; set; }
    
    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player")) return;
        var player = hitInfo.GetComponent<PlayerBehaviour>();
        player.Die();
    }
}