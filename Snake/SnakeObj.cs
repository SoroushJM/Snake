﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake;

public class SnakeObj
{
    public SnakeObj(int x, int y, Directions direction)
    {
        HeadX = x;
        HeadY = y;
        Direction = direction;
    }

    public SnakeObj(int x, int y) : this(x, y, Directions.right) { }

    public int HeadX;
    public int HeadY;
    public Directions Direction { get; set; }

    public enum Directions
    {
        up, down, left, right
    }
}