using SixLabors.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using SD=System.Drawing;
using IS = SixLabors.Primitives;

namespace ChakraCore.NET.Plugin.Drawing.ImageSharp
{
    public static class ValueConvertHelper
    {
        public static IS.PointF ToPointF(SD.PointF value,Matrix3x2 matrix)
        {
            return IS.PointF.Transform( new IS.PointF(value.X, value.Y),matrix);
        }
        public static IS.PointF ToPointF(SD.PointF value)
        {
            return new IS.PointF(value.X, value.Y);
        }

        public static IS.PointF[] ToPointFArray(this IEnumerable<SD.PointF> value, Matrix3x2 matrix)
        {
            return (from item in value
                    select ToPointF(item,matrix)).ToArray();
        }

        public static IS.SizeF ToSizeF(SD.SizeF value, Matrix3x2 matrix)
        {
            return IS.SizeF.Transform(new IS.SizeF() { Height = value.Height, Width = value.Width },matrix);
        }
        public static IS.SizeF ToSizeF(SD.SizeF value)
        {
            return new IS.SizeF() { Height = value.Height, Width = value.Width };
        }

        public static IS.Point ToPoint(SD.PointF value,Matrix3x2 matrix)
        {
            var tmp = ToPointF(value,matrix);
            return  new IS.Point((int)tmp.X, (int)tmp.Y);
        }

        public static IS.Size ToSize(SD.SizeF value,Matrix3x2 matrix)
        {
            var tmp = ToSizeF(value, matrix);
            return new IS.Size((int)tmp.Width, (int)tmp.Height);
        }

        public static IS.RectangleF ToRectangleF(SD.RectangleF value, Matrix3x2 matrix)
        {
            return IS.RectangleF.Transform( new IS.RectangleF(value.X, value.Y,value.Width,value.Height),matrix);
        }

        public static EllipsePolygon ToEllipsePolygon(SD.PointF position, SD.SizeF size,Matrix3x2 matrix)
        {
            return new EllipsePolygon(ToPointF(position), ToSizeF(size)).Transform(matrix);
        }
    }
}
