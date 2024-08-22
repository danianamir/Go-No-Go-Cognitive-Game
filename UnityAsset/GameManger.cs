using System;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public float x_size;
    public float y_size;
    private float timer = 0;
    private float beep_timer = 0;

    private int trail_index = 0;

    public int block_index = 0;

    private bool is_current_trail_end = false;

    public float elapse_time_of_trial = 0;


    private AudioSource audioSource;

    public event Action<bool, bool, bool, bool> isgotype_isinfixed_isinshow_isinend;
    public event Action<ShowTime.Actions> send_correct_action;
    public Sprite[] arrow_sprite = new Sprite[11];

    public Sprite[] airplane_shapes = new Sprite[3];
    public Sprite[] distractor_shapes = new Sprite[3];

    public Sprite warning_sprite;
    public Sprite end_sprite;

    public Block[] blocks;

    public bool end_of_game = false;


    private void Start()
    {

        Player player = FindObjectOfType<Player>();
        player.on_player_coorect_respond += end_trail;

        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {

        timer = timer + Time.deltaTime;
        elapse_time_of_trial = elapse_time_of_trial + Time.deltaTime;
        // process_block(blocks[0]);
        process_all_bocks(blocks);

    }




    public void process_all_bocks(Block[] blocks)
    {
        if (block_index < blocks.Length)
        {
            process_block(blocks[block_index]);
        }
        else
        {
            if (end_of_game == false)
            {
                // process the end game sign 
                GameObject ob = new GameObject();
                ob.AddComponent<SpriteRenderer>();
                SpriteRenderer sp = ob.GetComponent<SpriteRenderer>();
                sp.sprite = end_sprite;
                ob.transform.position = new Vector3(10, 6, 0);

                end_of_game = true;
            }

        }

    }
    public void process_block(Block block)
    {

        Trail trial_to_process = block.trails[trail_index];

        process_trial(trial_to_process);
        if (trial_to_process.is_end_of_trial)
        {
            elapse_time_of_trial = 0;
            trail_index = trail_index + 1;
            if (trail_index == block.trails.Length)
            {
                block_index = block_index + 1;
                trail_index = 0;
                timer = 0;
            }
        }



    }
    public bool check_block_validation(Block block)
    {
        return true;
    }

    public void process_trial(Trail trial)
    {


        // check whter the current trail is end by player or it end by itself 

        if (is_current_trail_end)
        {


            timer = 0;
            trial.is_end_of_trial = true;
            is_current_trail_end = false;
            audioSource.Pause();
            GameObject[] s = GameObject.FindGameObjectsWithTag("show_time");
            for (int i = 0; i < s.Length; i++)
            {
                Destroy(s[i]);
            }
        }






        //process the go
        if (trial.is_go)
        {
            //precesss the fixed time of the go
            if (trial.is_in_fixed_time)
            {
                isgotype_isinfixed_isinshow_isinend?.Invoke(true, true, false, false);

                // make the fixed warning
                GameObject ob = new GameObject();
                ob.tag = "show_time";
                ob.transform.position = new Vector3(x_size / 2, y_size / 2, 0);
                ob.AddComponent<SpriteRenderer>();
                SpriteRenderer ob_spr = ob.GetComponent<SpriteRenderer>();
                ob_spr.sprite = warning_sprite;

                if (timer > trial.fixation_time)
                {

                    trial.is_in_fixed_time = false;
                    trial.is_in_show_time = true;
                    timer = 0;

                    //destroy fixed warning
                    GameObject[] s = GameObject.FindGameObjectsWithTag("show_time");
                    for (int i = 0; i < s.Length; i++)
                    {
                        Destroy(s[i]);
                    }
                }
            }
            // process the show time of the go
            if (trial.is_in_show_time)
            {
                isgotype_isinfixed_isinshow_isinend?.Invoke(true, false, true, false);
                if (!trial.show_time_proccesd)
                {
                    process_show_time(trial.showtime);
                    trial.show_time_proccesd = true;

                }


                if (timer > trial.show_time_duration_go)
                {

                    isgotype_isinfixed_isinshow_isinend?.Invoke(true, false, false, true);
                    trial.is_in_show_time = false;
                    trial.is_end_of_trial = true;

                    timer = 0;

                    // destroy all the objects

                    GameObject[] s = GameObject.FindGameObjectsWithTag("show_time");
                    for (int i = 0; i < s.Length; i++)
                    {
                        Destroy(s[i]);
                    }


                }
            }
        }




        // process the nogo
        else
        {

            //precesss fixed time of nogo
            if (trial.is_in_fixed_time)
            {
                isgotype_isinfixed_isinshow_isinend?.Invoke(false, true, false, false);

                // make the fixed warning
                GameObject ob = new GameObject();
                ob.tag = "show_time";
                ob.transform.position = new Vector3(x_size / 2, y_size / 2, 0);
                ob.AddComponent<SpriteRenderer>();
                SpriteRenderer ob_spr = ob.GetComponent<SpriteRenderer>();
                ob_spr.sprite = warning_sprite;

                if (timer > trial.fixation_time)
                {

                    trial.is_in_fixed_time = false;
                    trial.is_in_show_time = true;
                    trial.is_in_stop_signal_delay = true;
                    timer = 0;

                    //destroy fixed warning
                    GameObject[] s = GameObject.FindGameObjectsWithTag("show_time");
                    for (int i = 0; i < s.Length; i++)
                    {
                        Destroy(s[i]);
                    }
                }
            }

            // proccess the show time of nogo
            if (trial.is_in_show_time)
            {
                isgotype_isinfixed_isinshow_isinend?.Invoke(false, false, true, false);
                if (!trial.show_time_proccesd)
                {

                    process_show_time(trial.showtime);
                    trial.show_time_proccesd = true;

                }


                // process the stop signal time
                if (trial.is_in_stop_signal_delay)
                {
                    if (timer > trial.stop_signal_delay)
                    {
                        trial.is_in_stop_signal_delay = false;
                        trial.is_in_beep_duration = true;

                    }
                }


                // process the beep duration
                if (trial.is_in_beep_duration)
                {

                    // play the song
                    if (!trial.beep_duration_proccesed)
                    {
                        beep_timer = 0;
                        audioSource.Play();
                        trial.beep_duration_proccesed = true;
                    }


                    beep_timer = beep_timer + Time.deltaTime;


                    if (beep_timer > trial.beep_duration)
                    {
                        audioSource.Pause();
                        beep_timer = 0;
                    }

                }


                // check the end of the show time of nogo 
                if (timer > trial.show_time_duration_no_go)
                {
                    isgotype_isinfixed_isinshow_isinend?.Invoke(false, false, false, true);
                    trial.is_in_show_time = false;
                    trial.is_end_of_trial = true;
                    audioSource.Pause();
                    beep_timer = 0;
                    timer = 0;

                    GameObject[] s = GameObject.FindGameObjectsWithTag("show_time");

                    for (int i = 0; i < s.Length; i++)
                    {
                        Destroy(s[i]);
                    }
                }

            }





        }





    }












    public void process_show_time(ShowTime sh)
    {


        initialize_airplane(sh, x_size, y_size);

        initialize_distractors(sh, 4, 4, x_size, y_size);





    }

    void end_trail(bool is_trail_end)
    {
        is_current_trail_end = true;
    }



    public void initialize_airplane(ShowTime sh, float x, float y)
    {


        GameObject airplane = new GameObject();
        airplane.transform.localScale = new Vector3(2, 2, 2);
        airplane.tag = "show_time";
        airplane.AddComponent<SpriteRenderer>();
        SpriteRenderer airplane_spr = airplane.GetComponent<SpriteRenderer>();
        airplane_spr.sortingOrder = 0;

        ShowTime.ShapeOfAirPlane shape = sh.shape_of_airplanes;
        ShowTime.DirectionOfAirplane airplane_direction = sh.direction_of_airplane;
        ShowTime.LocationOfAairplane aairplane_location = sh.location_of_airplane;
        ShowTime.ColorOfAirPlane airPlane_color = sh.colorOfAirPlane;
        Vector3 position = new Vector3();
        Vector3 rotation = new Vector3();



        switch (shape)
        {
            case ShowTime.ShapeOfAirPlane.type1:
                airplane_spr.sprite = airplane_shapes[0];
                break;
            case ShowTime.ShapeOfAirPlane.type2:
                airplane_spr.sprite = airplane_shapes[1];
                break;
            case ShowTime.ShapeOfAirPlane.type3:
                airplane_spr.sprite = airplane_shapes[2];
                break;
        }

        switch (airplane_direction)
        {


            case ShowTime.DirectionOfAirplane.left:
                rotation = new Vector3(0, 0, 180);
                sh.correct_action = ShowTime.Actions.left;
                send_correct_action?.Invoke(ShowTime.Actions.left);
                break;
            case ShowTime.DirectionOfAirplane.up:
                rotation = new Vector3(0, 0, 90);
                sh.correct_action = ShowTime.Actions.up;
                send_correct_action?.Invoke(ShowTime.Actions.up);
                break;
            case ShowTime.DirectionOfAirplane.right:
                rotation = new Vector3(0, 0, 0);
                sh.correct_action = ShowTime.Actions.right;
                send_correct_action?.Invoke(ShowTime.Actions.right);
                break;
            case ShowTime.DirectionOfAirplane.down:
                rotation = new Vector3(0, 0, 270);
                sh.correct_action = ShowTime.Actions.down;
                send_correct_action?.Invoke(ShowTime.Actions.down);
                break;


        }



        Vector3[] v = location_of_airplane(x, y);

        switch (aairplane_location)
        {
            case ShowTime.LocationOfAairplane.center:
                position = v[0];
                break;
            case ShowTime.LocationOfAairplane.up:
                position = v[1];
                break;
            case ShowTime.LocationOfAairplane.down:
                position = v[2];
                break;
            case ShowTime.LocationOfAairplane.left:
                position = v[3];
                break;
            case ShowTime.LocationOfAairplane.right:
                position = v[4];
                break;

        }




        switch (airPlane_color)
        {
            case ShowTime.ColorOfAirPlane.color_1:
                airplane_spr.color = new Color(0.7f, 0.9f, 1.0f, 1.0f);

                break;
            case ShowTime.ColorOfAirPlane.color_2:
                airplane_spr.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);

                break;
            case ShowTime.ColorOfAirPlane.color_3:
                airplane_spr.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);

                break;


        }




        airplane.transform.position = position;
        airplane.transform.rotation = Quaternion.Euler(rotation);








    }



    public Vector3[] location_of_airplane(float x, float y)
    {
        Vector3[] v = new Vector3[5];
        Vector3 center = new Vector3((x / 2), (y / 2), 0);
        Vector3 upBoundary = new Vector3(center.x, center.y + y / 4f, 0);
        Vector3 downBoundary = new Vector3(center.x, center.y - y / 4f, 0);
        Vector3 leftBoundary = new Vector3(center.x - x / 4f, center.y, 0);
        Vector3 rightBoundary = new Vector3(center.x + x / 4f, center.y, 0);
        v[0] = center;
        v[1] = upBoundary;
        v[2] = downBoundary;
        v[3] = leftBoundary;
        v[4] = rightBoundary;

        return v;
    }


    public void initialize_distractors(ShowTime sh, float x_distance, float y_distance, float x_area, float y_area)
    {

        Vector3 location = new Vector3();
        Vector3[] v = location_of_airplane(x_area, y_area);

        if (sh.location_of_airplane == ShowTime.LocationOfAairplane.center)
        {
            location = v[0];
        }
        if (sh.location_of_airplane == ShowTime.LocationOfAairplane.up)
        {
            location = v[1];
        }
        if (sh.location_of_airplane == ShowTime.LocationOfAairplane.down)
        {
            location = v[2];
        }
        if (sh.location_of_airplane == ShowTime.LocationOfAairplane.left)
        {
            location = v[3];
        }
        if (sh.location_of_airplane == ShowTime.LocationOfAairplane.right)
        {
            location = v[4];
        }

        ShowTime.ShapeOfDistractors distractor_shape = sh.shapeOfDistractors;
        ShowTime.DirectionOfDistractors distractor_direction = sh.directionOfDistractors;
        ShowTime.ColorOfDistractors distractor_color = sh.colorOfDistractors;

        Vector3[] location_of_distractios = new Vector3[8];
        location_of_distractios[0] = new Vector3(location.x, location.y + y_distance, 0);
        location_of_distractios[1] = new Vector3(location.x, location.y - y_distance, 0);
        location_of_distractios[2] = new Vector3(location.x + x_distance, location.y, 0);
        location_of_distractios[3] = new Vector3(location.x - x_distance, location.y, 0);

        location_of_distractios[4] = new Vector3(location.x - x_distance, location.y - y_distance, 0);
        location_of_distractios[5] = new Vector3(location.x + x_distance, location.y - y_distance, 0);
        location_of_distractios[6] = new Vector3(location.x + x_distance, location.y + y_distance, 0);
        location_of_distractios[7] = new Vector3(location.x - x_distance, location.y + y_distance, 0);

        Vector3 rotation = new Vector3();












        GameObject[] distractors = new GameObject[8];

        SpriteRenderer[] distractors_spr = new SpriteRenderer[8];
        for (int i = 0; i < distractors.Length; i++)
        {
            distractors[i] = new GameObject();
            distractors[i].tag = "show_time";
            distractors[i].AddComponent<SpriteRenderer>();
            distractors_spr[i] = distractors[i].GetComponent<SpriteRenderer>();
            distractors_spr[i].sortingOrder = 0;








            switch (distractor_shape)
            {
                case ShowTime.ShapeOfDistractors.type1:
                    distractors_spr[i].sprite = distractor_shapes[0];
                    break;
                case ShowTime.ShapeOfDistractors.type2:
                    distractors_spr[i].sprite = distractor_shapes[1];
                    break;
                case ShowTime.ShapeOfDistractors.typ3:
                    distractors_spr[i].sprite = distractor_shapes[2];
                    break;
            }









            switch (distractor_direction)
            {


                case ShowTime.DirectionOfDistractors.left:
                    rotation = new Vector3(0, 0, 180);

                    break;
                case ShowTime.DirectionOfDistractors.up:
                    rotation = new Vector3(0, 0, 90);

                    break;
                case ShowTime.DirectionOfDistractors.right:
                    rotation = new Vector3(0, 0, 0);

                    break;
                case ShowTime.DirectionOfDistractors.down:
                    rotation = new Vector3(0, 0, 270);

                    break;


            }



            switch (distractor_color)
            {
                case ShowTime.ColorOfDistractors.color_1:
                    //blue
                    distractors_spr[i].color = new Color(0.7f, 0.9f, 1.0f, 1.0f);

                    break;
                case ShowTime.ColorOfDistractors.color_2:
                    //red
                    distractors_spr[i].color = new Color(1.0f, 0.5f, 0.5f, 1.0f);

                    break;
                case ShowTime.ColorOfDistractors.color_3:
                    //green
                    distractors_spr[i].color = new Color(0.5f, 1.0f, 0.5f, 1.0f);

                    break;


            }



            distractors[i].transform.position = location_of_distractios[i];
            distractors[i].transform.rotation = Quaternion.Euler(rotation);



        }






    }






}
