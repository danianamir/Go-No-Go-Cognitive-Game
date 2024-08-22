using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Block
{



    public Trail[] trails;









    public void init_all_trials(Initializer.Trial_go_str[] trial_go_str, Initializer.Trial_nogo_str[] trial_nogo_str)
    {


        // List<int>go_or_nogo_indicator = new List<int>();
        int go = trial_go_str.Length;
        int nogo = trial_nogo_str.Length;
        int lenth = go + nogo;
        trails = new Trail[lenth];

    

        int go_remain = go;
        int nogo_remain = nogo;
        int i = 0;
        int go_index = 0;
        int no_go_index = 0;




        while (go_remain > 0 || nogo_remain > 0)
        {
            int rand = Random.Range(0, 2);



            if (rand == 0 && go_remain > 0)
            {



                trails[i] = new Trail();

                trails[i].number_in_block = i;
                trial_go_str[go_index].number_in_block = i;
                trails[i].fixation_time = trial_go_str[go_index].fixation_time;
                trails[i].show_time_duration_go = trial_go_str[go_index].show_time_duration_go;

                trails[i].is_go = true;

                trails[i].init_showtime(trial_go_str[go_index].showTimeStruct);


                i++;
                go_index++;
                go_remain = go_remain - 1;
            }
            if (rand == 1 && nogo_remain > 0)
            {


                trails[i] = new Trail();

                trails[i].number_in_block = i;
                trial_nogo_str[no_go_index].number_in_block = i;
                trails[i].fixation_time = trial_nogo_str[no_go_index].fixation_time;
                trails[i].show_time_duration_no_go = trial_nogo_str[no_go_index].show_time_duration_no_go;

                trails[i].stop_signal_delay = trial_nogo_str[no_go_index].stop_signal_delay;
                trails[i].beep_duration = trial_nogo_str[no_go_index].beep_duration;

                trails[i].is_go = false;

                trails[i].init_showtime(trial_nogo_str[no_go_index].showTimeStruct);


                i++;
                no_go_index++;
                nogo_remain = nogo_remain - 1;

            }



        }







    }











}
