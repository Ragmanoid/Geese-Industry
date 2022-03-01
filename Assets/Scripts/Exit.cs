using UnityEngine;

public class Exit : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("Player");
    }
    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.CompareTag("Player")) return;
        var player = hitInfo.GetComponent<PlayerBehaviour>();
        player.Win();
    }
}
