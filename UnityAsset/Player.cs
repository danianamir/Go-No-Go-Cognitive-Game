using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    public int win = 0;
    public int loss = 0;

    public bool isgotype;
    public bool isinfixed;
    public bool isinshow;
    public bool isinend;
    public int block_index;

    public GameManger game_manger;
    public ShowTime.Actions correct_action;

    public TMP_Text win_show;
    public TMP_Text loss_show;

    public event Action<bool> on_player_coorect_respond;

    private bool x;


    public List<int> win_loss = new List<int>();
    public List<float> time_of_click = new List<float>();
    void Start()
    {

        game_manger.isgotype_isinfixed_isinshow_isinend += set_player_responsetime;
        game_manger.send_correct_action += set_player_correct_action;

    }


    void set_player_responsetime(bool isgotype, bool isinfixed, bool isinshow, bool isinend)
    {
        this.isgotype = isgotype;
        this.isinfixed = isinfixed;
        this.isinshow = isinshow;
        this.isinend = isinend;

    }

    void set_player_correct_action(ShowTime.Actions correct_action)
    {
        this.correct_action = correct_action;
    }

    void Update()
    {


        if (game_manger.end_of_game)
        {

        }
        else
        {




            if (isgotype)
            {
                if (isinfixed)
                {
                    // nothing can be done 
                }
                if (isinshow)
                {

                    if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        if (Input.GetKeyUp(KeyCode.UpArrow) && correct_action == ShowTime.Actions.up
                                           || Input.GetKeyUp(KeyCode.DownArrow) && correct_action == ShowTime.Actions.down
                                           || Input.GetKeyUp(KeyCode.LeftArrow) && correct_action == ShowTime.Actions.left
                                           || Input.GetKeyUp(KeyCode.RightArrow) && correct_action == ShowTime.Actions.right)
                        {
                            time_of_click.Add(game_manger.elapse_time_of_trial);
                            win_loss.Add(1);
                            win = win + 1;

                            on_player_coorect_respond?.Invoke(true);
                        }
                        else
                        {

                            time_of_click.Add(game_manger.elapse_time_of_trial);
                            win_loss.Add(0);
                            loss = loss + 1;

                            on_player_coorect_respond?.Invoke(true);
                        }
                    }
                }
                if (isinend)
                {
                    time_of_click.Add(game_manger.elapse_time_of_trial);
                    win_loss.Add(0);
                    loss = loss + 1;
                }
            }
            else
            {
                if (isinfixed)
                {
                    // nothing can be done 
                }
                if (isinshow)
                {
                    if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
                    {
                        time_of_click.Add(game_manger.elapse_time_of_trial);
                        win_loss.Add(0);
                        loss = loss + 1;
                        on_player_coorect_respond?.Invoke(true);

                    }
                }
                if (isinend)
                {
                    time_of_click.Add(game_manger.elapse_time_of_trial);
                    win_loss.Add(1);
                    win = win + 1;
                }
            }
        }


        win_show.text = win.ToString();
        loss_show.text = loss.ToString();

    }





}




