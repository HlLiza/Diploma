using Network.DAL.EFModel;
using System;

namespace Network.DAL.Interfaces
{
    public interface IImage
    {
        void AddImage(Image img);
        Image FindImg(Guid? id);
       
    }
}
