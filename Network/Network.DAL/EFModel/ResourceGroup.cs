//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Network.DAL.EFModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResourceGroup
    {
        public System.Guid Id { get; set; }
        public System.Guid GroupId { get; set; }
        public byte[] Resource { get; set; }
        public string Comments { get; set; }
        public System.Guid AuthorId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}