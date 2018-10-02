using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace StarWars
{
    //Шаймухаметов Денис
    //Выполнил задание 1 и 2

    /// <summary>Класс игровой логики</summary>
    internal static class Game
    {
        /// <summary>Конекст буфера отрисовки графики</summary>
        private static BufferedGraphicsContext __Context;

        /// <summary>Таймер обновления игрового интерфейса</summary>
        private static readonly Timer __Timer = new Timer { Interval = 100 };

        /// <summary>Массив графических игровых объекотв</summary>
        private static GameObject[] __GameObjects;
        private static Asteroid[] __Asteroids;
        private static Bullet __Bullet;
        private static Ship __Ship;

        private static int s_width;
        private static int s_height;

        /// <summary>Буфер, в который будем проводить отрисовку графики очередного кадра</summary>
        public static BufferedGraphics Buffer { get; private set; }

        /// <summary>Ширина игрового поля</summary>
        public static int Width
        {
            get
            {
                return s_width;
            }
            private set
            {
                if (value > 1000 || value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    s_width = value;
                }
            }
        }
        /// <summary>Высота игрового поля</summary>
        public static int Height
        {
            get
            {
                return s_height;
            }
            private set
            {
                if (value > 1000 || value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    s_height = value;
                }

            }
        }
        /// <summary>Загрузка данных игровой логики</summary>
        public static void Load()
        {
            __GameObjects = new GameObject[30];

            for (var i = 0; i < __GameObjects.Length; i++)
                __GameObjects[i] = new Star(
                    new Point(600, i * 20),
                    new Point(i, 0),
                    new Size(5, 5));

            const int asteroids_count = 10;
            __Asteroids = new Asteroid[asteroids_count];

            var rnd = new Random();
            for (var i = 0; i < asteroids_count; i++)
            {
                var speed = rnd.Next(3, 50);
                __Asteroids[i] = new Asteroid(
                    new Point(100, rnd.Next(0, Height)),
                    new Point(-speed, speed),
                    new Size(speed, speed));
            }

            __Bullet = new Bullet(new Point(0, 200), new Size(4, 1));
            __Ship = new Ship(400);
            __Ship.ShipDie += OnShipDie;

        }

        private static void OnShipDie(object sender, EventArgs e)
        {
            __Ship = null;
            __Timer.Enabled = false;
            var g = Buffer.Graphics;
            g.Clear(Color.DarkBlue);
            g.DrawString(
                "Game over!",
                new Font(
                    FontFamily.GenericSansSerif,
                    60,
                    FontStyle.Bold | FontStyle.Underline),
                Brushes.White,
                200, 100);

            Buffer.Render();
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

            MessageLog.LoadDelegate();
        }


        private static void OnGameFormKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    __Bullet = new Bullet(__Ship.Rect.Location, new Size(4, 1));
                    break;
                case Keys.Up:
                    __Ship.Up();
                    break;
                case Keys.Down:
                    __Ship.Down();
                    break;
#if DEBUG
                case Keys.W:
                    __Ship.Die();
                    break;
#endif
            }
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
            var g = Buffer.Graphics;
            g.Clear(Color.Black);

            foreach (var game_object in __GameObjects)
                game_object.Draw();

            for (var i = 0; i < __Asteroids.Length; i++)
                __Asteroids[i]?.Draw();

            __Bullet?.Draw();

            __Ship?.Draw();

            Buffer.Render();
        }

        /// <summary>Метод обновления состояния игры между кадрами</summary>
        private static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object.Update();

            __Bullet?.Update();

            for (var i = 0; i < __Asteroids.Length; i++)
            {
                var asteroid = __Asteroids[i];
                if (asteroid == null) continue;
                asteroid.Update();
                if (__Bullet != null && asteroid.Collision(__Bullet))
                {
                    __Asteroids[i] = null;
                    __Bullet = null;
                    System.Media.SystemSounds.Hand.Play();
                    MessageLog.processor.ProcessMessage("Попадание в астероид");
                    continue;
                }

                if (__Ship != null && asteroid.Collision(__Ship))
                {
                    var rnd = new Random();
                    __Ship.Energy -= rnd.Next(1, 10);
                    MessageLog.processor.ProcessMessage("Астероид столкнулся с кораблем");
                    if (__Ship.Energy <= 0)
                    {
                        MessageLog.processor.ProcessMessage("Корабль уничтожен");
                        __Ship.Die();
                    }
                }
            }
            //foreach (var asteroid in __Asteroids)
            //{
            //    asteroid.Update();
            //    if (asteroid.Collision(__Bullet))
            //    {
            //        System.Media.SystemSounds.Hand.Play();
            //        Point pBullet = new Point(0, 200);
            //        GameObject.ReloadPosition(__Bullet, pBullet);
            //        Point pAsteroid = new Point(0, 60);
            //        GameObject.ReloadPosition(asteroid, pAsteroid);

            //    }
            //}
        }

       
    }
}
