using System.Collections.Generic;
using Tetris.Models.Contract;

namespace Tetris.Models
{
    public class Stick : IShape
    {
        public List<int[,]> Position => new List<int[,]>()
        {
            new int[,]
            {
            { 1 },
            { 1 },
            { 1 },
            { 1 }
            },
            new int[,]
            {
            { 1, 1, 1, 1 },
            }
        };
    }
}
