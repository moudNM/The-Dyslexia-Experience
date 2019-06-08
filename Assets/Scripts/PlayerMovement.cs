using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject MasterInputControl;
    public Sprite PlayerUp, PlayerDown, PlayerLeft, PlayerRight;//set Sprite
    public Rigidbody2D body; //set body position
    public float speed = 5.0f; //movement speed
    public GameObject curr = null;

    void Start(){
        body = GetComponent<Rigidbody2D>();
    }
	void Update () {

        //change sprite based on Up, Down, Left, Right keys
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerUp;
            body.MovePosition(new Vector2((transform.position.x), (transform.position.y + speed)));

        }else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerDown;
            body.MovePosition(new Vector2((transform.position.x), (transform.position.y - speed)));
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerLeft;
            body.MovePosition(new Vector2((transform.position.x - speed), (transform.position.y)));
        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.GetComponent<SpriteRenderer>().sprite = PlayerRight;
            body.MovePosition(new Vector2((transform.position.x + speed), (transform.position.y)));
        }

        //player action button
        else if(Input.GetKeyDown(KeyCode.E) && curr){ 
            if (curr.CompareTag("person"))
            {
                curr.GetComponent<PersonScript>().action();
            }
            else if (curr.CompareTag("item"))
            {
                curr.GetComponent<ItemScript>().action();
            }

        }

	}

    //check for collisions
    void OnTriggerEnter2D(Collider2D obj)
    {
        //add boxtrigger here
        if(obj.CompareTag("BoxTrigger")){
            MasterInputControl.GetComponent<MasterInputControlScript>().triggerEventMode(obj.gameObject);
        }
        else if (obj.CompareTag("person"))
        {
            curr = obj.gameObject;
        }
        else if(obj.CompareTag("item")){
            curr = obj.gameObject;
        }
    }

    //exit collisions
    void OnTriggerExit2D(Collider2D obj)
    {
            curr = null;
    }

}
