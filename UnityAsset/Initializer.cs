using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{



    private GameManger gameManger;


    public Block_str[] block_str;





    [System.Serializable]
    public struct Block_str
    {
        public Trial_go_str[] block_trial_go;
        public Trial_nogo_str[] block_trial_nogo;

    }



    [System.Serializable]
    public struct Trial_go_str
    {

        public int number_in_block;
        public float fixation_time;
        public float show_time_duration_go;

        public ShowTime_str showTimeStruct;

    }


    [System.Serializable]
    public struct Trial_nogo_str
    {

        public int number_in_block;
        public float fixation_time;
        public float show_time_duration_no_go;
        public float stop_signal_delay;
        public float beep_duration;

        public ShowTime_str showTimeStruct;
    }



    [System.Serializable]
    public struct ShowTime_str
    {

        public ShowTime.DirectionOfAirplane direction_of_airplane;
        public ShowTime.ShapeOfAirPlane shape_of_airplanes;
        public ShowTime.LocationOfAairplane location_of_airplane;
        public ShowTime.ColorOfAirPlane colorOfAirPlane;




        public ShowTime.DirectionOfDistractors directionOfDistractors;
        public ShowTime.ShapeOfDistractors shapeOfDistractors;
        public ShowTime.ColorOfDistractors colorOfDistractors;
    }





    void Start()
    {
        gameManger = FindObjectOfType<GameManger>().GetComponent<GameManger>();

        init_blocks();
    }



    public void init_blocks()
    {
       

        gameManger.blocks = new Block[block_str.Length];
        for (int i = 0; i < block_str.Length; i++)
        {
            gameManger.blocks[i] = new Block();
            gameManger.blocks[i].init_all_trials(block_str[i].block_trial_go, block_str[i].block_trial_nogo);

        }

    }



}
