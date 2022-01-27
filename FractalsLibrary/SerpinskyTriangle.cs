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
    public class SerpinskyTriangle : Fractal
    {
        public override int MaxAvaibleRecursionDepth { get; } = 8;

        public override void StartRendering()
        {
            Sketch.Children.Clear();
            Point[] points =
            {
                new Point(Sketch.Width/2, 0),
                new Point(0, Sketch.Height*Math.Sqrt(3)/2),
                new Point(Sketch.Width, Sketch.Height*Math.Sqrt(3)/2)
            };
            RenderTriangle(points, 0);
            Rendering(points, 1);
        }

        private void Rendering(Point[] vertices, int recursionDepth)
        {
            if (recursionDepth >= MaxRecursionDepth)
            {
                return;
            }
            Point[] middlePoints =
            {
                MiddlePoint(vertices[1], vertices[2]),
                MiddlePoint(vertices[0], vertices[2]),
                MiddlePoint(vertices[0], vertices[1])
            };
            RenderTriangle(middlePoints, recursionDepth);
            Rendering(new Point[] { vertices[0], middlePoints[1], middlePoints[2] }, recursionDepth + 1);
            Rendering(new Point[] { vertices[1], middlePoints[0], middlePoints[2] }, recursionDepth + 1);
            Rendering(new Point[] { vertices[2], middlePoints[1], middlePoints[0] }, recursionDepth + 1);
        }

        private Point MiddlePoint(Point firstPoint, Point secondPoint)
            => new Point((firstPoint.X + secondPoint.X) / 2, (firstPoint.Y + secondPoint.Y) / 2);

        private void RenderTriangle(Point[] points, int recursionDepth)
        {
            Line line = new Line
            {
                X1 = points[0].X,
                X2 = points[1].X,
                Y1 = points[0].Y,
                Y2 = points[1].Y,
                Stroke = new SolidColorBrush(ColorPalette[recursionDepth])
            };
            Sketch.Children.Add(line);
            line = new Line
            {
                X1 = points[0].X,
                X2 = points[2].X,
                Y1 = points[0].Y,
                Y2 = points[2].Y,
                Stroke = new SolidColorBrush(ColorPalette[recursionDepth])
            };
            Sketch.Children.Add(line);
            line = new Line
            {
                X1 = points[2].X,
                X2 = points[1].X,
                Y1 = points[2].Y,
                Y2 = points[1].Y,
                Stroke = new SolidColorBrush(ColorPalette[recursionDepth])
            };
            Sketch.Children.Add(line);
        }

        public override string ToString() => "Треугольник Серпинского";
    }
}
