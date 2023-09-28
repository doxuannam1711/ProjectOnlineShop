using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Model.EF
{
    [Table("Credential")]
    [Serializable]//Phải có cái này thì mới chuyển sang session được bằng Session.Add()
    public class Credential
    {
        [Key]
        [StringLength(20)]
        public string UserGroupID { get; set; }

        [StringLength(50)]
        public string RoleID { get; set; }
    }
}
