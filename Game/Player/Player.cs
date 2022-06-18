using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    None, //Normal
    Laddering, //on a ladder
    Piping //leave all control of body to Pipe.cs
}

public class Player : MonoBehaviour
{
    public static Player Instance {get; private set;} //singleton

    public bool grounded {get; private set;}
    public bool jumpable {get; set;} //this will be false in the moment between the player jumping and Unity recognizing that the player is off the ground
    public bool exiting_pipe {get; set;}
    public PlayerState state {get; private set;}

    //cool values
    [SerializeField]
    public float max_horizontal_vel;
    [SerializeField]
    public float friction_amount;
    [SerializeField]
    float speed;
    [SerializeField]
    float climbing_speed;
    [SerializeField]
    float jump_force;

    //serialized classes
    [SerializeField]
    Body body;
    [SerializeField]
    Interacter interacter;
    [SerializeField]
    Transform interacter_tsf;
    [SerializeField]
    Animator body_animator;

    bool ladderable; //able to climb ladder?
    short direction; //-1 is left, 1 is right

    string current_animation; //This is the variable that holds what is the current animation.

    List<Item> items;

    AudioClip jump_sound;

    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        grounded = true;
        jumpable = true;
        ladderable = false;
        state = PlayerState.None;
        direction = 1;
        current_animation = "Idle_Left";
        items = new List<Item>();

        jump_sound = Resources.Load<AudioClip>("Sounds/newjump");
    }

    // Update is called once per frame
    void Update()
    {
        short horizontal_axis = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
            horizontal_axis -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            horizontal_axis += 1;

        if (state == PlayerState.Piping) {

        } else if (state == PlayerState.Laddering) {
            short vertical_axis = 0;

            if (Input.GetKey(KeyCode.UpArrow)) {
                vertical_axis += 1;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                vertical_axis -= 1;
            }

            body.Climb(new Vector2(horizontal_axis, vertical_axis) * climbing_speed);
        } else {
            if (horizontal_axis != 0) {
                body.Walk(horizontal_axis * speed);
                direction = horizontal_axis;
            }

            if (grounded && jumpable) {
                if (Input.GetKey(KeyCode.UpArrow)) {
                    body.Jump(jump_force);
                    AudioController.Instance.PlaySound(jump_sound);
                    //waiting_for_extra_grounded_set = true;
                }
            }
        }

        interacter_tsf.position = body.transform.position + new Vector3(direction * 0.625f, 0, 0);

        if (state != PlayerState.Piping) {
            if (Input.GetKeyDown(KeyCode.Z)) {
                if (ladderable) {
                    SetLaddering(state == PlayerState.None); //flips the state, if yes then no, if no then yes
                } else {
                    interacter.Interact(); //coding super intense
                }
            }
        }  

        //animation clusterfrick
        string direction_text = direction == -1 ? "Left" : "Right";
        Vector2 velocity = body.GetVelocity();
        bool should_animation_play = true; // when laddering but not moving, the animation won't advance/play

        if (state == PlayerState.Piping) {
            PlayAnimation("Land_" + direction_text);
        } else if (state == PlayerState.Laddering) {
            //...
            PlayAnimation("Ladder");
            if (velocity == Vector2.zero) { // when laddering but not moving, the animation won't advance/play
                should_animation_play = false;
            }
        } else {
            if (grounded) {
                if (Mathf.Abs(velocity.x) > 0.5) { //walking
                    PlayAnimation("Walk_" + direction_text);
                } else { //idle
                    PlayAnimation("Idle_" + direction_text);
                }
            } else { //jumping
                if (velocity.y > 2) {
                    PlayAnimation("TakeOff_" + direction_text);
                } else if (velocity.y < -1.5f) {
                    PlayAnimation("Land_" + direction_text);
                } else {
                    PlayAnimation("MidJump_" + direction_text);
                }
            }
        }

        body_animator.speed = should_animation_play ? 1f : 0f;
    }

    public void SetPiping(bool p_piping) {
        SetState(PlayerState.Piping, p_piping);
        body.SetHardstuck(p_piping);
        body.SetCollisions(!p_piping);
        body.SetSmall(p_piping);
    }

    public void SetLadderable(bool p_ladderable) {
        ladderable = p_ladderable;

        if (!ladderable) {
            SetLaddering(false);
        } 
    }

    public void SetLaddering(bool p_laddering) {
        if (p_laddering == (state == PlayerState.Laddering)) //if p_laddering is true and state is laddering too
            return;

        SetState(PlayerState.Laddering, p_laddering);
        body.SetHardstuck(p_laddering);
    }

    public void SetGrounded(bool p_grounded) {
        grounded = p_grounded;
    }

    void PlayAnimation(string p_animation) {
        if (current_animation == p_animation)
            return;

        body_animator.Play(p_animation);

        current_animation = p_animation;
    }

    void SetState(PlayerState p_state, bool p_truth) {
        state = (p_truth ? p_state : PlayerState.None);
    }

    //inventory stuff
    public void AddItem(Item p_item) {
        items.Add(p_item);
    }

    public bool HasItem(Item p_item) {
        return items.Contains(p_item);
    }

    public void RemoveItem(Item p_item) {
        items.Remove(p_item);
    }
}
