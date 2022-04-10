using System.Drawing;

namespace Snake {
    public static class Settings {
        /// <summary>
        /// Размер ячейки игрового поля
        /// </summary>
        public static int CellSize = 40;
        /// <summary>
        /// Ширина игрового поля в клетках
        /// </summary>
        public static int WidthSize = 10;
        /// <summary>
        /// Высота игрового поля в клетках
        /// </summary>
        public static int HeightSize = 10;
        /// <summary>
        /// Скорость игры. Время шага = 1000ms/GameSpeed
        /// </summary>
        public static int GameSpeed = 10;

        // Цвета для различных типов ячеек
        public static Color HeadColor = Color.RoyalBlue;
        public static Color BodyColor = Color.LightSteelBlue;
        public static Color FoodColor = Color.Coral;
        public static Color FieldColor = Color.Honeydew;
        public static Color WaypointColor = Color.LightYellow;
        public static Color ExceptionMessageColor = Color.DarkRed;
    }
}
