using System.Collections.Generic;

namespace Tetris.Models.Contract
{
    public class Cube : IShape
    {
        public List<int[,]> Position => new List<int[,]>()
        {
            new int[,]
            {
            { 1,1 },
            { 1,1 }
            }
        };
    }
}
