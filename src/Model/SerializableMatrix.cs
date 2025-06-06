using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    public class SerializableMatrix
    {
        public float[] Elements { get; set; }

        public SerializableMatrix()
        {
            Elements = new float[6]; 
        }

        public SerializableMatrix(Matrix matrix)
        {
            Elements = matrix.Elements;
        }

        public Matrix ToMatrix()
        {
            Matrix matrix = new Matrix();
            matrix = new Matrix(
                Elements[0], Elements[1],
                Elements[2], Elements[3],
                Elements[4], Elements[5]
            );
            return matrix;
        }
    }
}
