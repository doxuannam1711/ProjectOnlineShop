using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = ("Yêu cầu nhập tên đăng nhập"))]
        public string UserName { set; get; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = ("Yêu cầu nhập mật khẩu"))]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = ("Độ dài mật khẩu ít nhất 6 ký tự"))]
        public string Password { set; get; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = ("Yêu cầu xác nhận mật khẩu"))]
        [Compare("Password", ErrorMessage = ("Mật khẩu không trùng khớp"))]
        public string ConfirmPassword { set; get; }

        [Display(Name = "Tên đầy đủ")]
        [Required(ErrorMessage = ("Yêu cầu nhập tên đăng nhập"))]
        public string Name { set; get; }

        [Display(Name = "Địa chỉ")]
        public string Adress { set; get; }

        [Required(ErrorMessage = ("Yêu cầu nhập Email"))]
        [Display(Name = "Địa chỉ Email")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z")]
        public string Email { set; get; }

        [Display(Name = "Điện thoại")]
        public string Phone { set; get; }

        [Display(Name = "Tỉnh/Thành")]
        public string ProvinceID { set; get; }

        [Display(Name = "Quận/Huyện")]
        public string DistrictID{ set; get; }

        [Display(Name = "Khu vực/Vùng")]
        public string PrecinctID { set; get; }
    }
}