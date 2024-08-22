using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTime
{

    public enum DirectionOfAirplane
    {
        right,
        left,
        up,

        down,

    }




    public enum LocationOfAairplane
    {
        center,
        right,
        left,
        up,
        down,
    }

    public enum ShapeOfAirPlane
    {

        type1,
        type2,
        type3,
    }


    public enum ColorOfAirPlane
    {
        color_1,
        color_2,

        color_3,
    }

    public enum ShapeOfDistractors
    {
        type1,
        type2,
        typ3,
    }


    public enum DirectionOfDistractors
    {
        right,
        left,
        up,

        down,
    }


    public enum ColorOfDistractors
    {
        color_1,
        color_2,
        color_3,
    }

    public DirectionOfAirplane direction_of_airplane;

    public LocationOfAairplane location_of_airplane;
    public ShapeOfAirPlane shape_of_airplanes;

    public ColorOfAirPlane colorOfAirPlane;




    public DirectionOfDistractors directionOfDistractors;

    public ShapeOfDistractors shapeOfDistractors;

    public ColorOfDistractors colorOfDistractors;



    public enum Actions
    {
        up,
        down,
        right,
        left,
    }
    public Actions correct_action;


}
