﻿using System.Collections.Generic;
using Tetris.Models.Contract;

namespace Tetris.Models
{
    public class LLeft : IShape
    {
        public List<int[,]> Position => new List<int[,]>()
        {
            new int[,]
            {
            { 1, 0 },
            { 1, 0 },
            { 1, 1 }
            },
            new int[,]
            {
            { 0, 0, 1 },
            { 1, 1, 1 },
            },
            new int[,]
            {
            { 1, 1 },
            { 0, 1 },
            { 0, 1 }
            },
            new int[,]
            {
            { 1, 1, 1 },
            { 1, 0, 0 },
            }
        };
    }
}
