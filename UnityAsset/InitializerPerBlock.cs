using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;




public class InitializerPerBlock : MonoBehaviour
{
    public List<string> jsonObjects = new List<string>();

    [System.Serializable]
    public struct Block_str
    {

        public int go;
        public int nogo;

        public Vector2 fixation_time;
        public Vector2 show_time_duration_go;
        public Vector2 show_time_duration_no_go;
        public Vector2 stop_signal_delay;

        public Vector2 beep_duration;






        // direction............................
        public Default_Airplane_Direction default_Airplane_Direction;
        public Distractor_Direction_is_in_Aiplane_Direction distractor_Direction_Is_In_Aiplane_Direction;




        // location ................................
        public Defualt_Airplane_Location defualt_Airplane_Location;
        public Airplane_location_constant airplane_location_Constant;


        // shape ............................................

        public Defualt_Airplane_Shape defualt_Airplane_Shape;

        public Default_Distractor_shape default_Distractor_Shape;

        public Airplane_Shape_constant airplane_shape_Constant;

        public Distractor_shape_constant distractor_Shape_Constant;



        // color ...............................................

        public Defualt_Airplane_Color defualt_Airplane_Color;

        public Default_Distractor_color default_Distractor_Color;

        public Airplane_Color_constant airplane_Color_Constant;

        public Distracor_color_constant distracor_Color_Constant;


    }

    [SerializeField]
    public List<Block_str> block_Str = new List<Block_str>();













    //direction .....................................................................................
    public enum Default_Airplane_Direction
    {
        alldirection,
        not_left,
        not_right,
        not_up,
        not_down,
        left_right,
        up_down,
        left_up,
        left_down,
        right_down,
        right_up,
    }

    public enum Distractor_Direction_is_in_Aiplane_Direction

    {
        yes,
        no,
    }

    //location.................................................................................................................

    public enum Defualt_Airplane_Location
    {
        center,
        up,

        down,

        left,

        right,


    }
    public enum Airplane_location_constant
    {
        constant,
        variable,

    }


    //shape......................................................................................


    public enum Defualt_Airplane_Shape
    {
        type_1,
        type_2,
        type_3,
    }

    public enum Default_Distractor_shape
    {
        type_1,
        type_2,
        type_3,
    }

    public enum Airplane_Shape_constant
    {
        constant,
        variable,

    }

    public enum Distractor_shape_constant
    {
        constant,
        variable,
    }



    //color......................................................................................



    public enum Defualt_Airplane_Color
    {
        color_1,
        color_2,
        color_3,
    }


    public enum Default_Distractor_color
    {
        color_1,
        color_2,
        color_3,
    }


    public enum Airplane_Color_constant
    {
        constant,
        variable,
    }


    public enum Distracor_color_constant
    {
        constant,
        variable,
    }
    private Initializer initializer;


    //........................................................................................................................



    private void Start()
    {






        string jsonText = File.ReadAllText(start.adress);

        if (jsonText == null)
        {
            Debug.Log("empty jsonnnnnn");
        }
        else
        {

            int startIndex = jsonText.IndexOf("{");
            int endIndex = 0;
            bool x = true;

            while (x)
            {
                // Debug.Log(startIndex);
                // Find end index of current object
                endIndex = jsonText.IndexOf("},{", startIndex) + 1;
                // Debug.Log(endIndex);


                if (endIndex < startIndex)
                {
                    endIndex = jsonText.IndexOf("]", startIndex);
                    // Extract object JSON 
                    string element = jsonText.Substring(startIndex, endIndex - startIndex);
                    // Debug.Log(element);
                    // Add to array
                    jsonObjects.Add(element);
                    x = false;
                }
                else
                {
                    // Extract object JSON 
                    string element = jsonText.Substring(startIndex, endIndex - startIndex);
                    //Debug.Log(element);
                    // Add to array
                    jsonObjects.Add(element);
                    // Set start to search for next object
                    startIndex = endIndex + 1;


                }


            }





            for (int i = 0; i < jsonObjects.Count; i++)
            {

                block_Str.Add(JsonUtility.FromJson<Block_str>(jsonObjects[i]));
            }



            if (check_before_initial_block())
            {
                initializer = FindObjectOfType<Initializer>().GetComponent<Initializer>();
                initial_block();
            }
            else
            {
                Debug.Log("condition not satisfied");
            }


        }


    }

    public void initial_block()
    {
        initializer.block_str = new Initializer.Block_str[block_Str.Count];


        for (int i = 0; i < block_Str.Count; i++)
        {
            initializer.block_str[i].block_trial_go = new Initializer.Trial_go_str[block_Str[i].go];
            initializer.block_str[i].block_trial_nogo = new Initializer.Trial_nogo_str[block_Str[i].nogo];
            float fixed_time = Random.Range(block_Str[i].fixation_time.x, block_Str[i].fixation_time.y)/1000f;

            // direction
            Default_Airplane_Direction default_Airplane_Direction = block_Str[i].default_Airplane_Direction;
            Distractor_Direction_is_in_Aiplane_Direction distractor_Direction_Is_In_Aiplane_Direction = block_Str[i].distractor_Direction_Is_In_Aiplane_Direction;
            //location
            Defualt_Airplane_Location defualt_Airplane_Location = block_Str[i].defualt_Airplane_Location;
            Airplane_location_constant airplane_Location_Constant = block_Str[i].airplane_location_Constant;
            //shape
            Defualt_Airplane_Shape defualt_Airplane_Shape = block_Str[i].defualt_Airplane_Shape;
            Default_Distractor_shape default_Distractor_Shape = block_Str[i].default_Distractor_Shape;
            Airplane_Shape_constant airplane_Shape_Constant = block_Str[i].airplane_shape_Constant;
            Distractor_shape_constant distractor_Shape_Constant = block_Str[i].distractor_Shape_Constant;
            //color
            Defualt_Airplane_Color defualt_Airplane_Color = block_Str[i].defualt_Airplane_Color;
            Default_Distractor_color default_Distractor_Color = block_Str[i].default_Distractor_Color;
            Airplane_Color_constant airplane_Color_Constant = block_Str[i].airplane_Color_Constant;
            Distracor_color_constant distracor_Color_Constant = block_Str[i].distracor_Color_Constant;






            for (int j = 0; j < block_Str[i].go; j++)
            {
                // fixed time + show time
                initializer.block_str[i].block_trial_go[j] = new Initializer.Trial_go_str();
                initializer.block_str[i].block_trial_go[j].fixation_time = fixed_time;
                initializer.block_str[i].block_trial_go[j].show_time_duration_go = Random.Range(block_Str[i].show_time_duration_go.x, block_Str[i].show_time_duration_go.y)/1000f;





                // init the possible directions......................................................
                int rand;
                ShowTime.DirectionOfAirplane choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                ShowTime.DirectionOfDistractors choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                if (distractor_Direction_Is_In_Aiplane_Direction == Distractor_Direction_is_in_Aiplane_Direction.yes)
                {

                    switch (default_Airplane_Direction)
                    {

                        case Default_Airplane_Direction.alldirection:
                            rand = Random.Range(0, 4);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            if (rand == 3)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            break;
                        case Default_Airplane_Direction.not_left:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.not_right:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                        case Default_Airplane_Direction.not_up:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            break;
                        case Default_Airplane_Direction.not_down:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.left_right:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.up_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                        case Default_Airplane_Direction.left_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                        case Default_Airplane_Direction.left_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            break;
                        case Default_Airplane_Direction.right_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.right_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                    }
                }
                if (distractor_Direction_Is_In_Aiplane_Direction == Distractor_Direction_is_in_Aiplane_Direction.no)
                {

                    switch (default_Airplane_Direction)
                    {

                        case Default_Airplane_Direction.alldirection:
                            rand = Random.Range(0, 4);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 3)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.not_left:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.not_right:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.not_up:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.not_down:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.left_right:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.up_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.left_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.left_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                            }
                            break;
                        case Default_Airplane_Direction.right_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }

                            }
                            break;
                        case Default_Airplane_Direction.right_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                int rand_dist = Random.Range(0, 3);
                                if (rand == 0)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                                }
                                if (rand == 1)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                                }
                                if (rand == 2)
                                {
                                    choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                                }
                            }
                            break;

                    }



                }
                initializer.block_str[i].block_trial_go[j].showTimeStruct.direction_of_airplane = choosen_direction_of_airplane;
                initializer.block_str[i].block_trial_go[j].showTimeStruct.directionOfDistractors = choosen_direction_of_distractor;


                // init the location...............................................................
                ShowTime.LocationOfAairplane choosen_location_of_airplane = ShowTime.LocationOfAairplane.center;
                switch (airplane_Location_Constant)
                {
                    case Airplane_location_constant.constant:
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.center)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.center;
                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.up)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.up;
                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.down)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.down;

                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.left)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.left;

                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.right)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.right;
                        }
                        break;
                    case Airplane_location_constant.variable:
                        rand = Random.Range(0, 5);
                        if (rand == 0)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.center;
                        }
                        if (rand == 1)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.right;
                        }
                        if (rand == 2)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.left;
                        }
                        if (rand == 3)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.up;
                        }
                        if (rand == 4)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.down;
                        }

                        break;

                }
                initializer.block_str[i].block_trial_go[j].showTimeStruct.location_of_airplane = choosen_location_of_airplane;


                // init the sahpe ....................................................................
                ShowTime.ShapeOfAirPlane choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type1;
                ShowTime.ShapeOfDistractors choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type1;
                rand = 0;
                switch (airplane_Shape_Constant)
                {
                    case Airplane_Shape_constant.constant:

                        if (block_Str[i].defualt_Airplane_Shape == Defualt_Airplane_Shape.type_1)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type1;
                        }
                        if (block_Str[i].defualt_Airplane_Shape == Defualt_Airplane_Shape.type_2)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type2;

                        }
                        if (block_Str[i].defualt_Airplane_Shape == Defualt_Airplane_Shape.type_3)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type3;

                        }

                        break;
                    case Airplane_Shape_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type1;
                        }
                        if (rand == 1)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type2;
                        }
                        if (rand == 2)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_go[j].showTimeStruct.shape_of_airplanes = choosen_shape_of_airpane;

                rand = 0;
                switch (distractor_Shape_Constant)
                {
                    case Distractor_shape_constant.constant:

                        if (block_Str[i].default_Distractor_Shape == Default_Distractor_shape.type_1)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type1;
                        }
                        if (block_Str[i].default_Distractor_Shape == Default_Distractor_shape.type_2)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type2;

                        }
                        if (block_Str[i].default_Distractor_Shape == Default_Distractor_shape.type_3)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.typ3;

                        }

                        break;
                    case Distractor_shape_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type1;
                        }
                        if (rand == 1)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type2;
                        }
                        if (rand == 2)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.typ3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_go[j].showTimeStruct.shapeOfDistractors = choosen_shape_of_distractor;


                // init the color ....................................................................
                ShowTime.ColorOfAirPlane choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_1;
                ShowTime.ColorOfDistractors choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_1;

                rand = 0;
                switch (airplane_Color_Constant)
                {
                    case Airplane_Color_constant.constant:

                        if (block_Str[i].defualt_Airplane_Color == Defualt_Airplane_Color.color_1)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_1;
                        }
                        if (block_Str[i].defualt_Airplane_Color == Defualt_Airplane_Color.color_2)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_2;

                        }
                        if (block_Str[i].defualt_Airplane_Color == Defualt_Airplane_Color.color_3)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_3;

                        }

                        break;
                    case Airplane_Color_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_1;
                        }
                        if (rand == 1)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_2;
                        }
                        if (rand == 2)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_go[j].showTimeStruct.colorOfAirPlane = choosen_color_of_airpane;

                rand = 0;
                switch (distracor_Color_Constant)
                {
                    case Distracor_color_constant.constant:

                        if (block_Str[i].default_Distractor_Color == Default_Distractor_color.color_1)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_1;
                        }
                        if (block_Str[i].default_Distractor_Color == Default_Distractor_color.color_2)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_2;

                        }
                        if (block_Str[i].default_Distractor_Color == Default_Distractor_color.color_3)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_3;

                        }

                        break;
                    case Distracor_color_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_1;
                        }
                        if (rand == 1)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_2;
                        }
                        if (rand == 2)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_go[j].showTimeStruct.colorOfDistractors = choosen_color_of_distractor;


            }








            for (int k = 0; k < block_Str[i].nogo; k++)
            {
                // fixed time + show time + stop signal + beep
                initializer.block_str[i].block_trial_nogo[k] = new Initializer.Trial_nogo_str();
                initializer.block_str[i].block_trial_nogo[k].fixation_time = fixed_time;
                initializer.block_str[i].block_trial_nogo[k].show_time_duration_no_go = Random.Range(block_Str[i].show_time_duration_no_go.x, block_Str[i].show_time_duration_no_go.y) / 1000f;
                initializer.block_str[i].block_trial_nogo[k].stop_signal_delay = Random.Range(block_Str[i].stop_signal_delay.x, block_Str[i].stop_signal_delay.y) / 1000f;
                initializer.block_str[i].block_trial_nogo[k].beep_duration = Random.Range(block_Str[i].beep_duration.x, block_Str[i].beep_duration.y) / 1000f;

                // check the range 
                if (true)
                {

                }

                // init the possible directions......................................................
                int rand;
                ShowTime.DirectionOfAirplane choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                ShowTime.DirectionOfDistractors choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                if (distractor_Direction_Is_In_Aiplane_Direction == Distractor_Direction_is_in_Aiplane_Direction.yes)
                {

                    switch (default_Airplane_Direction)
                    {

                        case Default_Airplane_Direction.alldirection:
                            rand = Random.Range(0, 4);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            if (rand == 3)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            break;
                        case Default_Airplane_Direction.not_left:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.not_right:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                        case Default_Airplane_Direction.not_up:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            break;
                        case Default_Airplane_Direction.not_down:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.left_right:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.up_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                        case Default_Airplane_Direction.left_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                        case Default_Airplane_Direction.left_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;
                            }
                            break;
                        case Default_Airplane_Direction.right_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.right_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                            }
                            break;
                    }
                }
                if (distractor_Direction_Is_In_Aiplane_Direction == Distractor_Direction_is_in_Aiplane_Direction.no)
                {

                    switch (default_Airplane_Direction)
                    {

                        case Default_Airplane_Direction.alldirection:
                            rand = Random.Range(0, 4);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                            }
                            if (rand == 3)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            break;
                        case Default_Airplane_Direction.not_left:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                            }
                            break;
                        case Default_Airplane_Direction.not_right:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            break;
                        case Default_Airplane_Direction.not_up:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            break;
                        case Default_Airplane_Direction.not_down:
                            rand = Random.Range(0, 3);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            if (rand == 2)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                            }
                            break;
                        case Default_Airplane_Direction.left_right:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                            }
                            break;
                        case Default_Airplane_Direction.up_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            break;
                        case Default_Airplane_Direction.left_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            break;
                        case Default_Airplane_Direction.left_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.left;
                            }
                            break;
                        case Default_Airplane_Direction.right_down:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.down;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                                choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;
                            }
                            break;
                        case Default_Airplane_Direction.right_up:
                            rand = Random.Range(0, 2);
                            if (rand == 0)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.right;
                            }
                            if (rand == 1)
                            {
                                choosen_direction_of_airplane = ShowTime.DirectionOfAirplane.up;
                            }
                            break;

                    }


                    rand = 0;
                    if (rand == 0)
                    {
                        choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.up;
                    }
                    if (rand == 1)
                    {
                        choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.down;

                    }
                    if (rand == 2)
                    {
                        choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.left;

                    }
                    if (rand == 3)
                    {
                        choosen_direction_of_distractor = ShowTime.DirectionOfDistractors.right;

                    }
                }
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.direction_of_airplane = choosen_direction_of_airplane;
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.directionOfDistractors = choosen_direction_of_distractor;


                // init the location...............................................................
                ShowTime.LocationOfAairplane choosen_location_of_airplane = ShowTime.LocationOfAairplane.center;
                switch (airplane_Location_Constant)
                {
                    case Airplane_location_constant.constant:
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.center)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.center;
                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.up)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.up;
                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.down)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.down;

                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.left)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.left;

                        }
                        if (block_Str[i].defualt_Airplane_Location == Defualt_Airplane_Location.right)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.right;
                        }
                        break;
                    case Airplane_location_constant.variable:
                        rand = Random.Range(0, 5);
                        if (rand == 0)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.center;
                        }
                        if (rand == 1)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.right;
                        }
                        if (rand == 2)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.left;
                        }
                        if (rand == 3)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.up;
                        }
                        if (rand == 4)
                        {
                            choosen_location_of_airplane = ShowTime.LocationOfAairplane.down;
                        }

                        break;

                }
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.location_of_airplane = choosen_location_of_airplane;


                // init the sahpe ....................................................................
                ShowTime.ShapeOfAirPlane choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type1;
                ShowTime.ShapeOfDistractors choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type1;
                rand = 0;
                switch (airplane_Shape_Constant)
                {
                    case Airplane_Shape_constant.constant:

                        if (block_Str[i].defualt_Airplane_Shape == Defualt_Airplane_Shape.type_1)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type1;
                        }
                        if (block_Str[i].defualt_Airplane_Shape == Defualt_Airplane_Shape.type_2)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type2;

                        }
                        if (block_Str[i].defualt_Airplane_Shape == Defualt_Airplane_Shape.type_3)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type3;

                        }

                        break;
                    case Airplane_Shape_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type1;
                        }
                        if (rand == 1)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type2;
                        }
                        if (rand == 2)
                        {
                            choosen_shape_of_airpane = ShowTime.ShapeOfAirPlane.type3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.shape_of_airplanes = choosen_shape_of_airpane;

                rand = 0;
                switch (distractor_Shape_Constant)
                {
                    case Distractor_shape_constant.constant:

                        if (block_Str[i].default_Distractor_Shape == Default_Distractor_shape.type_1)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type1;
                        }
                        if (block_Str[i].default_Distractor_Shape == Default_Distractor_shape.type_2)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type2;

                        }
                        if (block_Str[i].default_Distractor_Shape == Default_Distractor_shape.type_3)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.typ3;

                        }

                        break;
                    case Distractor_shape_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type1;
                        }
                        if (rand == 1)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.type2;
                        }
                        if (rand == 2)
                        {
                            choosen_shape_of_distractor = ShowTime.ShapeOfDistractors.typ3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.shapeOfDistractors = choosen_shape_of_distractor;


                // init the color ....................................................................
                ShowTime.ColorOfAirPlane choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_1;
                ShowTime.ColorOfDistractors choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_1;

                rand = 0;
                switch (airplane_Color_Constant)
                {
                    case Airplane_Color_constant.constant:

                        if (block_Str[i].defualt_Airplane_Color == Defualt_Airplane_Color.color_1)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_1;
                        }
                        if (block_Str[i].defualt_Airplane_Color == Defualt_Airplane_Color.color_2)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_2;

                        }
                        if (block_Str[i].defualt_Airplane_Color == Defualt_Airplane_Color.color_3)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_3;

                        }

                        break;
                    case Airplane_Color_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_1;
                        }
                        if (rand == 1)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_2;
                        }
                        if (rand == 2)
                        {
                            choosen_color_of_airpane = ShowTime.ColorOfAirPlane.color_3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.colorOfAirPlane = choosen_color_of_airpane;

                rand = 0;
                switch (distracor_Color_Constant)
                {
                    case Distracor_color_constant.constant:

                        if (block_Str[i].default_Distractor_Color == Default_Distractor_color.color_1)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_1;
                        }
                        if (block_Str[i].default_Distractor_Color == Default_Distractor_color.color_2)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_2;

                        }
                        if (block_Str[i].default_Distractor_Color == Default_Distractor_color.color_3)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_3;

                        }

                        break;
                    case Distracor_color_constant.variable:
                        rand = Random.Range(0, 3);
                        if (rand == 0)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_1;
                        }
                        if (rand == 1)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_2;
                        }
                        if (rand == 2)
                        {
                            choosen_color_of_distractor = ShowTime.ColorOfDistractors.color_3;
                        }


                        break;

                }
                initializer.block_str[i].block_trial_nogo[k].showTimeStruct.colorOfDistractors = choosen_color_of_distractor;

            }
        }

    }


    public bool check_before_initial_block()
    {
        if (true)
        {

        }
        return true;
    }


}




