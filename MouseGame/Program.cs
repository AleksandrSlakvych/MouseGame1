using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseGame
{
    public interface IUiComponent
    {
        void Draw(ConsoleGraphics g);
    }

    public class EventButtonArgs : EventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Button : IUiComponent
    {
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
                OnLeftButtonClickOn?.Invoke(this.x, this.y);
                OnLeftButtonClickMessage(this.x, this.y);
                //OnLeftButtonClick?.Invoke(this, new EventButtonArgs { X = this.x, Y = this.y });
            }
            g.FillRectangle(0xff00ff00, x, y, sizeW, sizeH);
        }

        //public event EventHandler<EventButtonArgs> OnLeftButtonClick;

        public event Func<int,int, int> OnLeftButtonClickOn;

        public event Action<int, int> OnLeftButtonClickMessage;
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

            Func<int, int, int> func = (x, y) => { x = Input.MouseX; y = Input.MouseY; return x;  };

            Action<int, int> action = (x, y) => { x = Input.MouseX; y = Input.MouseY;  };

            firstbutton.OnLeftButtonClickMessage += action;
            firstbutton.OnLeftButtonClickOn += func;

            //firstbutton.OnLeftButtonClick += Firstbutton_OnLeftButtonClick;
            while (true)
            {
                foreach (var component in components)
                {
                    component.Draw(g);
                }



                g.FlipPages();
            }

        }


        private static void Firstbutton_OnLeftButtonClick(object sender, EventButtonArgs eventButton)
        {
            eventButton.X += 10;
            eventButton.Y += 10;
        }
    }

}
