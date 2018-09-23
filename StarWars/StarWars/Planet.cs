using System.Drawing;

namespace StarWars
{
    class Planet : GameObject
    {
        private Image GetImage = Image.FromFile("planet.png");

        /// <summary>Инициализация новой планеты</summary>
        /// <param name="Position">ПОложение на игровой сцене</param>
        /// <param name="Speed">Скорость перемещения между кадрами</param>
        /// <param name="Size">Размер на игровой сцене</param>
        public Planet(Point Position, Point Speed, Size Size)
            : base(Position, Speed, Size) // Передача параметров в конструктор предка
        {
            // Конструктор звезды ничего больше не делает
        }


        public override void Draw()
        {
            var g = Game.Buffer.Graphics;
            g.DrawImage(GetImage, new Rectangle(_Position, _Size));
        }

        /// <summary>Переопределяем метод обновления состояния</summary>
        public override void Update()
        {
            _Position.X -= _Speed.X;
            if (_Position.X < 0)
                _Position.X = Game.Width + _Size.Width;
        }
    }
}
