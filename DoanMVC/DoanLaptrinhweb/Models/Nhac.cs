//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoanLaptrinhweb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Nhac
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nhac()
        {
            this.ChiTietDatHangs = new HashSet<ChiTietDatHang>();
        }
    
        public int MaNhac { get; set; }
        public string TenNhac { get; set; }
        public string TenCasi { get; set; }
        public Nullable<decimal> GiaBan { get; set; }
        public string AnhBia { get; set; }
        public Nullable<System.DateTime> NgayCapNhat { get; set; }
        public Nullable<int> SoLuongTon { get; set; }
        public Nullable<int> MaDangnhac { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDatHang> ChiTietDatHangs { get; set; }
        public virtual Dangnhac Dangnhac { get; set; }
    }
}
