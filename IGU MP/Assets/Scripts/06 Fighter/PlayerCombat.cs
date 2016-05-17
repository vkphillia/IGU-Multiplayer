using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerCombat : NetworkBehaviour
{
    public const int maxHealth = 100;
    public bool destroyOnDeath;
    public Sprite[] playerSprite;
    public Sprite[] playerWithGloveSprite;
    public Image healthBar;

    [HideInInspector]
    [SyncVar]
    public int playerNo;

    [HideInInspector]
    [SyncVar(hook = "OnRoundOver")]
    public bool roundWon;

    //Network syncvar
    [SyncVar(hook = "OnHealthChange")]
    public int health;

    [SyncVar(hook = "GloveChange")]
    public bool hasGlove;

    //hard to control WHEN Init is called (networking make order between object spawning non deterministic)
    //so we call init from multiple location (depending on what between spaceship & manager is created first).
    protected bool _wasInit = false;

    protected SpriteRenderer _mySpriteRenderer;

    void Awake()
    {
        //register the spaceship in the gamemanager, that will allow to loop on it.
        GameManager.sFighters.Add(this);
    }

    void Start()
    {
        if (GameManager.sInstance != null)
        {//we MAY be awake late (see comment on _wasInit above), so if the instance is already there we init
            Init();
        }

        _mySpriteRenderer = GetComponent<SpriteRenderer>();
        _mySpriteRenderer.sprite = playerSprite[playerNo];
        Debug.Log("Inside start L=" + isLocalPlayer + "s=" + isServer);
    }

    public void Init()
    {
        if (_wasInit)
            return;

        if(transform.position.y==-3)
        {
            healthBar = UIManager.Instance.healthBar[0];
        }
        else
        {
            healthBar = UIManager.Instance.healthBar[1];
        }
        health = maxHealth;
        _wasInit = true;
        
        OnHealthChange(health);
        Invoke("SetSpeed", 3);
    }

    void SetSpeed()
    {
        if (!isLocalPlayer)
            return;
        GetComponent<PlayerMovementController>().mySpeed = 4;
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        health -= amount;
        if (health <= 0)
        {
            if (destroyOnDeath)
            {
                //Destroy(gameObject);
                //gameObject.SetActive(false);
            }
            else
            {
                health = maxHealth;
                // called on the server, will be invoked on the clients
                //RpcRespawn();
            }

            // Command function is called from the client, but invoked on the server
            StopGame();
        }
    }

    
    void StopGame()
    {
        Debug.Log("Stopping game"+ GameManager.sFighters[0].health +" " + GameManager.sFighters[1].health);

        //GameManager.sInstance._gameRunning = false;

        if (GameManager.sFighters[0].health > GameManager.sFighters[1].health)
        {
            //sFighters[0] won
            GameManager.sFighters[0].roundWon = true;
            GameManager.sFighters[1].roundWon = false;
        }
        else
        {
            //sFighters[1] won
            GameManager.sFighters[1].roundWon = true;
            GameManager.sFighters[0].roundWon = false;
        }
    }

    void GloveChange(bool hasGlove)
    {
        if(hasGlove)
        {
            //GetComponent<Rigidbody2D>().mass *= 10;
            _mySpriteRenderer.sprite = playerWithGloveSprite[playerNo];
            Invoke("RemoveGlove", 5f);
            //gameObject.layer = 0; //layer default
        }
        else
        {
            //GetComponent<Rigidbody2D>().mass /= 10;
            //gameObject.layer = 8; //player
            _mySpriteRenderer.sprite = playerSprite[playerNo];
        }
    
    }

    void RemoveGlove()
    {
        hasGlove = false;
        GloveSpawn.Instance.SpawnGloves();
    }

    void OnHealthChange(int health)
    {
        float amountTemp = (float)health / (float)maxHealth;
        UpdateHealthBar(amountTemp);
    }

    void UpdateHealthBar(float amount)
    {
        healthBar.fillAmount = amount;
    }

    void OnRoundOver(bool won)
    {
        if (!isLocalPlayer)
            return;

        Debug.Log("round over " + won);
        GetComponent<PlayerMovementController>().mySpeed = 0;

        if (won)
        {
            UIManager.Instance.dialouge.text = "You Won";
        }
        else
        {
            UIManager.Instance.dialouge.text = "You Loose";
        }

        StartCoroutine(GameManager.sInstance.ReturnToLoby());
    }
}
