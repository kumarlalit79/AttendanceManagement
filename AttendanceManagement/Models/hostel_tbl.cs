//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AttendanceManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class hostel_tbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hostel_tbl()
        {
            this.student_tbl = new HashSet<student_tbl>();
        }
    
        public int hostel_id { get; set; }
        public string hostel_name { get; set; }
        public System.DateTime Created_date { get; set; }
        public string Created_by { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<student_tbl> student_tbl { get; set; }
    }
}
