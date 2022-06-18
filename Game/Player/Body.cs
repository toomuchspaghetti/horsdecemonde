using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public static Body Instance {get; private set;} //singleton

    [SerializeField]
    Player player;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    BoxCollider2D collid;
    [SerializeField]
    SpriteRenderer sprite_renderer;

    Vector2 force;
    bool hardstuck; //vicent
    bool launched;

    List<ContactPoint2D> contacts;

    void Awake() {
        Instance = this;
    } 

    // Start is called before the first frame update
    void Start()
    {
        force = Vector2.zero;
        SetHardstuck(false);
        launched = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hardstuck) {
            rb.velocity = force;
        } else {
            float friction_impact = 0f;
            Vector2 new_velocity;

            if (force.x != 0 || player.grounded) { //if moving, it isn't in the "launched" state anymore, it is in control of it's movement
                launched = false;
            }

            new_velocity = rb.velocity + force;

            if (!launched) {
                if (force.x == 0 || (rb.velocity.x != 0 && Mathf.Sign(force.x) != Mathf.Sign(rb.velocity.x)))
                    friction_impact = Mathf.Min(Mathf.Abs(rb.velocity.x), player.friction_amount * Time.deltaTime) * Mathf.Sign(rb.velocity.x); 

                new_velocity.x = Mathf.Clamp(new_velocity.x - friction_impact, -player.max_horizontal_vel, player.max_horizontal_vel);
            }

            rb.velocity = new_velocity;
        }

        force = Vector2.zero;
    }

    void FixedUpdate() {
        contacts = new List<ContactPoint2D>();
        collid.GetContacts(contacts);

        bool new_grounded = false;

        foreach (ContactPoint2D contact in contacts) {
            if (contact.collider.gameObject.layer == 8) { //ground
                float angle = Mathf.Atan2(contact.normal.x, contact.normal.y);
                if (angle <= 0.7853981634f && angle >= -0.7853981634) { //pi/4, 45 degrees in radians
                    new_grounded = true;
                    break;
                }
            }
        }

        player.SetGrounded(new_grounded);


        if (!player.jumpable) {
            if (!new_grounded || Mathf.Round(rb.velocity.y * 1000f) / 1000f == 0) //if off the ground or if the jump literally didn't go through
                player.jumpable = true;
            return;
        }
    }

    public void Walk(float p_movement) {
        force.x += p_movement * Time.deltaTime;
    }

    public void Jump(float p_force) { 
        if (player.jumpable) {
            force = Vector2.up * p_force;
            player.SetGrounded(false);
            player.jumpable = false;
        }      
    }

    public void Launch(Vector2 p_vector) {
        rb.velocity = p_vector;
        launched = true;
        player.SetGrounded(false);
        player.jumpable = false;
    }

    public void Climb(Vector2 p_vector) {
        force = p_vector;
    }

    public void Glide(Vector2 p_vector) {
        force = p_vector;
    }

    public void SetPosition(Vector2 p_position) {
        transform.position = (Vector3) p_position;
    }

    public void ResetVelocity() {
        rb.velocity = Vector2.zero;
        force = Vector2.zero;
    }

    public Vector2 GetPosition() {
        return (Vector2) transform.position;
    }

    public Vector2 GetVelocity() {
        return rb.velocity;
    }

    public void SetHardstuck(bool p_hardstuck) { //vincet
        if (hardstuck == p_hardstuck)
            return;

        hardstuck = p_hardstuck;

        if (hardstuck) {
            rb.gravityScale = 0;
        } else {
            rb.gravityScale = 2;
        }

        rb.velocity = Vector2.zero;
    }

    public void SetSmall(bool p_small) {
        if (p_small) {
            sprite_renderer.size = new Vector2(0.70f, 0.70f);
        } else {
            sprite_renderer.size = Vector2.one;
        }
    }

    public void SetCollisions(bool p_collisions) {
        collid.enabled = p_collisions;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.layer == 9) //ladder
            player.SetLadderable(true);
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.layer == 9) //ladder
            player.SetLadderable(false);
    }

    public BoxCollider2D GetCollider() {
        return collid;
    }
}
