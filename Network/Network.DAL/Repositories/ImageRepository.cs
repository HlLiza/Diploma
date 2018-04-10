using Network.DAL.Interfaces;
using System;
using Network.DAL.EFModel;

namespace Network.DAL.Repositories
{
    public class ImageRepository :RepositoryBase, IImage
    {

        public ImageRepository(InstitutNetworkContext context) : base(context) { }

        public void Add(Image img)
        {
            _context.Image.Add(img);
            Save();
        }

        public void Delete(Image img)
        {
            _context.Image.Remove(img);
            Save();
        }

        public Image Find(Guid? id)
        {
            return _context.Image.Find(id);
        }

        
        public void Update(Image img)
        {
            var item = _context.Image.Find(img.Id);
            _context.Entry(item).CurrentValues.SetValues(img);
            Save();
        }
    }
}
