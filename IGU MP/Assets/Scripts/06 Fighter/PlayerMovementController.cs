using UnityEngine;
using System.Collections;
using UnityEngine.Networking;


public class PlayerMovementController : NetworkBehaviour
{
	//private int myNo;
    [SyncVar]
    public float mySpeed;

    protected Rigidbody2D _myRigidbody2d;

    void Start()
    {
        //Debug.Log(transform.rotation.z);
        if(transform.rotation.z==1)
        {
            Camera.main.transform.rotation = new Quaternion(0, 0, 1, 0);
        }
        _myRigidbody2d= GetComponent<Rigidbody2D>();
    }
    
    void Update ()
	{
        if (!isLocalPlayer)
            return;
        
            //_myRigidbody2d.velocity = transform.up * mySpeed;
            KeyboardControls();
            MobileControls();
        
    }
    
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        _myRigidbody2d.velocity = transform.up * mySpeed;
        //GetComponent<Rigidbody2D>().AddForce(transform.up * 10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer==8 && GetComponent<PlayerCombat>().hasGlove)
        {
            other.transform.eulerAngles += 180f * Vector3.forward;
            transform.eulerAngles += 90f * Vector3.forward;
            other.GetComponent<Rigidbody2D>().velocity = transform.up * mySpeed*2;
            other.gameObject.GetComponent<PlayerCombat>().TakeDamage(10);
        }
    }

	void KeyboardControls ()
	{
		
			if (Input.GetButton ("moven"))
			{
				MoveClockWise ();
			}
			else if (Input.GetButton ("movem"))
			{
				MoveAntiClockWise ();
			}
	}

	void MobileControls ()
	{
        int count = Input.touchCount;
        for (int i = 0; i < count; i++)
        {
            Touch touch = Input.GetTouch(i);

            if (touch.position.x < Screen.width / 2 && touch.position.y < Screen.height / 2)
            {
                MoveClockWise();
            }
            else if (touch.position.x > Screen.width / 2 && touch.position.y < Screen.height / 2)
            {
                MoveAntiClockWise();
            }
        }
		
	}

	void MoveClockWise ()
	{
		transform.Rotate (0, 0, 7);
	}

	void MoveAntiClockWise ()
	{
		transform.Rotate (0, 0, -7);
	}

	void OnDestroy ()
	{
		//GameManager.GetPlayerNo -= getPlayerNo;
	
	}
}
