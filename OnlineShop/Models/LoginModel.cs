﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="Bạn phải nhập tên đăng nhập")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Bạn phải nhập Mật khẩu")]
        [Display(Name = "Mật khẩu")]
        public string Password { set; get; }

    }
}