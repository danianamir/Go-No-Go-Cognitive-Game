using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using static Output;

using System.Linq;

public class Exporter : MonoBehaviour
{
    public InitializerPerBlock initialzer_per_block;
    public Output output;
    public Player player;

    public Initializer initializer;
    public Canvas canvasToshow;
    public TextMeshProUGUI textMeshPro;
    private bool end_of_game = true;

    public GameManger gameManger;
    private void Start()
    {
        canvasToshow.gameObject.SetActive(false);


    }


    void Update()
    {

        if (gameManger.end_of_game == true && end_of_game == true)
        {
            init();
            export_json(output);
            end_of_game = false;
        }

    }


    public void init()
    {
        // init actions
        output.actions = initialzer_per_block.block_Str;

        // init observation 
        int start = 0;
        for (int i = 0; i < initializer.block_str.Length; i++)
        {


            Observation observation = new Observation();


            // oublocks 
            List<Output.Out_Block> o_list = new List<Out_Block>();


            foreach (var item in initializer.block_str[i].block_trial_go)
            {
                Output.Out_Block o = new Output.Out_Block();
                o.type = 0;
                o.number_in_block = item.number_in_block;

                o.fixation_time = item.fixation_time;
                o.show_time_duration_go = item.show_time_duration_go;
                o.show_time_duration_no_go = 0;
                o.stop_signal_delay = 0;
                o.beep_duration = 0;


                o.showTimeStruct = item.showTimeStruct;

                o_list.Add(o);
            }


            foreach (var item in initializer.block_str[i].block_trial_nogo)
            {
                Output.Out_Block o = new Output.Out_Block();
                o.type = 1;
                o.number_in_block = item.number_in_block;

                o.fixation_time = item.fixation_time;
                o.show_time_duration_go = 0;
                o.show_time_duration_no_go = item.show_time_duration_no_go;
                o.stop_signal_delay = item.stop_signal_delay;
                o.beep_duration = item.beep_duration;

                o.showTimeStruct = item.showTimeStruct;

                o_list.Add(o);
            }



            List<Output.Out_Block> sorted = o_list.OrderBy(p => p.number_in_block).ToList();

            observation.out_block = new List<Output.Out_Block>();

            foreach (var item in sorted) { observation.out_block.Add(item); }









            // win loss click time
            observation.win_loss = new List<int>();
            observation.click_exact_time = new List<float>();

            for (int j = 0; j < gameManger.blocks[i].trails.Length; j++)
            {


                observation.win_loss.Insert(j, player.win_loss[start + j]);
                observation.click_exact_time.Insert(j, player.time_of_click[start + j] - initializer.block_str[i].block_trial_go[0].fixation_time);

            }

            start = start + gameManger.blocks[i].trails.Length;



            output.observations.Insert(i, observation);
        }



    }

    public void export_json(Output output)
    {

        string json = JsonUtility.ToJson(output);
        string file_name = start.id_name + ".json";
        string filePath = Path.Combine(start.out_adress, file_name);
        File.WriteAllText(filePath, json);
        canvasToshow.gameObject.SetActive(true);
        textMeshPro.text = filePath;
        Debug.Log(filePath);


    }


}
