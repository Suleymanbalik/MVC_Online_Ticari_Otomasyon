using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MVC_Online_Ticari_Otomasyon.Models.Classes
{
    public class Message
    {
        [Key]
        [DisplayName("Message ID")]
        public int MessageId { get; set; }

        [DisplayName("Sender")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageSender { get; set; }

        [DisplayName("Receiver")]
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageReceiver { get; set; }

        [DisplayName("Subject")]
        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        public string MessageSubject { get; set; }

        [DisplayName("Content")]
        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string MessageContent { get; set; }

        [DisplayName("Date")]
        [Column(TypeName = "Date")]
        public DateTime MessageDate { get; set; }
    }
}