using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWars
{
    class Asteroid : GameObject
    {
        /// <summary> Картинка которая будет отрисовываться на игровой сцене </summary>
        protected Image Image = Image.FromFile("meteor.png");

        /// <summary>Инициализация нового игрового объекта</summary>
        /// <param name="Position">ПОложение на игровой сцене</param>
        /// <param name="Speed">Скорость перемещения между кадрами</param>
        /// <param name="Size">Размер на игровой сцене</param>
        public Asteroid(Point Position, Point Speed, Size Size)
          : base(Position, Speed, Size) // Передача параметров в конструктор предка
        {
            // Конструктор звезды ничего больше не делает
        }


        /// <summary>Метод отрисовки графики объекта на игровой сцене</summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, new Rectangle(_Position, _Size));
        }

        /// <summary>Метод обновления состояния объекта при смене кадров</summary>
        public override void Update()
        {
            _Position.X += _Speed.X;  // Перемещаем объект на сцене в соответствии с вектором скорости
            _Position.Y += _Speed.Y;

            // Проверяем граничные условия выхдода объекта за пределы сцены (меняем знак соответствующей составляющей вектора скорости)
            if (_Position.X < 0 || _Position.X > Game.Width - _Size.Width)
                _Speed.X *= -1;
            if (_Position.Y < 0 || _Position.Y > Game.Height - _Size.Height)
                _Speed.Y *= -1;
        }

        
    }
}
