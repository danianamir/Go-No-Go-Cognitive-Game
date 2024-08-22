using Google.Protobuf.WellKnownTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Initializer;



[System.Serializable]
public class Output : MonoBehaviour
{

    [System.Serializable]
    public struct Out_Block
    {
        public int type;
        public int number_in_block;
        public float fixation_time;
        public float show_time_duration_go;
        public float show_time_duration_no_go;
        public float stop_signal_delay;
        public float beep_duration;

        public ShowTime_str showTimeStruct;
    }

    [System.Serializable]
    public struct Observation
    {
        public List<int> win_loss;
        public List<float> click_exact_time;
     
        public List<Out_Block> out_block;
    }


    public List<Observation> observations = new List<Observation>();

    public List<InitializerPerBlock.Block_str> actions = new List<InitializerPerBlock.Block_str>();









}
