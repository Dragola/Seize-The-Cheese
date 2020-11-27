using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeCubeScript : MonoBehaviour
{

    public GameObject player;
    private BoxCollider2D boxCollider;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            boxCollider = player.GetComponent<BoxCollider2D>();
            boxCollider.size = new Vector2(3.96f, 5.82f);
            boxCollider.offset = new Vector2(0, 0);
            gameObject.transform.parent = null;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
            Debug.Log("Dropped");

        }
    }
}
