using System.Collections.Generic;

namespace Tetris.Models.Contract
{
    public interface IShape
    {
        public List<int[,]> Position { get; }
    }
}
