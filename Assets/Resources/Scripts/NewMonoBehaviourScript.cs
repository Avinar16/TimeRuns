using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Rigidbody2D rb2d;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.anyKey)
        {
            Vector2 newpos = rb2d.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rb2d.MovePosition(newpos);
        }
    }
}
