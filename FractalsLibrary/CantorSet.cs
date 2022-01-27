﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace FractalsLibrary
{
    public class CantorSet : Fractal
    {
        public override int MaxAvaibleRecursionDepth { get; } = 10;

        private double distance = 20;
        public double Distance
        {
            get => distance;
            set
            {
                if (value < 5 || value > 30)
                {
                    throw new ArgumentException("Недопустимое расстояние между отрезками.\n" +
                        "Введите число от 5 до 30.");
                }
                distance = value;
                StartRendering();
            }
        }

        private double thickness = 3;

        public override void StartRendering()
        {
            Sketch.Children.Clear();
            RenderSettings();
            Line line = new Line
            {
                X1 = 0,
                X2 = Sketch.Width,
                Y1 = Sketch.Height / 5,
                Y2 = Sketch.Height / 5,
                StrokeThickness = thickness,
                Stroke = new SolidColorBrush(StartColor)
            };
            Sketch.Children.Add(line);
            Rendering(line, 1);
        }

        private void Rendering(Line baseLine, int recursionDepth)
        {
            if (recursionDepth >= MaxRecursionDepth)
            {
                return;
            }
            Line[] lines = new Line[2];
            lines[0] = new Line
            {
                X1 = baseLine.X1,
                X2 = baseLine.X1 + (baseLine.X2 - baseLine.X1) / 3,
                Y1 = baseLine.Y1 + Distance,
                Y2 = baseLine.Y1 + Distance,
                StrokeThickness = thickness,
                Stroke = new SolidColorBrush(ColorPalette[recursionDepth])
            };
            Sketch.Children.Add(lines[0]);
            lines[1] = new Line
            {
                X1 = baseLine.X1 + 2 * (baseLine.X2 - baseLine.X1) / 3,
                X2 = baseLine.X2,
                Y1 = baseLine.Y1 + Distance,
                Y2 = baseLine.Y1 + Distance,
                StrokeThickness = thickness,
                Stroke = new SolidColorBrush(ColorPalette[recursionDepth])
            };
            Sketch.Children.Add(lines[1]);
            Rendering(lines[0], recursionDepth + 1);
            Rendering(lines[1], recursionDepth + 1);
        }

        public override string ToString() => "Множество Кантора";

        private void RenderSettings()
        {
            System.Windows.Controls.TextBox textBoxDistance = new();
            textBoxDistance.Text = Distance.ToString();
            textBoxDistance.KeyUp += TextBoxDistanceKeyUp;
            textBoxDistance.Width = 50;
            textBoxDistance.VerticalAlignment = VerticalAlignment.Center;
            textBoxDistance.Margin = new Thickness(5, 5, 0, 5);
            System.Windows.Controls.Label labelDistance = new();
            labelDistance.Content = "- Растояние между отрезками";
            System.Windows.Controls.StackPanel settings = new();
            settings.Orientation = System.Windows.Controls.Orientation.Horizontal;
            settings.Children.Add(textBoxDistance);
            settings.Children.Add(labelDistance);
            Sketch.Children.Add(settings);
        }

        private void TextBoxDistanceKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                Distance = int.Parse(((System.Windows.Controls.TextBox)sender).Text);
        }
    }
}
