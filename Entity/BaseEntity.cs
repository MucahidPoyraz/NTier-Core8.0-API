using System;
using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        // Zaman bilgiler
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Kullanıcı bilgileri
        public int CreatedById { get; set; }
        public int? UpdatedById { get; set; }
        public int? DeletedById { get; set; }

        // Soft delete
        public bool IsDeleted { get; set; } = false;

        // Concurrency kontrolü
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
