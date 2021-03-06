using System;
using System.Collections;
using System.Collections.Generic;

namespace Gma.QrCodeNet.Encoding
{
    internal struct MatrixRectangle : IEnumerable<MatrixPoint>
    {
        public MatrixPoint Location { get; private set; }
        public MatrixSize Size { get; private set; }

        internal MatrixRectangle(MatrixPoint location, MatrixSize size) :
            this()
        {
            Location = location;
            Size = size;
        }

        public IEnumerator<MatrixPoint> GetEnumerator()
        {
            for (int j = Location.Y; j < Location.Y + Size.Height; j++)
            {
                for (int i = Location.X; i < Location.X + Size.Width; i++)
                {
                    yield return new MatrixPoint(i, j);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Format("Rectangle({0};{1}):({2} x {3})", Location.X, Location.Y, Size.Width, Size.Height);
        }
    }
}
