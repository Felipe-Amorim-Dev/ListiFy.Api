using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Entities
{
    public class ItemFoto
    {
        #region Atributos
        private Guid? _id;
        private byte[]? _foto;
        private Guid? _itemId;
        private Item? _item;
        #endregion

        #region Métodos
        public Guid? Id { get => _id; set => _id = value; }
        public byte[]? Foto { get => _foto; set => _foto = value; }
        public Guid? ItemId { get => _itemId; set => _itemId = value; }
        public Item? Item { get => _item; set => _item = value; }
        #endregion
    }
}
