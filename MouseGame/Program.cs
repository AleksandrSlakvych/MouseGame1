using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseGame
{
    delegate void EventHandler(Button e);

    class EventArgs
    {
        public int X { get; set; }
        public int Y  { get; set; }

        public EventArgs(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public interface IUiComponent
    {
        void Draw(ConsoleGraphics g);
    }
    
    class Button : IUiComponent
    {
        public event EventHandler ChangePosition;

        public int x, y;
        public int sizeW = 30, sizeH = 30;

        public Button(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Draw(ConsoleGraphics g)
        {
            if (Input.IsMouseLeftButtonDown)
            {
                ChangePosition?.Invoke(this) ;
            }
            g.FillRectangle(0xff00ff00, x, y, sizeW, sizeH);
        }

       

        
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleGraphics g = new ConsoleGraphics();
            List<IUiComponent> components = new List<IUiComponent>();
            var firstbutton = new Button(10, 10);
            var secondbutton = new Button(50, 50);
            components.Add(firstbutton);
            components.Add(secondbutton);
            firstbutton.ChangePosition += Firstbutton_ChangePosition;
            while (true)
            {
                foreach (var component in components)
                {
                    component.Draw(g);
                }



                g.FlipPages();
            }

        }

        static void Firstbutton_ChangePosition(Button e)
        {
            e.x = Input.MouseX;
            e.y= Input.MouseY;
        }
    }

}
