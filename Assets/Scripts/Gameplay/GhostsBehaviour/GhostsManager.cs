using UnityEngine;

public class GhostsManager : MonoBehaviour
{
    public static GhostsManager instance;
    public JersonBehaviour playerJerson;
    public CientGhostBehaviour cientGhost;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void OnNewGhostClient()
    {
        playerJerson.StartStateReceivingClient();
    }
}
