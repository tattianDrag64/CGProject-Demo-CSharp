using System.Drawing;
using System;

namespace Draw
{
    public class ImageModel
    {
        public Bitmap Image { get; set; }
        public event Action ImageChanged;

        public void UpdateImage(Bitmap newImage)
        {
            Image = newImage;
            ImageChanged?.Invoke();
        }
    }
}