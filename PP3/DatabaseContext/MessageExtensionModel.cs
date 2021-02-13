using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PP3.DatabaseContext
{
    [Table("tbl_message")]
    public class MessageModel
    {
        [Column("message_id"), Key]
        public int Id { get; set; }
        [Column("data")]
        public int Data { get; set; }
    }
}
