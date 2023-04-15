﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SimpleSocialMedia_ClassLibrary.Entities
{
    [Table("Message")]
    public partial class Message
    {
        [Key]
        public int MessageID { get; set; }
        public int AccountID { get; set; }
        public int ChatID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string Text { get; set; }

        [ForeignKey("AccountID")]
        [InverseProperty("Messages")]
        public virtual Account Account { get; set; }

        [ForeignKey("ChatID")]
        [InverseProperty("Messages")]
        public virtual Chat Chat { get; set; }
    }
}