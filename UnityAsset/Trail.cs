using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail
{
    public int number_in_block;
    public float fixation_time;
    public float show_time_duration_go;
    public float show_time_duration_no_go;
    public float stop_signal_delay;
    public float beep_duration;

    public ShowTime showtime;


    public bool is_go;


    [HideInInspector]
    public bool is_in_fixed_time = true;
    [HideInInspector]
    public bool is_in_show_time = false;

    [HideInInspector]
    public bool show_time_proccesd = false;

    [HideInInspector]
    public bool is_in_stop_signal_delay = false;
    [HideInInspector]
    public bool is_in_beep_duration = false;
    [HideInInspector]
    public bool beep_duration_proccesed = false;
    [HideInInspector]
    public bool is_end_of_trial = false;




    public void init_showtime(Initializer.ShowTime_str sh)
    {
        showtime = new ShowTime();
        showtime.direction_of_airplane = sh.direction_of_airplane;
        showtime.shape_of_airplanes = sh.shape_of_airplanes;
        showtime.location_of_airplane = sh.location_of_airplane;
        showtime.colorOfAirPlane = sh.colorOfAirPlane;

        showtime.colorOfDistractors = sh.colorOfDistractors;
        showtime.shapeOfDistractors = sh.shapeOfDistractors;
        showtime.directionOfDistractors = sh.directionOfDistractors;

    }

}
