using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;

namespace Network.DAL.Repositories
{
    public class ImageRepository :RepositoryBase, IImage
    {

        public ImageRepository(InstitutNetworkContext context) : base(context) { }

        public void AddImage(Image img)
        {
            _context.Image.Add(img);
            base.Save();
        }

        public Image FindImg(Guid? id)
        {
            return _context.Image.Find(id);
        }

        
    }
}
