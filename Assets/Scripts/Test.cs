using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JGM.Game
{
    public class Test : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 80, 80), "Play"))
            {

            }

            GUI.Button(new Rect(100, 0, 80, 80), "X");
            GUI.Button(new Rect(200, 0, 80, 80), "O");
            GUI.Button(new Rect(300, 0, 80, 80), "Empty");

            // Make a group on the center of the screen
            GUI.BeginGroup(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100));
            // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

            // We'll make a box so you can see where the group is on-screen.
            GUI.Box(new Rect(0, 0, 100, 100), "Tic Tac Toe");
            GUI.Button(new Rect(10, 40, 80, 30), "Play");

            // End the group we started above. This is very important to remember!
            GUI.EndGroup();
        }
    }
}
