﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Snake;


/// <summary>
/// a class that handels Map of game
/// a 2d arrays of cells
/// </summary>
public class GameMap
{
    
    private List <SnakeObj> SnakeObjs { get; }

    public List<List<Cell>> _Map { get; }

    MapSize MapSize { get; }

    public int AppelCount { get; set; }

    public GameMap(List<List<Cell>> map, List<SnakeObj> snakeObjs)
    {
        SnakeObjs = snakeObjs;
        _Map = map;
        foreach (var row in map)
        {
            foreach (var cell in row)
            {
                foreach (var snakeobj in snakeObjs)
                {
                    cell.SnakesValues.Add(new SnakeValues(snakeobj,0));
                }
            }
        }

        InitalSnakes(snakeObjs);

    }


    /// <summary>
    /// this contructor will create map for given size
    /// you doesnt need to provide snake object
    /// </summary>
    /// <param name="snakeObjs"></param>
    /// <param name="mapSize">size will exclist here</param>
    public GameMap(MapSize mapSize ,List<SnakeObj>? snakeObjs=null )
    {
        snakeObjs ??= [];

        SnakeObjs = snakeObjs;
        MapSize = mapSize;
        _Map = new List<List<Cell>> { };

        for (int i = 0; i < mapSize.coulmn; i++)
        {
            _Map.Add(new List<Cell>());
        }

        for (int i = 0; i < mapSize.coulmn; i++)
        {
            for (int j = 0; j < mapSize.row; j++)
            {
                var cell = new Cell(snakeObjs);
                _Map[i].Add(cell);
            }
        }

    }
    /// <summary>
    /// will place snakes on the map
    /// </summary>
    public void InitalSnakes(List<SnakeObj> snakeObjs)
    {
        foreach (var snake in snakeObjs)
        {
            var x = snake.HeadX;
            var y = snake.HeadY;

            var listOfSnakeValu = _Map[x][y].SnakesValues;
            for (int i = 0; i < listOfSnakeValu.Count; i++)
            {
                if (listOfSnakeValu[i]._SnakeObj == snake)
                {
                    listOfSnakeValu[i]._SnakeValue = 1;
                }
            }
        }
    }



}
/// <summary>
/// A list of Snakes that will exist in this cell
/// with thier value
/// </summary>
public class Cell
{
    public bool IsWall = false;
    public List<SnakeValues> SnakesValues;

    /// <summary>
    /// true if this cell have appel on it
    /// </summary>
    public bool HaveAppel { get; set; }


    public Cell() 
    {
        SnakesValues = new List<SnakeValues>();
        HaveAppel = false;
    }
    public Cell(List<SnakeObj> snakeObjs)
    {
        SnakesValues = new() { };
        foreach (var snake in snakeObjs)
        {
            SnakesValues.Add(new SnakeValues (snake,  0 ));
        }
        HaveAppel = false;
    }

    public SnakeValues? GetSnakeValues(SnakeObj snakeRef )
    {
        foreach (var snakeValue in SnakesValues)
        {
            if (snakeValue._SnakeObj == snakeRef)
            {
                return snakeValue;
            }
        }
        return null;
    }
}

/// <summary>
/// snake object itself and its value in that cell
/// </summary>
public class SnakeValues
{
    public SnakeValues(SnakeObj? snakeObj,int? snakeValue)
    {
        _SnakeValue = snakeValue?? 0;
        _SnakeObj = snakeObj;
    }
    public SnakeObj _SnakeObj { get; set; }
    public int _SnakeValue { get; set; }
}