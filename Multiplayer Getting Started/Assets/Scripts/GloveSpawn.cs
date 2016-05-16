using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GloveSpawn : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public int numEnemies;

    //Static Singleton Instance
    public static GloveSpawn _Instance = null;

    //property to get instance
    public static GloveSpawn Instance
    {
        get
        {
            //if we do not have Instance yet
            if (_Instance == null)
            {
                _Instance = (GloveSpawn)FindObjectOfType(typeof(GloveSpawn));
            }
            return _Instance;
        }
    }

    public override void OnStartServer()
    {
        Invoke("SpawnGloves",3f);
        Debug.Log("Inside glove start server L=" + isLocalPlayer + "s=" + isServer);
    }

    public void SpawnGloves()
    {
        Debug.Log("Inside Glove Spawn");
        var pos = new Vector3(0, 0, 0);
        var enemy = (GameObject)Instantiate(enemyPrefab, pos, Quaternion.identity);
        enemy.transform.SetParent(transform,false);
        NetworkServer.Spawn(enemy);
        
        //NetworkServer.Destroy(enemy);
    }
    
}
