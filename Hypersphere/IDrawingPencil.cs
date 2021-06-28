using System.Windows;
using System.Windows.Controls;

namespace Hypersphere
{
    interface IDrawingPencil
    {
        public void CreatePencil(Canvas canvasForDraw);
        public void DrawLineGeometry(Point start, Point end);
        /*
         * Я карандаш для рисования
         * Чтобы рисовать мне нужно:
         * 1. холст
         * 2. координаты по которым я буду рисовать
         * У меня есть свойства:
         * 1. толщина
         * 2. цвет
         */
    }
}
