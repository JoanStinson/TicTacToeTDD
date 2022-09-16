using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JGM.Game
{
    public interface IBoard
    {
        void PlacePlayerToken(int playerId, int position);
        void Clear();
    }
}
