using System;
using System.Drawing;
using System.Windows.Forms;

namespace StarWars
{
    class SplashScreen
    {
        private static Button Start = new Button();

        private static Button Recornds = new Button();

        private static Button Exit = new Button();

        /// <summary>Конекст буфера отрисовки графики</summary>
        private static BufferedGraphicsContext __Context;

        /// <summary>Таймер обновления игрового интерфейса</summary>
        private static readonly Timer __Timer = new Timer { Interval = 100 };

        /// <summary>Массив графических игровых объекотв</summary>
        private static GameObject[] __GameObjects;

        /// <summary>Буфер, в который будем проводить отрисовку графики очередного кадра</summary>
        public static BufferedGraphics Buffer { get; private set; }


        /// <summary>Ширина игрового поля</summary>
        public static int Width { get; private set; }
        /// <summary>Высота игрового поля</summary>
        public static int Height { get; private set; }



        /// <summary>Загрузка данных игровой логики</summary>
        public static void Load()
        {

            //__GameObjects = new GameObject[1];
            //__GameObjects[0] = new Planet(
            //        new Point(600,  20),
            //        new Point(1, 0),
            //        new Size(100, 100));

            //Start.Text = "Старт";
            //Start.Size = new Size(60, 20);
            //Start.Location = new Point(10, 10);

            //Recornds.Text = "Рекорды";
            //Recornds.Size = new Size(60, 20);
            //Recornds.Location = new Point(10, 40);

            //Start.Text = "Выход";
            //Start.Size = new Size(60, 20);
            //Start.Location = new Point(10, 70);

        }

        /// <summary>Инициализация игровой логики</summary>
        /// <param name="form">Игровая форма</param>
        public static void Init(Form form)
        {
            Width = form.Width;
            Height = form.Height;

            __Context = BufferedGraphicsManager.Current;

            var graphics = form.CreateGraphics();
            Buffer = __Context.Allocate(graphics, new Rectangle(0, 0, Width, Height));

            __Timer.Tick += OnTimerTick;
            __Timer.Enabled = true;
        }

        /// <summary>Метод, вызываемвый таймером всякий раз при истечении указанного интервала времени</summary>
        private static void OnTimerTick(object Sender, EventArgs e)
        {
            Update();
            Draw();
        }

        /// <summary>Метод отрисовки очередного кадра игры</summary>
        public static void Draw()
        {
            var g = Buffer.Graphics; // Извлекаем графический контекст для рисования
            g.Clear(Color.Black);    // Заливаем всю поверхность одним цветом (чёрным)

            #region Пример рисования примитивов для проверки процесса создания игровой сцены
            //g.DrawRectangle(Pens.White, 100, 100, 200, 200);  // Рисуем прямоугольник
            //g.FillEllipse(Brushes.Red, 100, 100, 200, 200);   // Заливаем эллипс
            #endregion

            // Пробегаемся по всем графическим объектам и вызываем у каждого метод отрисовки
            foreach (var game_object in __GameObjects)
                game_object.Draw();

            Buffer.Render(); // Переносим содержимое буфера на экран
        }

        /// <summary>Метод обновления состояния игры между кадрами</summary>
        private static void Update()
        {
            // Пробегаемся по всем игровым объектам
            foreach (var game_object in __GameObjects)
                game_object.Update(); // И вызываем у каждого метод обновления состояния
        }
    }
}
