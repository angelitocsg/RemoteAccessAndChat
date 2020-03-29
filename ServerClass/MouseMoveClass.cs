using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Server
{
    public class MouseMoveClass
    {
        public MouseMoveClass(int iX, int iY)
        {
            Cursor.Position = new Point(iX, iY);
        }
    }
}

/*
        // Declare.
[DllImport("user32.dll")]
static extern bool SetCursorPos(int X, int Y);
 
[DllImport("user32.dll")]
static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
 
[Flags]
public enum MouseEventTFlags
{
    LEFTDOWN = 0x00000002,
    LEFTUP = 0x00000004,
    MIDDLEDOWN = 0x00000020,
    MIDDLEUP = 0x00000040,
    MOVE = 0x00000001,
    ABSOLUTE = 0x00008000,
    RIGHTDOWN = 0x00000008,
    RIGHTUP = 0x00000010
}
 

Para clicar numa determinada posição:

 

// Ajusta a posição do cursor.
SetCursorPos(250, 200);
// Simula um clique com o botão esquerdo.
mouse_event((uint)MouseEventTFlags.LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
mouse_event((uint)MouseEventTFlags.LEFTUP, 0, 0, 0, UIntPtr.Zero);
 * */