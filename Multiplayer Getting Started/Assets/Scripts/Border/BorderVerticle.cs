using UnityEngine;
using System.Collections;

public class BorderVerticle : MonoBehaviour
{
	void OnTriggerEnter2D (Collider2D other)
	{
		//if (other.gameObject.layer == 8)
		//{
			other.transform.Rotate (0, 0, (360 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
        //}
        //if (other.gameObject.layer == 9)
        //{
        //	other.transform.Rotate (0, 0, (360 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
        //}
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //  //  Debug.Log("collided");
    //    //other.gameObject.GetComponent<Rigidbody2D>().velocity = -other.gameObject.GetComponent<Rigidbody2D>().velocity;
    //    other.transform.Rotate(0, 0, (360 - other.transform.rotation.eulerAngles.z) - other.transform.rotation.eulerAngles.z);
    //    other.gameObject.GetComponent<Rigidbody2D>().AddForce(other.transform.up * 10);
    //}
}
