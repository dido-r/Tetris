using System.Collections.Generic;
using Tetris.Models.Contract;

namespace Tetris.Models
{
    public class ZRight : IShape
    {
        public List<int[,]> Position => new List<int[,]>()
        {
            new int[,]
            {
            { 0, 1, 1 },
            { 1, 1, 0 },
            },
            new int[,]
            {
            { 1, 0 },
            { 1, 1 },
            { 0, 1 }
            }
        };
    }
}
