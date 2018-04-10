using Network.DAL.EFModel;
using System;

namespace Network.DAL.Interfaces
{
    public interface IImage
    {
        void Add(Image img);
        void Update(Image img);
        Image Find(Guid? id);
        void Delete(Image img);
    }
}
